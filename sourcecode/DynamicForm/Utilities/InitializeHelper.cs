using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using WFCore;
using System.Threading;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using DynamicForm.Core;

namespace DynamicForm
{
    public class InitializeHelper
    {
        static InitializeHelper()
        {
            NinjectHelper.Bind<IExecutor, ExecutorImp>();
            NinjectHelper.Bind<IEngine, Engine>();
        }
        static bool isInitialized = false;
        public static void Init()
        {
            if (!isInitialized)
            {
                var path = HttpRuntime.AppDomainAppPath;
                var di = new DirectoryInfo(path);
                var configDirectories = di.GetDirectories("config", SearchOption.AllDirectories);
                if (configDirectories.Length > 0)
                {
                    DFPub.ConfigFolder = configDirectories[0].FullName;
                    var list = DFPub.CheckFormConfig(DFPub.ConfigFolder);
                    if (list.Count > 0)
                    {
                        throw new Exception(string.Join(Environment.NewLine, list));
                    }
                }
                else
                {
                    throw new Exception("不能找到表单页面配置的 config 目录".GetRes());
                }
                Pub.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                isInitialized = true;
                WFRes.Instance.Load();
            }
        }
    }
}