using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon.VM;
using WFCore;
using Dapper;
using DapperExtensions;
using WFCommon.Utility;
using WFDataAccess;
using WFCommon;

namespace DynamicForm
{
    public class StateEngineHelper
    {
        public const string LogFileName = "StateEngineHelper";
        /// <summary>
        /// 尝试某个运行时节点的下面的步骤
        /// </summary>
        /// <param name="InstanceStepId"></param>
        /// <returns></returns>
        private List<WFItem> TryGetNextSteps(string InstanceStepExecutorId)
        {
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);
            var InstanceStep = WFDA.Instance.GetInstanceStep(InstanceStepExecutor.InstanceStepId);
            var instance = WFDA.Instance.GetInstance(InstanceStep.InstanceId);

            var data1 = WFBusinessData.AddPrefix("WF_T_INSTANCE_", WFBusinessData.CreateInstance<WF_T_INSTANCE>(instance));
            var data2 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEP_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEP>(InstanceStep));
            var data3 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEPEXECUTOR_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEPEXECUTOR>(InstanceStepExecutor));
            WFBusinessData.Merge(data2, data1);
            WFBusinessData.Merge(data3, data1);
            var businessData = data1;

            var Step = WFDA.Instance.GetStep(InstanceStep.StepId);
            var sql = Pub.GetOriginalSql(Step.Script);

            return GetWFItems(instance, businessData, ref sql);
        }

        /// <summary>
        /// 从某个运行时步骤到另外一个步骤
        /// </summary>
        /// <param name="InstanceStepExecutorId"></param>
        /// <param name="ToStepId"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurrentUserName"></param>
        private void GotoStep(string InstanceStepExecutorId, string ToStepId, string CurrentUserId, string CurrentUserName)
        {
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);
            var ToStep = WFDA.Instance.GetStep(ToStepId);

            if (ToStep.StepId == InstanceStepExecutor.StepId)
            {
                throw new WFException("当前步骤和下一步骤不能相同".GetRes());
            }

            var engine = NinjectHelper.Get<IEngine>();
            if (engine == null)
            {
                throw new WFException("找不到 IEngine".GetRes());
            }

            // 更新步骤签核信息
            InstanceStepExecutorDAO.UpdateUnfinishedByInstanceStepId(InstanceStepExecutor.InstanceStepId, "自动完成", Pub.Finished, CurrentUserName);

            var rollback = new List<string>() { "回退", "不同意", "拒绝" };
            if (rollback.Contains(InstanceStepExecutor.ExecuteResult))
            {
                engine.RollbackDBWF(ToStep.ModelId, InstanceStepExecutor.InstanceId, InstanceStepExecutor.InstanceStepId, ToStep.StepId, true, CurrentUserId, CurrentUserName);
            }
            else
            {
                Extend02(CurrentUserId, CurrentUserName, InstanceStepExecutor, engine);
                // 继续工作流
                engine.GotoDBWF(ToStep.ModelId, InstanceStepExecutor.InstanceId, InstanceStepExecutor.InstanceStepId, ToStep.StepId, true, CurrentUserId, CurrentUserName);
            }
        }

        private static void Extend02(string CurrentUserId, string CurrentUserName, WF_T_INSTANCESTEPEXECUTOR InstanceStepExecutor, IEngine engine)
        {
            try
            {
                // 附加执行
                var step = WFDA.Instance.GetStep(InstanceStepExecutor.StepId);
                if (!string.IsNullOrWhiteSpace(step.Extend02))
                {
                    var business = NinjectHelper.Get<IWFBusiness>(step.Extend02);
                    if (business != null)
                    {
                        var context = engine.GetContext(InstanceStepExecutor.InstanceId, CurrentUserId, CurrentUserName);
                        var instanceStep = WFDA.Instance.GetInstanceStep(InstanceStepExecutor.InstanceStepId);
                        context.InstanceStep = instanceStep;
                        context.IsRollback = false;
                        business.Leave(context);
                    }
                }
            }
            catch (Exception ex)
            {
                WFLog.ErrorFormat(LogFileName, ex.Message, ex);
            }
        }

        private void SetFinished(string InstanceStepExecutorId, string ExecuteResult, string CurrentUserName)
        {
            InstanceStepExecutorDAO.UpdateInstanceStepExecutor(InstanceStepExecutorId, ExecuteResult, null, null, Pub.Finished, CurrentUserName);
        }

        private void SetFinished(string InstanceStepExecutorId, string ExecuteResult, string ExecuteComment, string CurrentUserName)
        {
            InstanceStepExecutorDAO.UpdateInstanceStepExecutor(InstanceStepExecutorId, ExecuteResult, ExecuteComment, null, Pub.Finished, CurrentUserName);
        }

        private void SetFinished(string InstanceStepExecutorId, string ExecuteResult, string ExecuteComment, string memo, string CurrentUserName)
        {
            InstanceStepExecutorDAO.UpdateInstanceStepExecutor(InstanceStepExecutorId, ExecuteResult, ExecuteComment, memo, Pub.Finished, CurrentUserName);
        }

        /// <summary>
        /// 自动流转
        /// </summary>
        /// <param name="InstanceStepExecutorId"></param>
        /// <param name="ExecuteResult"></param>
        /// <param name="CurrentUserId"></param>
        /// <param name="CurrentUserName"></param>
        public void AutoFlow(string InstanceStepExecutorId, string ExecuteResult, string CurrentUserId, string CurrentUserName)
        {
            var nextSteps = TryGetNextSteps(InstanceStepExecutorId);
            if (nextSteps.Count == 1)
            {
                // 更新当前任务已经完成
                SetFinished(InstanceStepExecutorId, ExecuteResult, CurrentUserName);

                // 接下去就一个节点，就直接流转
                GotoStep(InstanceStepExecutorId, nextSteps.First().value, CurrentUserId, CurrentUserName);
            }
            else
            {
                /// TODO: 暂时先报错处理
                throw new NotSupportedException();
            }
        }


        public void Goto(string InstanceStepExecutorId, string ExecuteComment, string ExecuteResult, string ToStepId, string CurrentUserId, string CurrentUserName)
        {
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);
            if (InstanceStepExecutor.ExecuteStatus == Pub.Finished)
            {
                throw new WFException("该签核任务已经完成");
            }
            // 更新当前任务已经完成
            SetFinished(InstanceStepExecutorId, ExecuteResult, ExecuteComment, CurrentUserName);

            BackToAdditionalSource(InstanceStepExecutor.InstanceStepId, CurrentUserName);
            if (!string.IsNullOrWhiteSpace(ToStepId))
            {
                GotoStep(InstanceStepExecutorId, ToStepId, CurrentUserId, CurrentUserName);
            }
        }

        /// <summary>
        /// 更新来源，方便在加签的用户操作后通知原来的用户
        /// </summary>
        /// <param name="listNewExecutors"></param>
        /// <param name="Extend01"></param>
        /// <param name="Extend02"></param>
        /// <param name="user"></param>
        private void SaveAdditionalApproveInfo(List<string> listNewExecutors, string Extend01, string Extend02, string user)
        {
            foreach (var item in listNewExecutors)
            {
                InstanceStepExecutorDAO.UpdateInstanceStepExecutorExt(item, "Extend01", Extend01, user);
                InstanceStepExecutorDAO.UpdateInstanceStepExecutorExt(item, "Extend02", Extend02, user);
            }
        }

        /// <summary>
        /// 是否隐藏下一步
        /// 不管是加签还是转签，加签的用户和转签的用户都要签核完成
        /// </summary>
        /// <param name="InstanceStepId"></param>
        /// <returns></returns>
        public bool IsHideNextStep(string InstanceStepId)
        {
            var extend04 = WFDA.Instance.GetInstanceStep(InstanceStepId).Extend04;
            if (!string.IsNullOrWhiteSpace(extend04))
            {
                // 需要返回到加签的来源人，需要隐藏下一步
                return true;
            }
            var list = InstanceStepExecutorDAO.QueryByExecuteStatus(InstanceStepId, Pub.Unfinished);
            list.ForEach(a => a.Extend01 = string.Format("{0}", a.Extend01));
            if (list.Count(a => a.Extend01.Contains("签")) >= 2)
            {
                return true;
            }
            else
            {
                // 默认显示选择下一步的下拉框
                return false;
            }
        }

        /// <summary>
        /// 如果有加签人,就返回到加签人,如果没有加签人,直接忽略
        /// </summary>
        /// <param name="InstanceStepId"></param>
        /// <param name="currentUser"></param>
        private void BackToAdditionalSource(string InstanceStepId, string currentUser)
        {
            var instanceStep = WFDA.Instance.GetInstanceStep(InstanceStepId);
            if (!string.IsNullOrWhiteSpace(instanceStep.Extend04))
            {
                var item = InstanceStepExecutorDAO.Get(instanceStep.Extend04);
                if (item != null)
                {
                    var list = InstanceStepExecutorDAO.QueryByExecuteStatus(InstanceStepId, Pub.Unfinished);
                    list.ForEach(a => a.Extend01 = string.Format("{0}", a.Extend01));
                    if (!list.Any(a => a.Extend01.Contains("签")))
                    {
                        // 转签加签都完成了
                        ExecutorImp.InsertOneAndProxy(InstanceStepId, item.ExecutorId, item.ExecutorName, string.Empty, currentUser, true);
                        // 需要返回到加签的来源人
                        WFDA.Instance.UpdateInstanceStepExt(InstanceStepId, "Extend04", null, currentUser);
                    }
                }
            }
        }

        public List<WFItem> GetSubmitOrApproveSteps(string InstanceStepExecutorId)
        {
            WFInstance Instance;
            WFInstanceStep InstanceStep;
            WF_M_STEP Step;
            WF_T_INSTANCESTEPEXECUTOR InstanceStepExecutor;
            InstanceStepExecutorDAO.GetAllInfo(InstanceStepExecutorId,
                out InstanceStepExecutor, out InstanceStep, out Instance, out Step);

            if (IsHideNextStep(InstanceStepExecutor.InstanceStepId))
            {
                return new List<WFItem>();
            }

            // 得到 sql 语句
            var instance = WFDA.Instance.GetInstance(InstanceStep.InstanceId);
            var data1 = WFBusinessData.AddPrefix("WF_T_INSTANCE_", WFBusinessData.CreateInstance<WF_T_INSTANCE>(instance));
            var data2 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEP_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEP>(InstanceStep));
            var data3 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEPEXECUTOR_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEPEXECUTOR>(InstanceStepExecutor));
            WFBusinessData.Merge(data2, data1);
            WFBusinessData.Merge(data3, data1);
            var businessData = data1;

            var sql = Pub.GetOriginalSql(Step.Script);

            return GetWFItems(instance, businessData, ref sql);
        }

        private static List<WFItem> GetWFItems(WFInstance instance, WFBusinessData businessData, ref string sql)
        {
            List<WFItem> list = new List<WFItem>();
            if (!string.IsNullOrWhiteSpace(sql))
            {
                if (sql.IndexOf("WF_M_STEP") >= 0 && sql.IndexOf("WF_M_MODEL") >= 0)
                {
                    using (var db = Pub.DB)
                    {
                        list = db.Query<VM_WF_M_STEP>(sql, Pub.ToDynamic(businessData))
                        .Select(a => new WFItem() { text = a.StepName, value = a.StepId }).ToList();
                    }
                }
                else
                {
                    using (var db = Pub.DB)
                    {
                        var StepNames = db.Query<string>(sql, Pub.ToDynamic(businessData)).ToList();
                        if (StepNames.Count == 0)
                        {
                            StepNames.Add("-1");
                        }
                        sql = "SELECT a.StepId,a.StepName FROM dbo.WF_M_STEP a, dbo.WF_M_MODEL b WHERE a.ModelId=b.ModelId AND b.ModelName=@ModelName AND a.StepName IN @StepNames";

                        list = db.Query<VM_WF_M_STEP>(sql, new
                        {
                            ModelName = instance.ModelName,
                            StepNames = StepNames
                        })
                        .Select(a => new WFItem() { text = a.StepName, value = a.StepId }).ToList();
                    }

                }
            }
            return list;
        }

        public List<WFItem> GetRejectOrRollbackSteps(string InstanceStepExecutorId)
        {
            WFInstance Instance;
            WFInstanceStep InstanceStep;
            WF_M_STEP Step;
            WF_T_INSTANCESTEPEXECUTOR InstanceStepExecutor;
            InstanceStepExecutorDAO.GetAllInfo(InstanceStepExecutorId,
                out InstanceStepExecutor, out InstanceStep, out Instance, out Step);

            if (IsHideNextStep(InstanceStepExecutor.InstanceStepId))
            {
                return new List<WFItem>();
            }

            // 查找之前走过的节点
            var listFromStepIds = new List<string>();
            var InstanceConnectors = WFDA.Instance.GetInstanceConnectorByTo(InstanceStep.InstanceId, InstanceStep.InstanceStepId);
            // 根据节点运行时连线记录，“到节点”查找所有的之前走过的节点
            GetFromSteps(listFromStepIds, InstanceConnectors);

            List<WFItem> list = new List<WFItem>();
            using (var db = Pub.DB)
            {
                list = WFDA.Instance.GetSteps(Step.ModelId).Where(a => listFromStepIds.Contains(a.StepId) && a.StepType != "Start" && a.StepId != InstanceStepExecutor.StepId)
                    .Select(a => new WFItem() { text = a.StepName, value = a.StepId }).ToList();
            }
            return list;
        }

        private void GetFromSteps(List<string> list, List<WFInstanceConnector> InstanceConnectors)
        {
            foreach (var item in InstanceConnectors)
            {
                if (!list.Contains(item.FromStepId))
                {
                    list.Add(item.FromStepId);
                }
                var _InstanceConnectors = WFDA.Instance.GetInstanceConnectorByTo(item.InstanceId, item.InstanceStepFrom);
                GetFromSteps(list, _InstanceConnectors);
            }
        }


        /// <summary>
        /// 继续执行工作流
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="user"></param>
        /// <param name="ExecuteResult"></param>
        /// <param name="ExecuteComment"></param>
        /// <param name="Memo"></param>
        public void LabData_ContinueDBWF(string InstanceStepExecutorId, string ToStepId, string ExecuteResult, string ExecuteComment, string Memo,
            string currentUserId, string currentUserName, bool continueWF)
        {
            var InstanceStepExecutor = InstanceStepExecutorDAO.Get(InstanceStepExecutorId);
            var InstanceStep = WFDA.Instance.GetInstanceStep(InstanceStepExecutor.InstanceStepId);
            var Instance = WFDA.Instance.GetInstance(InstanceStepExecutor.InstanceId);

            // 更新 InstanceStepExecutor 的 Extend01 字段，让工作流进行流转
            var toStep = WFDA.Instance.GetStep(ToStepId);
            SetFinished(InstanceStepExecutorId, ExecuteResult, ExecuteComment, Memo, currentUserName);
            WFDA.Instance.UpdateInstanceStepExt(InstanceStep.InstanceStepId, "Extend01", toStep.StepId, currentUserName);
            WFDA.Instance.UpdateInstanceStepExt(InstanceStep.InstanceStepId, "Extend02", toStep.StepName, currentUserName);

            if (continueWF)
            {
                var engine = NinjectHelper.Get<IEngine>();
                if (engine == null)
                {
                    throw new Exception("找不到 IEngine".GetRes());
                }
                // 继续工作流
                engine.ContinueDBWF(Instance.ModelId, Instance.InstanceId, InstanceStep.InstanceStepId, true, currentUserId, currentUserName);
            }
        }

        /// <summary>
        /// 转签或者加签
        /// </summary>
        /// <param name="InstanceStepExecutorId"></param>
        /// <param name="list"></param>
        /// <param name="ExecuteComment"></param>
        /// <param name="ExecuteResult"></param>
        /// <param name="p"></param>
        public void TransferOrAdditional(string InstanceStepExecutorId, List<VM_WF_M_USER> list, string ExecuteComment, string ExecuteResult, string currentUser)
        {
            WFInstance Instance;
            WFInstanceStep InstanceStep;
            WF_M_STEP Step;
            WF_T_INSTANCESTEPEXECUTOR InstanceStepExecutor;
            InstanceStepExecutorDAO.GetAllInfo(InstanceStepExecutorId,
                out InstanceStepExecutor, out InstanceStep, out Instance, out Step);

            ExecuteComment = string.Format("{0}\r\n{1}", ExecuteComment, InstanceStepExecutor.ExecuteComment);
            var engine = NinjectHelper.Get<IEngine>();
            if (engine == null)
            {
                throw new WFException("找不到 IEngine".GetRes());
            }

            var memo = string.Format("{0}{1}给{2}", currentUser, ExecuteResult, string.Join(",", list.Select(a => a.UserName)));

            // 加签或者转签
            for (int i = 0; i < list.Count; i++)
            {
                var user = list[i];
                var listInstanceStepExecutorIds = ExecutorImp.InsertOneAndProxy(InstanceStep.InstanceStepId, user.UserId, user.UserName
                    , memo, currentUser, true);
                SaveAdditionalApproveInfo(listInstanceStepExecutorIds, ExecuteResult, InstanceStepExecutor.InstanceStepExecutorId, user.UserName);
            }
            if (ExecuteResult == "加签" && string.IsNullOrWhiteSpace(InstanceStep.Extend04))
            {
                WFDA.Instance.UpdateInstanceStepExt(InstanceStep.InstanceStepId, "Extend04", InstanceStepExecutorId, currentUser);
            }
            // 结束当前我自己的任务
            SetFinished(InstanceStepExecutorId, ExecuteResult, ExecuteComment, memo, currentUser);
        }
    }
}