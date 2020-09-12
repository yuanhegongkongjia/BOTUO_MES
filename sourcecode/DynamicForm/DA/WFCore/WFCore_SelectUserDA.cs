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
using WFCommon.VM;

namespace DynamicForm.DA
{
    public class WFCore_SelectUserDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            CheckInput(form, entity);

            var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
            var actionName = DFPub.UrlDecode(entity["actionName"]);
            var user = Util.GetCurrentUser();
            var ExecuteComment = entity["ExecuteComment"];
            var ExecuteResult = actionName;
            var list = base.GetGridClientData<VM_WF_M_USER>(entity);
            new StateEngineHelper().TransferOrAdditional(InstanceStepExecutorId, list, ExecuteComment, ExecuteResult, user.UserName);
            base.WriteScript(string.Format("alert('{0}成功，本窗口将自动关闭！');window.top.close();".GetRes(), actionName), ref message);
            return DFPub.EXECUTE_SUCCESS;
        }

        public override void CheckInput(FormM form, DFDictionary entity)
        {
            var list = base.GetGridClientData<VM_WF_M_USER>(entity);
            if (list == null || list.Count == 0)
            {
                throw new WFException("请添加用户".GetRes());
            }
            base.CheckInput(form, entity);
        }
    }

}
