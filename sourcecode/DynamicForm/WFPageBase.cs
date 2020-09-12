using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WFCommon.Utility;
using WFDataAccess;

namespace DynamicForm
{
    public class WFPageBase : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            InitializeHelper.Init();
            //var message = string.Empty;
            //var user = Util.GetCurrentUser().UserId;
            //var formName = string.Format("{0}", Request["DF_FORMNAME"]);
            //if (formName.IndexOf("Form_XDSW_M_LOCK") < 0 && !CheckSystemStatus(user, ref message))
            //{
            //    Response.Write(message);
            //    Response.End();
            //}
            base.OnLoad(e);
        }

        ///// <summary>
        ///// 检查系统状态
        ///// </summary>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //internal static bool CheckSystemStatus(string userId, ref string message)
        //{
        //    var list = XDSW_M_LOCKLoader.Query(new Core.DFDictionary());
        //    var now = DateTime.Now;
        //    var subList = list.Where(a => ParseHelper.ParseDate(a.StartTime) <= now && now <= ParseHelper.ParseDate(a.EndTime) && a.Status == "锁定").ToList();

        //    if (subList.Count > 0)
        //    {
        //        var roles = string.Join(",", subList.Select(a => a.AllowRoleId).ToList()).Split(',').ToList();
        //        if (UserRoleLoader.IsUserInRole(userId, roles))
        //        {
        //            return true;
        //        }
        //        message = "财务结账，系统锁定中！".GetRes();
        //        return false;
        //    }
        //    // 系统可用
        //    return true;
        //}
    }
}