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
    public class WFCore_MyWFDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var user = Util.GetCurrentUser();
            GetList(entity, vm, start, limit, user.UserId);
            return DFPub.EXECUTE_SUCCESS;
        }

        public static void GetList(DFDictionary entity, DataGridVM vm, int start, int limit, string userId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,user1.ChineseName + ISNULL('[' + user2.ChineseName + ' 代理申请]','') AS ChineseName,dept.DeptDisplayText
                from WF_T_INSTANCE a
                LEFT OUTER JOIN dbo.WF_M_USER user1 ON a.Requestor=user1.UserId
                LEFT OUTER JOIN dbo.WF_M_DEPT dept ON dept.DeptId = user1.DeptId
                LEFT OUTER JOIN dbo.WF_M_USER user2 ON a.RequestorProxy=user2.UserId
                WHERE 1=1";
                //var sql = @"select * from v_ad_mywf a where 1=1 ";
                sql += " and (a.Requestor=@UserId or a.RequestorProxy=@UserId)";
                if (!string.IsNullOrWhiteSpace(entity["InstanceStatus"]))
                {
                    sql += " and a.InstanceStatus in @InstanceStatus";
                }
                if (!string.IsNullOrWhiteSpace(entity["InstanceId"]))
                {
                    sql += " and a.InstanceId like @InstanceId";
                }
                if (!string.IsNullOrWhiteSpace(entity["ModelName"]))
                {
                    sql += " and a.ModelName like @ModelName";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestTimeFrom"]))
                {
                    sql += " and a.RequestTime>=@RequestTimeFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestTimeTo"]))
                {
                    sql += " and a.RequestTime<=@RequestTimeTo";
                }
                if (!string.IsNullOrWhiteSpace(entity["AFENumber"]))
                {
                    sql += " and a.AFENumber like @AFENumber";
                }
                if (!string.IsNullOrWhiteSpace(entity["ProjectName"]))
                {
                    sql += " and a.ProjectName like @ProjectName";
                }
                sql += " order by a.LastModifyTime desc";
                var parameters = new
                {
                    UserId = userId,
                    InstanceStatus = entity["InstanceStatus"].Split(',').ToList(),
                    InstanceId = string.Format("%{0}%", entity["InstanceId"]),
                    ModelName = string.Format("%{0}%", entity["ModelName"]),
                    RequestTimeFrom = ParseHelper.ParseDate(entity["RequestTimeFrom"]).GetValueOrDefault().ToString("yyyy-MM-dd"),
                    RequestTimeTo = ParseHelper.ParseDate(entity["RequestTimeTo"]).GetValueOrDefault().ToString("yyyy-MM-dd 23:59:59.999"),
                    AFENumber = string.Format("%{0}%", entity["AFENumber"].ToUpper()),
                    ProjectName = string.Format("%{0}%",entity["ProjectName"])
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
            }
        }

      

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            this.Model.Add("InstanceStatus", "Finished,Running");
            return base.Get(form, entity, ref message);
        }

        public static VM_WF_T_INSTANCE Get(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,user1.ChineseName + ISNULL('[' + user2.ChineseName + ' 代理申请]','') AS ChineseName,dept.DeptDisplayText
from WF_T_INSTANCE a
LEFT OUTER JOIN dbo.WF_M_USER user1 ON a.Requestor=user1.UserId
LEFT OUTER JOIN dbo.WF_M_DEPT dept ON dept.DeptId = user1.DeptId
LEFT OUTER JOIN dbo.WF_M_USER user2 ON a.RequestorProxy=user2.UserId
WHERE 1=1";
                var parameters = new
                {
                    InstanceId = QueryBuilder.Equal(ref sql, dict, "a.InstanceId", "InstanceId")
                };
                return db.Query<VM_WF_T_INSTANCE>(sql, parameters).FirstOrDefault();
            }
        }
    }
}
