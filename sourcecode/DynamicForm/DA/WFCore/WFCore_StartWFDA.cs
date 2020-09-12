using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WFCore_StartWFDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            ArgumentCheck.CheckMustInput(entity, "ModelId");

            var engine = NinjectHelper.Get<IEngine>();
            if (engine == null)
            {
                throw new Exception("找不到 IEngine".GetRes());
            }

            var user = Util.GetCurrentUser();
            var instanceId = string.Empty;

            // 信达生物 SAP 只能传递 10 位流水号
            var model = WFDA.Instance.GetModelById(entity["ModelId"]);
            if (model.DFFormName == "Form_XDSW_T_PR")
            {
                instanceId = Pub.GetNextIdFromDB("P", DateTime.Now.ToString("yyMMdd"), string.Empty, 3);
            }
            else if (model.DFFormName == "Form_AD_T_PO")
            {
                instanceId = Pub.GetNextIdFromDB("P", DateTime.Now.ToString("yyyyMMdd"), string.Empty, 4);
            }
            else if (model.DFFormName == "Form_AD_T_BANK_PAYMENT")
            {
                instanceId = Pub.GetNextIdFromDB("F", DateTime.Now.ToString("yyyyMMdd"), string.Empty, 4);
            }
            else if (model.DFFormName == "Form_AD_T_FA")
            {
                instanceId = Pub.GetNextIdFromDB("G", DateTime.Now.ToString("yyyyMMdd"), string.Empty, 4);
            }
            else
            {
                instanceId = Pub.GetNextIdFromDB("A", DateTime.Now.ToString("yyyyMMdd"), string.Empty, 4);
            }

            var Requestor = user.UserId;
            var RequestorName = user.UserName;
            var RequestorProxy = string.Empty;
            var RequestorProxyName = string.Empty;

            // 如果是代理申请，客户端会传上来申请人编号
            if (!string.IsNullOrWhiteSpace(entity["Requestor"]))
            {
                Requestor = entity["Requestor"];
                var requestor = WF_M_USERLoader.Get(Requestor);
                if (requestor == null)
                {
                    throw new Exception(string.Format("根据用户编号 {0} 找不到用户", Requestor));
                }
                RequestorName = requestor.UserName;
                RequestorProxy = user.UserId;
                RequestorProxyName = user.UserName;
            }

            // 开始工作流
            engine.StartDBWF(entity["ModelId"], instanceId, Requestor, RequestorProxy, RequestorName,
                RequestorProxyName, DateTime.Now, true, user.UserId, user.UserName);

            if (!string.IsNullOrWhiteSpace(entity["SourceInstanceId"]))
            {
                // 复制单据
                CloneInstance(entity, instanceId);
            }

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

        private static void CloneInstance(DFDictionary entity, string instanceId)
        {
            var SourceInstanceId = entity["SourceInstanceId"];
            var model = WFDA.Instance.GetModelByInstanceId(instanceId);
            if (model == null)
            {
                throw new WFException(string.Format("根据工作流实例编号 {0} 不能找到对应的工作流模型定义", instanceId));
            }
            var f = DFPub.GetFormM(model.DFFormName);
            var da = NinjectHelper.Get<IDA>(f.DAImp);
            if (da == null)
            {
                throw new WFException(string.Format("根据 {0} 不能创建 IDA 接口", f.DAImp));
            }
            var d = new DFDictionary();
            d.Add("SourceInstanceId", SourceInstanceId);
            d.Add("InstanceId", instanceId);
            var msg = string.Empty;
            (da as BaseDA).CloneData(f, d, ref msg);
        }
    }
}
