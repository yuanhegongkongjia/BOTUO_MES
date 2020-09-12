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
    public class WFCore_MyTaskDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var user = Util.GetCurrentUser();
            GetList(entity, vm, start, limit, user.UserId);
            return DFPub.EXECUTE_SUCCESS;
        }

        public static void GetList(DFDictionary entity, DataGridVM vm, int start, int limit, string userId, string additionalSql = "")
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,b.Requestor,b.RequestorName,b.RequestorProxy,b.RequestorProxyName,b.InstanceStatus,b.RequestTime
                ,user1.ChineseName + ISNULL('[' + user2.ChineseName + ' 代理申请]','') AS ChineseName,dept.DeptDisplayText
                from WF_T_INSTANCESTEPEXECUTOR a left outer join WF_T_INSTANCE b on a.InstanceId=b.InstanceId
                LEFT OUTER JOIN dbo.WF_M_USER user1 ON b.Requestor=user1.UserId
                LEFT OUTER JOIN dbo.WF_M_DEPT dept ON dept.DeptId = user1.DeptId
                LEFT OUTER JOIN dbo.WF_M_USER user2 ON b.RequestorProxy=user2.UserId
                WHERE 1=1";

                //var sql = @"select * from v_ad_mytask a where 1=1 ";
                sql += " and a.ExecutorId=@UserId";
                if (!string.IsNullOrWhiteSpace(entity["ExecuteStatus"]))
                {
                    sql += " and a.ExecuteStatus in @ExecuteStatus";
                }
                if (!string.IsNullOrWhiteSpace(entity["InstanceId"]))
                {
                    sql += " and a.InstanceId like @InstanceId";
                }
                if (!string.IsNullOrWhiteSpace(entity["ModelName"]))
                {
                    sql += " and a.ModelName like @ModelName";
                }
                if (!string.IsNullOrWhiteSpace(entity["AFENumber"]))
                {
                    sql += " and a.AFENumber like @AFENumber";
                }
                if (!string.IsNullOrWhiteSpace(entity["ProjectName"]))
                {
                    sql += " and a.ProjectName like @ProjectName";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestorName"]))
                {
                    sql += " and a.RequestorName like @RequestorName";
                }
                sql += additionalSql;
                sql += " order by a.LastModifyTime desc";
                var parameters = new
                {
                    UserId = userId,
                    ExecuteStatus = entity["ExecuteStatus"].Split(',').ToList(),
                    InstanceId = string.Format("%{0}%", entity["InstanceId"]),
                    ModelName = string.Format("%{0}%", entity["ModelName"]),
                    AFENumber= string.Format("%{0}%", entity["AFENumber"].ToUpper()),
                    ProjectName = string.Format("%{0}%", entity["ProjectName"]),
                    RequestorName=string.Format("%{0}%", entity["RequestorName"])
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCESTEPEXECUTOR>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                list.ForEach(a => a.InstanceStatus = a.InstanceStatus.GetRes());
                vm.rows = list;
            }
        }

    }
}
