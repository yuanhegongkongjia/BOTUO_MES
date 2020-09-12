using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;

namespace DynamicForm.DA
{
    public class WF_UserRole_SettingDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var dict = new DFDictionary();
                if (!string.IsNullOrWhiteSpace(entity["UserId"]))
                {
                    using (var db = Pub.DB)
                    {
                        var sql = "SELECT * FROM WF_M_USER where UserId=@UserId";
                        var item = db.Query<VM_WF_M_USER>(sql, new { UserId = entity["UserId"] }).FirstOrDefault();
                        if (null != item)
                        {
                            dict.Merge(DFDictionary.Create<VM_WF_M_USER>(item));
                        }
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

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            try
            {
                /*基本查询语句*/
                var sql = @"select a.*,(case when c.RoleId is not null then 1 else 0 end) as selected from WF_M_ROLE a
LEFT OUTER JOIN WF_M_USERROLE c ON a.RoleId=c.RoleId AND c.UserId=@UserId";

                sql += " order by a.RoleId";

                using (var db = Pub.DB)
                {
                    var parameters = new
                    {
                        UserId = entity["UserId"]
                    };
                    vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                    var list = db.Query<VM_WF_M_ROLE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                    vm.rows = list;
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = ex.Message;
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var currentUser = Util.GetCurrentUser();                
                if (string.IsNullOrWhiteSpace(entity["UserId"]))
                {
                    throw new ArgumentNullException("UserId");
                }

                var list = JsonSerializeHelper.DeserializeObject<List<VM_WF_M_ROLE>>(Base64StringHelper.ConvertFromBase64String(entity[DFPub.GetKey_GridHiddenValue("grid1")]));               
                
                if (list == null)
                {
                    throw new Exception("Invalid grid data");
                }

                using (var db = Pub.DB)
                {
                    var sql = "DELETE FROM WF_M_USERROLE WHERE RoleId=@RoleId and UserId=@UserId";
                    db.Execute(sql, list.Select(a => new { RoleId = a.RoleId, UserId = entity["UserId"] }));

                    db.Insert(list.Where(a => a.selected).Select(a => new WF_M_USERROLE()
                    {
                        UserRoleId = Guid.NewGuid().ToString(),
                        RoleId = a.RoleId,
                        UserId = entity["UserId"],
                        CreateUser = currentUser.UserName,
                        CreateTime = DateTime.Now,
                        LastModifyUser = currentUser.UserName,
                        LastModifyTime = DateTime.Now
                    }));

                    message = "保存成功".GetRes();
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }      
    }
}
