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
    public class WF_T_MSGLoader
    {
        public static void Insert(WF_T_MSG item)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_T_MSG>(item);
            }
        }
    }
}
