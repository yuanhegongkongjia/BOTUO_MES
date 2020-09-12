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
    public class WFCore_StartWFTestDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["ModelId"]))
            {
                throw new Exception("缺少参数 ModelId".GetRes());
            }

            var engine = NinjectHelper.Get<IEngine>();
            if (engine == null)
            {
                throw new Exception("找不到 IEngine".GetRes());
            }

            var user = Util.GetCurrentUser();
            var instanceId = Pub.GetNextIdFromDB("T", DateTime.Now.ToString("yyyyMMdd"), string.Empty, 4);

            var Requestor = user.UserId;
            var RequestorName = user.UserName;
            var RequestorProxy = string.Empty;
            var RequestorProxyName = string.Empty;


            // 开始工作流
            engine.StartDBWF(entity["ModelId"], instanceId, Requestor, RequestorProxy, RequestorName,
                RequestorProxyName, DateTime.Now, true, user.UserId, user.UserName);

            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["ModelId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_T_INSTANCE where 1=1";
                    sql += " and InstanceId=@InstanceId";
                    var parameters = new
                    {
                        InstanceId = instanceId
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
