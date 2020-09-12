using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using System.Runtime.InteropServices;
using WFDataAccess;
using DynamicForm.Core;

namespace DynamicForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.btnLogin.ServerClick += btnLogin_ServerClick;
            base.OnInit(e);
        }

        [DllImport("advapi32.dll")]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        void btnLogin_ServerClick(object sender, EventArgs e)
        {

            var userName = this.user.Value;
            var pwd = this.password.Value;

            // 先检查用户名在本系统里面有没有
            var user = UserLoader.GetUserByName(userName);
            if (user == null)
            {
                ClientMessageHelper.Alert(Page, "用户名不存在".GetRes());
                WFLog.WriteLog("系统登录".GetRes(), userName, string.Format("用户名不存在，客户端IP地址 {0}".GetRes(), IPHelper.GetClientIPAddress()), WFLog.LOGLEVEL_ERROR, userName);
                return;
            }
            if (user.Status != 1)
            {
                ClientMessageHelper.Alert(Page, "用户已经被禁用".GetRes());
                WFLog.WriteLog("系统登录".GetRes(), userName, string.Format("用户已经被禁用，客户端IP地址 {0}".GetRes(), IPHelper.GetClientIPAddress()), WFLog.LOGLEVEL_WARN, userName);
                return;
            }

            // 先本机验证，如果本机验证不通过，进行域验证
            //
            // 发布的时候需要设置成 Release 模式进行发布
            //
            if (!Verify(userName, pwd))
            {
                ClientMessageHelper.Alert(Page, "用户名密码错误".GetRes());
                WFLog.WriteLog("系统登录".GetRes(), userName, string.Format("用户名密码错误，客户端IP地址 {0}".GetRes(), IPHelper.GetClientIPAddress()), WFLog.LOGLEVEL_ERROR, userName);
                return;
            }
            WFLog.WriteLog("系统登录".GetRes(), userName, string.Format("用户成功登录，客户端IP地址 {0}".GetRes(), IPHelper.GetClientIPAddress()), WFLog.LOGLEVEL_INFO, userName);
            InitUserSession(user.UserId, dpLanguage.SelectedValue.ToString());
            Response.Redirect(GetReturnUrl());
        }

        public static bool Verify(string userName, string password)
        {
            // 默认两种验证方式
            var domain = string.Format("{0}", WFCorePublicCodeHelper.GetPublicCode("Domain", "Authentication Method", "Domain,Password")).ToLower();

            if (domain.Contains("password"))
            {
                var user = UserLoader.GetUserByName(userName);
                if (user != null)
                {
                    var uesrHash = HashHelper.GenerateUserHash(userName, password);
                    if (uesrHash == user.Password)
                    {
                        return true;
                    }
                }
            }
            if (domain.Contains("domain"))
            {
                if (IsValidDomainUser(userName, password))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetReturnUrl()
        {
            var returnUrl = this.Request.QueryString["returnUrl"];
            if (returnUrl != null)
            {
                return returnUrl.ToString();
            }
            else
            {
                return "Index.aspx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var language = GetLanguageFromCookie();
                dpLanguage.SelectedValue = language;
                this.Title = "用户登录".GetRes(language);
                this.btnLogin.Value = "登录".GetRes(language);
            }
        }

        private void InitUserSession(string userId, string language)
        {
            var session = SessionHelper.RefreshUserToken(userId, "PCClient");
            // 更新用户登录最后时间

            /// <summary>
            /// 设置 Cookie
            /// </summary>
            /// <param name="session"></param>
            /// <param name="language"></param>

            var expiredTime = session.Expire ?? DateTime.Now.AddHours(4);
            SetLanguageCookie(language, expiredTime);

            // 更新用户登录最后时间
            UserLoader.Update(userId, DateTime.Now);
        }

        /// <summary>
        /// 从 Cookie 中得到语言
        /// </summary>
        /// <returns></returns>
        private string GetLanguageFromCookie()
        {
            var language = string.Empty;
            if (null != Request.Cookies[SystemConstants.DF_LANG]
                && null != Request.Cookies[SystemConstants.DF_LANG].Value)
            {
                language = Request.Cookies[SystemConstants.DF_LANG].Value;
            }
            if (language != SystemConstants.English
                && language != SystemConstants.ChineseSimplified
                && language != SystemConstants.ChineseTraditional)
            {
                language = SystemConstants.English;
            }
            return language;
        }

        private void SetLanguageCookie(string language, DateTime expires)
        {
            this.Response.Cookies.Remove(SystemConstants.DF_LANG);
            var ckLang = new HttpCookie(SystemConstants.DF_LANG, language);
            ckLang.Expires = expires;
            this.Response.Cookies.Add(ckLang);

            this.Session[SystemConstants.DF_LANG] = language;
        }


        protected void dpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = dpLanguage.SelectedValue;
            SetLanguageCookie(language, DateTime.Now.AddHours(4));
            this.Title = "用户登录".GetRes(language);
            this.btnLogin.Value = "登录".GetRes(language);
        }

        /// <summary>
        /// 域验证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool IsValidDomainUser(string user, string pwd)
        {
            const int LOGON32_LOGON_INTERACTIVE = 2; //通过网络验证账户合法性
            const int LOGON32_PROVIDER_DEFAULT = 0;  //使用默认的Windows 2000/NT NTLM验证方

            IntPtr tokenHandle = new IntPtr(0);
            tokenHandle = IntPtr.Zero;


            string domainName = WFCorePublicCodeHelper.GetPublicCode("Domain", "Domain", string.Empty);
            string domainAccount = user; //域帐号 如:administrator
            string domainPassword = pwd; //密码

            bool checkok = LogonUser(domainAccount, domainName, domainPassword, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
            return checkok;
        }

    }
}