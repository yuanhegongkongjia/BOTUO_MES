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
    public class WFCore_Panel_RequestorInfoDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["InstanceId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_T_INSTANCE where 1=1";
                    sql += " and InstanceId=@InstanceId";
                    var parameters = new
                    {
                        InstanceId = entity["InstanceId"]
                    };
                    var item = db.Query<WF_T_INSTANCE>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_T_INSTANCE>(item);
                    }
                }
            }
            return dict;
        }
    }
}
