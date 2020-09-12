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
    public class WFCore_TaskProcessingDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["InstanceStepExecutorId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = @"select a.*,c.DFFormName from WF_T_INSTANCESTEPEXECUTOR a 
left outer join WF_T_INSTANCE b on a.InstanceId=b.InstanceId
left outer join WF_M_MODEL c on b.ModelId=c.ModelId where 1=1";
                    sql += " and a.InstanceStepExecutorId=@InstanceStepExecutorId";
                    var parameters = new
                    {
                        InstanceStepExecutorId = entity["InstanceStepExecutorId"]
                    };
                    var item = db.Query<VM_WF_T_INSTANCESTEPEXECUTOR>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<VM_WF_T_INSTANCESTEPEXECUTOR>(item);
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(entity["InstanceId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = @"select a.*,b.DFFormName from WF_T_INSTANCE a
left outer join WF_M_MODEL b on a.ModelId=b.ModelId where 1=1";
                    sql += " and a.InstanceId=@InstanceId";
                    var parameters = new
                    {
                        InstanceId = entity["InstanceId"]
                    };
                    var item = db.Query<VM_WF_T_INSTANCE>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<VM_WF_T_INSTANCE>(item);
                    }
                }
            }
            else
            {
                throw new Exception("无效的参数，必须提供 InstanceStepExecutorId 或者 InstanceId".GetRes());
            }

            return dict;
        }
    }
}
