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
    /// <summary>
    /// 公用代码字典类
    /// </summary>
    public class WF_M_PUBLICCODELoader
    {
        public static List<WF_M_PUBLICCODE> Query(string CodeType)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_PUBLICCODE where 1=1";
                if (!string.IsNullOrWhiteSpace(CodeType))
                {
                    sql += " and CodeType=@CodeType";
                }
                sql += " order by CodeType,CodeOrder";
                return db.Query<WF_M_PUBLICCODE>(sql, new
                {
                    CodeType = CodeType,
                }).ToList();
            }
        }
    }
}
