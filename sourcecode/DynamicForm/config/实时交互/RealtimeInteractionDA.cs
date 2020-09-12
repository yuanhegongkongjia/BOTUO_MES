using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using WFCore;
using WFSignalR;

namespace DynamicForm.DA
{
    public class RealtimeInteractionDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var dfTaskId = entity["dfTaskId"];
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dfTaskId))
            {
                var list = WFLog.Query(dfTaskId);
                sb.Append(string.Join("\n", list.Select(a => string.Format("【{0}】{1}", a.CreateTime.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss.fff"), a.Msg))));
            }
            if (TaskHelper.TaskExists(dfTaskId))
            {
                sb.AppendLine(string.Format("当前服务端总任务数 {1}，任务 {0} 还在运行......", dfTaskId, TaskHelper.GetTasksList().Count));
            }
            else
            {
                sb.AppendLine(string.Format("当前服务端总任务数 {1}，任务 {0} 已经结束", dfTaskId, TaskHelper.GetTasksList().Count));
            }
            this.Model.Add("Msg", sb.ToString());
            base.SetAccess(form, entity);
        }


        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            var dfTaskId = entity["dfTaskId"];
            TaskHelper.AbortTask(dfTaskId);
            SendMsg(dfTaskId, string.Format("任务 {0} 成功结束", dfTaskId));
            return DFPub.EXECUTE_SUCCESS;
        }


        /// <summary>
        /// 添加后台任务，并且通过消息通知用户
        /// </summary>
        /// <param name="dfTaskId"></param>
        /// <param name="actionDesc"></param>
        /// <param name="action"></param>
        /// <param name="receiveUserId"></param>
        /// <param name="currentUser"></param>
        public static string AddTask(string dfTaskId, string actionDesc, Action action, string receiveUserId, string currentUser)
        {
            TaskHelper.StartNew(string.Empty, dfTaskId, () =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    SendMsg(dfTaskId, ex.Message);
                }
            }, null);
            var msgLink = string.Format("DFIndex.aspx?DF_FORMNAME=Sample_realtime_interaction&dfTaskId={0}", dfTaskId);
            MsgCenter.SendMsg(receiveUserId, actionDesc, msgLink, null, dfTaskId, currentUser, "NewTask");
            return msgLink;
        }


        /// <summary>
        /// 通过 SignalR 发送消息给客户端
        /// </summary>
        /// <param name="dfTaskId"></param>
        /// <param name="message"></param>
        public static void SendMsg(string dfTaskId, string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            if (string.IsNullOrWhiteSpace(dfTaskId))
                return;
            WFLog.DebugFormat(dfTaskId, message);
            ChatHubProxy.Instance.Value.SendChatMessage(dfTaskId, string.Format("【{0}】{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message));
        }
    }
}