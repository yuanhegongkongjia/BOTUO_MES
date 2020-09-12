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
    public class WFCore_AllTaskDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,b.Requestor,b.RequestorName,b.RequestorProxy,b.RequestorProxyName,b.InstanceStatus,b.RequestTime
                from WF_T_INSTANCESTEPEXECUTOR a left outer join WF_T_INSTANCE b on a.InstanceId=b.InstanceId where 1=1";

                //var sql = @"select * from v_ad_alltask a where 1=1 ";
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
                sql += " order by a.LastModifyTime desc";
                var parameters = new
                {
                    ExecuteStatus = entity["ExecuteStatus"].Split(',').ToList(),
                    InstanceId = string.Format("%{0}%", entity["InstanceId"]),
                    ModelName = string.Format("%{0}%", entity["ModelName"]),
                    AFENumber = string.Format("%{0}%", entity["AFENumber"].ToUpper()),
                    ProjectName = string.Format("%{0}%", entity["ProjectName"]),
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCESTEPEXECUTOR>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

    }
}
