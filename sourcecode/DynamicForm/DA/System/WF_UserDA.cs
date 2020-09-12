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
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WF_UserDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                using (var db = Pub.DB)
                {
                    if (entity["subAction"] == "ResetPassword")
                    {
                        var sql = "select * from WF_M_USER where UserId=@UserId";
                        foreach (var item in data)
                        {
                            var oldEntity = db.Query<WF_M_USER>(sql, new { UserId = item["UserId"] }).FirstOrDefault();
                            if (null != oldEntity)
                            {
                                oldEntity.Password = HashHelper.GenerateUserHash(oldEntity.UserName, "123456");
                                db.Update(oldEntity);
                            }
                        }
                        message = "重置成功，已经将密码重置为 123456".GetRes();
                    }
                    else
                    {
                        var sql = "delete from WF_M_USER where UserId=@UserId";
                        db.Execute(sql, data.Select(a => new { UserId = a["UserId"] }));
                        message = "删除成功".GetRes();
                    }
                }
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
            try
            {
                /*基本查询语句*/
                var sql = @"select a.*,b.DeptName from WF_M_USER a left outer join WF_M_DEPT b on a.DeptId=b.DeptId where 1=1";

                /*查询条件*/
                if (!string.IsNullOrWhiteSpace(entity["UserName"]))
                {
                    sql += " and a.UserName like @UserName";
                }
                sql += " order by a.UserName";
                using (var db = Pub.DB)
                {
                    var parameters = new
                    {
                        UserName = string.Format("%{0}%", entity["UserName"])
                    };
                    vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                    var list = db.Query<VM_WF_M_USER>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                    list.ForEach(a =>
                    {
                        a.Roles = string.Join(",", UserRoleLoader.QueryByUserId(a.UserId).Where(b => !string.IsNullOrWhiteSpace(b.RoleName)).Select(b => b.RoleName));
                    });
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
    }
}
