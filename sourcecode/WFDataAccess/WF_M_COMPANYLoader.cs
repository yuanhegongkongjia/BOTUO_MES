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
using System.Data;

namespace WFDataAccess
{
    public class WF_M_COMPANYLoader
    {
        public static WF_M_COMPANY Get(string CompanyCode)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from wf_m_company where CompanyCode=@CompanyCode";
                return db.Query<WF_M_COMPANY>(sql,new { CompanyCode=CompanyCode }).FirstOrDefault();
            }
        }
    }
}
