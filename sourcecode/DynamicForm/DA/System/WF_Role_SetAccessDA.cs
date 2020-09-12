using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WF_Role_SetAccessDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var dict = new DFDictionary();
                if (!string.IsNullOrWhiteSpace(entity["RoleId"]))
                {
                    using (var db = Pub.DB)
                    {
                        var sql = "select * from WF_M_ROLE where RoleId=@RoleId";
                        var item = db.Query<WF_M_ROLE>(sql, new { RoleId = entity["RoleId"] }).FirstOrDefault();
                        if (null != item)
                        {
                            dict.Merge(DFDictionary.Create<WF_M_ROLE>(item));
                        }

                        // 载入权限
                        sql = "select * from WF_M_AUTH_MODULE where RoleId=@RoleId";
                        var list1 = db.Query<WF_M_AUTH_MODULE>(sql, new { RoleId = entity["RoleId"] }).ToList();
                        dict.Add("DF_TREE_VIEWSTATE1", Base64StringHelper.ConvertToBase64String(JsonSerializeHelper.SerializeObject(
                            list1.Select(a => new WFTreeNode { id = a.ModuleId, _checked = true, expanded = true }))));

                        sql = "select * from WF_M_AUTH_DATA where RoleId=@RoleId";
                        var list2 = db.Query<WF_M_AUTH_DATA>(sql, new { RoleId = entity["RoleId"] }).ToList();
                        dict.Add("DF_TREE_VIEWSTATE2", Base64StringHelper.ConvertToBase64String(JsonSerializeHelper.SerializeObject(
                            list2.Select(a => new WFTreeNode { id = a.DeptId, _checked = true, expanded = true }))));
                    }
                }
                return dict;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {

            try
            {
                var currentUser = Util.GetCurrentUser();
                var listStatus1 = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(entity["DF_TREE_VIEWSTATE1"]));
                if (listStatus1 == null)
                {
                    throw new Exception("Invalid DF_TREE_VIEWSTATE1");
                }
                var listStatus2 = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(entity["DF_TREE_VIEWSTATE2"]));
                if (listStatus2 == null)
                {
                    throw new Exception("Invalid DF_TREE_VIEWSTATE2");
                }
                if (string.IsNullOrWhiteSpace(entity["RoleId"]))
                {
                    throw new ArgumentNullException("RoleId");
                }

                using (var db = Pub.DB)
                {
                    var sql = string.Empty;

                    sql = "DELETE FROM WF_M_AUTH_MODULE WHERE RoleId=@RoleId";
                    db.Execute(sql, new { RoleId = entity["RoleId"] });

                    sql = "DELETE FROM WF_M_AUTH_DATA WHERE RoleId=@RoleId";
                    db.Execute(sql, new { RoleId = entity["RoleId"] });

                    var checkedList = listStatus1.Where(a => a._checked).ToList();
                    db.Insert(checkedList.Select(a => new WF_M_AUTH_MODULE()
                    {
                        AuthId = Guid.NewGuid().ToString(),
                        ModuleId = a.id,
                        RoleId = entity["RoleId"],
                        CreateUser = currentUser.UserName,
                        CreateTime = DateTime.Now,
                        LastModifyUser = currentUser.UserName,
                        LastModifyTime = DateTime.Now
                    }));

                    checkedList = listStatus2.Where(a => a._checked).ToList();
                    db.Insert(checkedList.Select(a => new WF_M_AUTH_DATA()
                    {
                        AuthId = Guid.NewGuid().ToString(),
                        DeptId = a.id,
                        RoleId = entity["RoleId"],
                        CreateUser = currentUser.UserName,
                        CreateTime = DateTime.Now,
                        LastModifyUser = currentUser.UserName,
                        LastModifyTime = DateTime.Now
                    }));
                }
                message = "保存成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }


        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var subAction = entity["subAction"];
            if (entity["action"] == "querylist")
            {
                if (subAction == "QueryMenu")
                {
                    // 客户端在载入的时候调用树的数据源
                    vm.rows = QueryModuleTree(entity);
                }
                else if (subAction == "QueryDept")
                {
                    vm.rows = QueryDeptTree(entity);
                }
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private List<WFTreeNode> QueryModuleTree(DFDictionary dict)
        {
            var root = WF_M_MODULELoader.Query("ROOT").FirstOrDefault();
            var nodes = WF_M_MODULELoader.Query(string.Empty).OrderBy(a => a.ModuleOrder.GetValueOrDefault()).Select(a => new WFTreeNode()
            {
                pid = a.PModuleId,
                id = a.ModuleId,
                text = a.ModuleName
            }).ToList();
            var listStatus = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(dict["DF_TREE_VIEWSTATE1"]));
            var list = WFTreeHelper.GenerateTree("ROOT", nodes);
            WFTreeHelper.SetStatus(list, listStatus);
            return list;
        }

        private List<WFTreeNode> QueryDeptTree(DFDictionary dict)
        {
            var root = WF_M_DEPTLoader.Query("ROOT").FirstOrDefault();
            var nodes = WF_M_DEPTLoader.Query(string.Empty).OrderBy(a => a.DeptOrder.GetValueOrDefault()).Select(a => new WFTreeNode()
            {
                pid = a.PDeptId,
                id = a.DeptId,
                text = a.DeptName
            }).ToList();
            var listStatus = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(dict["DF_TREE_VIEWSTATE2"]));
            var list = WFTreeHelper.GenerateTree("ROOT", nodes);
            WFTreeHelper.SetStatus(list, listStatus);
            return list;
        }

    }
}
