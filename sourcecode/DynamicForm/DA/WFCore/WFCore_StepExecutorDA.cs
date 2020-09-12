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
    public class WFCore_StepExecutorDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["StepId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_STEP where 1=1";
                    sql += " and StepId=@StepId";
                    var parameters = new
                    {
                        StepId = entity["StepId"]
                    };
                    var item = db.Query<WF_M_STEP>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_M_STEP>(item);
                    }
                }
            }
            return dict;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_STEPEXECUTOR where 1=1";

                sql += " and ModelId=@ModelId and StepId=@StepId";
                sql += " order by ExecutorPriority";
                var parameters = new
                {
                    ModelId = entity["ModelId"],
                    StepId = entity["StepId"]
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_STEPEXECUTOR>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数 data".GetRes());
            }
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_STEPEXECUTOR where ExecutorId=@ExecutorId";
                db.Execute(sql, data.Select(a => new { ExecutorId = a["ExecutorId"] }));
            }
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
