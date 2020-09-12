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
    public class WFCore_ConnectorDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["ModelId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_MODEL where 1=1";
                    sql += " and ModelId=@ModelId";
                    var parameters = new
                    {
                        ModelId = entity["ModelId"]
                    };
                    var item = db.Query<WF_M_MODEL>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_M_MODEL>(item);
                    }
                }
            }
            return dict;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,b.StepName as FromStepName,c.StepName as ToStepName from WF_M_CONNECTOR a 
left outer join WF_M_STEP b on a.FromStepId=b.StepId 
left outer join WF_M_STEP c on a.ToStepId=c.StepId 
where 1=1";
                sql += " and a.ModelId=@ModelId";
                var parameters = new
                {
                    ModelId = entity["ModelId"]
                };
                sql += " order by b.StepOrder";
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_CONNECTOR>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

    }
}
