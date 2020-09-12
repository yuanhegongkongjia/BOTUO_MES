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
    public class WFCore_Panel_ApproveDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var page = ((this.Parent as ucForm).Page as DFIndexWF);
            var list = InstanceStepExecutorDAO.QueryUnfinishedByInstanceId(entity["InstanceId"]);
            if (list.Count == 0)
            {
                form.GetControlM("btnRemind").Remove();
            }
            // 1 保存 2 提交 3 同意 4 不同意 5 回退 6 转签 7 加签
            var result = GetAccess(form, entity);
            if ((result & 1) <= 0)
            {
                form.GetControlM("btnSave").Remove();
                if (page != null)
                {
                    page.UcForm1.Form.GetControlMs_Like("btnEdit").ForEach(a => a.Remove());
                    page.UcForm1.Form.GetControlMs_Like("btnUpdate").ForEach(a => a.Remove());
                    page.UcForm1.Form.GetControlMs_Like("btnDelete").ForEach(a => a.Remove());
                    page.UcForm1.Form.GetControlMs_Like("btnAdd").ForEach(a => a.Remove());
                    page.UcForm1.Form.GetControlMs_Like("btnUpload").ForEach(a => a.Remove());
                }
            }
            if ((result & 2) <= 0)
            {
                form.GetControlM("btnSubmit").Remove();
            }
            if ((result & 4) <= 0)
            {
                form.GetControlM("btnApprove").Remove();
            }
            if ((result & 8) <= 0)
            {
                form.GetControlM("btnReject").Remove();
            }
            if ((result & 16) <= 0)
            {
                form.GetControlM("btnRollback").Remove();
            }
            if ((result & 32) <= 0)
            {
                form.GetControlM("btnTransferApprove").Remove();
            }
            if ((result & 64) <= 0)
            {
                form.GetControlM("btnAdditionalApprove").Remove();
            }
            base.SetAccess(form, entity);
        }

        public override int Insert(DynamicForm.Core.FormM form, DynamicForm.Core.DFDictionary entity, ref string message)
        {
            Submit(entity, ref message);
            return base.Insert(form, entity, ref message);
        }

        public DataGridVM Remind(DFDictionary dict)
        {
            ArgumentCheck.CheckMustInput(dict, "InstanceId");
            var user = Util.GetCurrentUser();
            var list = InstanceStepExecutorDAO.QueryUnfinishedByInstanceId(dict["InstanceId"]);
            foreach (var item in list)
            {
                ExecutorImp.SendMessage(item.InstanceStepExecutorId, user.UserName);
            }
            return new DataGridVM() { data = "催签成功！".GetRes() };
        }

        private void Submit(DynamicForm.Core.DFDictionary entity, ref string message)
        {
            var user = Util.GetCurrentUser();
            var page = ((this.Parent as ucForm).Page as DFIndexWF);
            // 如果是提交按钮，业务 ucForm1 保存成功的话，就可以进行提交
            if (base.GetSubmitButton(entity) == "btnSubmit" && page.UcForm1.DA.ExecuteResult == DFPub.EXECUTE_SUCCESS)
            {
                var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
                if (!string.IsNullOrWhiteSpace(InstanceStepExecutorId))
                {
                    new StateEngineHelper().AutoFlow(InstanceStepExecutorId, "提交".GetRes(), user.UserId, user.UserName);
                    base.WriteScript(string.Format("alert('提交成功，本窗口将自动关闭！');window.top.close();"), ref message);
                }
            }
        }


        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            Submit(entity, ref message);
            return base.Update(form, entity, ref message);
        }

        private static int GetAccess(FormM form, DFDictionary entity)
        {
            var InstanceStepExecutorId = entity["InstanceStepExecutorId"];
            if (string.IsNullOrWhiteSpace(InstanceStepExecutorId))
            {
                return 0;
            }
            var result = 0;
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);

            if (null != InstanceStepExecutor)
            {
                if (InstanceStepExecutor.ExecuteStatus == Pub.Finished)
                {
                    var user = Util.GetCurrentUser();
                    // 判断是否是流程管理员
                    if (WFDA.Instance.IsInstanceStepRunning(InstanceStepExecutorId) && WFDA.Instance.IsWorkflowAdmin(user.UserName, InstanceStepExecutor.ModelName))
                    {
                        // 允许加签转签功能
                        result = result | 32;
                        result = result | 64;
                    }
                    return result;
                }
                var Step = WFDA.Instance.GetStep(InstanceStepExecutor.StepId);
                if (null != Step && !string.IsNullOrWhiteSpace(Step.AllowActions))
                {
                    // 1 保存 2 提交 3 同意 4 不同意 5 回退 6 转签 7 加签
                    var list = Step.AllowActions.Split(',');
                    if (list.Contains("1"))
                    {
                        result = result | 1;
                    }
                    if (list.Contains("2"))
                    {
                        result = result | 2;
                    }
                    if (list.Contains("3"))
                    {
                        result = result | 4;
                    }
                    if (list.Contains("4"))
                    {
                        result = result | 8;
                    }
                    if (list.Contains("5"))
                    {
                        result = result | 16;
                    }
                    if (list.Contains("6"))
                    {
                        result = result | 32;
                    }
                    if (list.Contains("7"))
                    {
                        result = result | 64;
                    }
                }
            }
            return result;
        }
    }

}
