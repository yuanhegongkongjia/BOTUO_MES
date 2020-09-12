using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.Utility;
using System.Text;

namespace WFDataAccess
{
    public class SM_T_ENVIRONMENTLoader
    {
        public static string GetEnvironment()
        {
            using (var db = Pub.DB)
            {
                var sql = @"select e.* from SM_T_ENVIRONMENT e
right join (
select ParamType,MAX(CollectTime) as CollectTime  from SM_T_ENVIRONMENT
where ParamType in ('TEMP','HUMI','AIR')
group by ParamType) a
on e.ParamType=a.ParamType and e.CollectTime=a.CollectTime";


                var list = db.Query<SM_T_ENVIRONMENT>(sql).ToList();
                var str = "";
                var temp = list.Where(a => a.ParamType == "TEMP").FirstOrDefault();
                var humi = list.Where(a => a.ParamType == "HUMI").FirstOrDefault();
                var air = list.Where(a => a.ParamType == "AIR").FirstOrDefault();
                if (temp != null)
                {
                    str = str + string.Format("温度："+temp.ParamValue+"℃ ");
                }
                if (humi != null)
                {
                    str = str + string.Format("湿度：" + humi.ParamValue + "% ");
                }
                if (air != null)
                {
                    str = str + string.Format("PM2.5：" + air.ParamValue );
                }
                return str;
            }
        }
    }
}
