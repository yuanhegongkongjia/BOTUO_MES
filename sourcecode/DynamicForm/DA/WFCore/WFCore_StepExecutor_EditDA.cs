using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm.DA
{
    public class WFCore_StepExecutor_EditDA : BaseDA
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
    }
}
