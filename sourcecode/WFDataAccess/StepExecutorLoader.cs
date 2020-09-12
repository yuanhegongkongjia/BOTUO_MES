using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.Utility;
using System.Text;
using System.Data;
using WFCommon.VM;

namespace WFDataAccess
{
    public class StepExecutorLoader
    {
        /// <summary>
        /// PO 打印单子直线式最后一个签合人
        /// </summary>
        /// <param name="InstanceId"></param>
        /// <returns></returns>
        public static DataTable GetLastStepExecutorsByInstanceId(string InstanceId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select Top 1*,convert(varchar(23),LastModifyTime,120) as LastModifyTime1 from WF_T_INSTANCESTEPEXECUTOR where InstanceId=@InstanceId order by lastmodifytime desc";
                return db.ExecuteDataTable(sql, new { InstanceId = InstanceId });
            }
        }
        public static DataTable GetStepExecutorsByInstanceId(string InstanceId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select *,convert(varchar(23),LastModifyTime,120) as LastModifyTime1 from WF_T_INSTANCESTEPEXECUTOR where InstanceId=@InstanceId order by lastmodifytime asc";
                return db.ExecuteDataTable(sql, new { InstanceId = InstanceId });
            }
        }

        public static List<VM_WF_T_INSTANCE> GetReimbursement(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = @"
select * from wf_t_instance a
where a.modelid='09ec1ea5-9ed7-4c62-8563-cc82db23afa9'
and a.InstanceStatus='Finished'
and not exists (select 1 from xdsw_t_certificate_d c where c.InstanceId=a.InstanceId)
 ";
                if (!string.IsNullOrWhiteSpace(dict["InstanceId"]))
                {
                    sql += " and a.InstanceId like @InstanceId";
                }

                sql += " order by a.InstanceId desc";
                var parameters = new
                {

                    InstanceId = string.Format("%{0}%", dict["InstanceId"])
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                return list;
            }
        }

        public static String GetCurrentExecutorName(string InstanceId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select * from dbo.WF_T_INSTANCESTEPEXECUTOR
where instanceid=@InstanceId and ExecuteStatus='Unfinished' order by LastModifyTime desc";
                var list = db.Query<WF_T_INSTANCESTEPEXECUTOR>(sql, new { InstanceId = InstanceId }).ToList();

                if(list.Count == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return string.Join(",", list.Select(a => a.ExecutorName));
                }
            }
        }

        public static List<WF_T_INSTANCESTEPEXECUTOR> GetList(string InstanceId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select StepName from dbo.WF_T_INSTANCESTEPEXECUTOR
where InstanceId=@InstanceId";
                var list = db.Query<WF_T_INSTANCESTEPEXECUTOR>(sql, new { InstanceId = InstanceId }).ToList();

                return list;
            }
        }

        public static string GetStepName(string InstanceStepExecutorId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select StepName from dbo.WF_T_INSTANCESTEPEXECUTOR
where InstanceStepExecutorId=@InstanceStepExecutorId";
                var item = db.Query<string>(sql, new { InstanceStepExecutorId = InstanceStepExecutorId }).FirstOrDefault();

                return item;
            }
        }
    }
}
