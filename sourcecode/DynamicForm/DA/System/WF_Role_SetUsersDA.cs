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
    public class WF_Role_SetUsersDA : BaseDA
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
            /*基本查询语句*/
            var sql = @"SELECT a.*,b.UserName FROM WF_M_USERROLE a
JOIN WF_M_USER b ON a.UserId=b.UserId
WHERE 1=1";

            /*查询条件*/
            sql += " and a.RoleId=@RoleId";
            sql += " order by b.UserName";

            using (var db = Pub.DB)
            {
                var parameters = new { RoleId = entity["RoleId"] };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_USERROLE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

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
                    var sql = "delete from WF_M_USERROLE where UserId=@UserId and RoleId=@RoleId";
                    db.Execute(sql, data.Select(a => new { UserId = a["UserId"], RoleId = a["RoleId"] }));
                }
                message = "删除成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
    }
}
