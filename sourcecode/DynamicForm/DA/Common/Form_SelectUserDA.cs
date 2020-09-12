using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_SelectUserDA:BaseDA
    {
        //public override string TableName
        //{
        //    get { return "WF_M_USER"; }
        //}

        //public override string CurrentUserName
        //{
        //    get { return Util.GetCurrentUser().UserName; }
        //}

        //public override object GetParam(DFDictionary entity)
        //{
        //    return new { UserName = entity["UserName"] };
        //}

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from WF_M_USER where 1=1";
            var param = new
            {
                UserName = QueryBuilder.Like(ref sql, entity, "UserName", "UserName")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by UserId", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}