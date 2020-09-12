using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFCommon.Utility;

namespace DynamicForm
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var user = Util.GetCurrentUser();
            }
            catch
            {
                this.Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(this.Request.Url.ToString()));
            }
            this.userName.InnerHtml = GetWelcome();
        }

        public string GetWelcome()
        {
            var user = Util.GetCurrentUser();
            var display = user.UserName;
            if (!string.IsNullOrWhiteSpace(user.UserId))
            {
                //display = string.Format("{0}", user.UserName);
                display = user.UserName;
            }
            return string.Format("{0}{1}{2}".GetRes(), "欢迎您！".GetRes(), "<br />", display);
        }
    }
}