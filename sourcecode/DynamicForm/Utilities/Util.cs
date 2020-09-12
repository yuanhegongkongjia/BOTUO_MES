using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Web;
using WFCore;
using System.Security.Cryptography;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using Common.Logging;
using System.Data;
using System.Text.RegularExpressions;
using WFCommon;
using WFCommon.Utility;

namespace DynamicForm
{
    public class Util
    {
        public static bool IsAdmin()
        {
            return GetCurrentUser().UserName == "admin";
        }
        public static WF_M_USER GetCurrentUser()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                if (HttpContext.Current.Request.Cookies[SystemConstants.DF_USER] != null)
                {
                    var DF_USER = HttpContext.Current.Request.Cookies[SystemConstants.DF_USER];
                    SessionHelper.GetSession(DF_USER.Value, SessionType.PCClient);
                    var user = GetUserById(DF_USER.Value);
                    if (user != null)
                    {
                        return user;
                    }
                }
            }
            throw new WFException("请重新登录系统".GetRes());
        }

        private static WF_M_USER GetUserById(string userId)
        {
            using (var db = Pub.DB)
            {
                return db.Query<WF_M_USER>("select * from WF_M_USER where UserId=@UserId and Status=1", new { UserId = userId }).FirstOrDefault();
            }
        }
    }
}