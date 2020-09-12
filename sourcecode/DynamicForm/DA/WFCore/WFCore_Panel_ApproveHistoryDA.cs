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
    public class WFCore_Panel_ApproveHistoryDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            GetList(entity, vm, start, limit);
            return DFPub.EXECUTE_SUCCESS;
        }

        public static void GetList(DFDictionary entity, DataGridVM vm, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.InstanceId,a.InstanceStepId,a.StepId,a.StepName,a.InstanceStepStatus,b.ExecutorId,b.ExecutorName,
b.ExecuteResult,b.ExecuteStatus,b.ExecuteComment,b.Memo,b.LastModifyTime,b.LastModifyUser,c.ChineseName,b.Extend01 as AdditionalApprove
FROM WF_T_INSTANCESTEP a
left outer join WF_T_INSTANCESTEPEXECUTOR b on a.InstanceStepId=b.InstanceStepId 
LEFT OUTER JOIN dbo.WF_M_USER c ON b.ExecutorId=c.UserId
WHERE 1=1";
                sql += " and a.InstanceId=@InstanceId";
                sql += " order by a.CreateTime desc,b.CreateTime desc";
                var parameters = new
                {
                    InstanceId = entity["InstanceId"]
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCESTEP>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                list.ForEach(a =>
                {
                    if (!string.IsNullOrWhiteSpace(a.AdditionalApprove))
                    {
                        a.StepName = string.Format("{0}-{1}", a.StepName, a.AdditionalApprove.Replace("来源".GetRes(), string.Empty));
                    }
                });
                vm.rows = list;
            }
        }

    }
}
