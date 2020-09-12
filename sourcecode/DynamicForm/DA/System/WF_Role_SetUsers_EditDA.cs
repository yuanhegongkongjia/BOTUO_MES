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
    public class WF_Role_SetUsers_EditDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["RoleId"]))
            {
                throw new ArgumentNullException("RoleId");
            }
            /*基本查询语句*/
            var sql = @"SELECT a.* FROM WF_M_USER a where 1=1";

            /*查询条件*/

            sql += " and a.UserId not in (select UserId from WF_M_USERROLE where RoleId=@RoleId)";
            sql += " order by a.UserName";
            using (var db = Pub.DB)
            {
                var parameters = new
                {
                    DeptId = string.Format("%{0}%", entity["DeptId"]),
                    RoleId = entity["RoleId"],
                    EmployeeName = string.Format("%{0}%", entity["EmployeeName"])
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_USER>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var currentUser = Util.GetCurrentUser();
                if (string.IsNullOrWhiteSpace(entity["RoleId"]))
                {
                    throw new ArgumentNullException("RoleId");
                }

                var list = JsonSerializeHelper.DeserializeObject<List<VM_WF_M_USER>>(Base64StringHelper.ConvertFromBase64String(entity[DFPub.GetKey_GridHiddenValue("grid1")]));
                if (list == null)
                {
                    throw new Exception("Invalid grid data");
                }

                using (var db = Pub.DB)
                {
                    var sql = "DELETE FROM WF_M_USERROLE WHERE RoleId=@RoleId and UserId=@UserId";
                    db.Execute(sql, list.Select(a => new { UserId = a.UserId, RoleId = entity["RoleId"] }));

                    db.Insert(list.Where(a => a.selected).Select(a => new WF_M_USERROLE()
                    {
                        UserRoleId = Guid.NewGuid().ToString(),
                        UserId = a.UserId,
                        RoleId = entity["RoleId"],
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
