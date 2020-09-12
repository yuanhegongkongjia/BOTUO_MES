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
    public class WFCore_SelectStepDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
            var actionName = DFPub.UrlDecode(entity["actionName"]);
            var action = entity["action"];
            if (string.IsNullOrWhiteSpace(InstanceStepExecutorId))
            {
                throw new WFException("缺少参数 InstanceStepExecutorId");
            }

            //form.GetControlM("NextStep").Text = actionName;
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);
            if (new StateEngineHelper().IsHideNextStep(InstanceStepExecutor.InstanceStepId))
            {
                // 加签不影响流程走向，所以要隐藏下一步
                form.GetControlM("NextStep").Visible = false;
            }
            else
            {
                if (action == "btnSubmit" || action == "btnApprove")
                {
                    var list = new StateEngineHelper().GetSubmitOrApproveSteps(InstanceStepExecutorId);
                    base.SetSelectDataSource(form, "NextStep", list.Select(a => new DFSelectItem() { Text = a.text, Value = a.value }).ToList());
                }
                else if (action == "btnRollback" || action == "btnReject")
                {
                    var list = new StateEngineHelper().GetRejectOrRollbackSteps(InstanceStepExecutorId);
                    base.SetSelectDataSource(form, "NextStep", list.Select(a => new DFSelectItem() { Text = a.text, Value = a.value }).ToList());
                }
            }
            base.SetAccess(form, entity);
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
            var actionName = DFPub.UrlDecode(entity["actionName"]);
            var user = Util.GetCurrentUser();
            var ExecuteComment = entity["ExecuteComment"];
            var ExecuteResult = actionName;
            var ToStepId = entity["NextStep"];
            var CurrentUserId = user.UserId;
            var CurrentUserName = user.UserName;
            new StateEngineHelper().Goto(InstanceStepExecutorId, ExecuteComment, ExecuteResult, ToStepId, CurrentUserId, CurrentUserName);

            base.WriteScript(string.Format("alert('{0}成功，本窗口将自动关闭！');window.top.close();", actionName), ref message);
            return DFPub.EXECUTE_SUCCESS;
        }

    }

}
