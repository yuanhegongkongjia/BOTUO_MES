using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;
using System.Data;
using System.IO;

namespace DynamicForm.DA
{
    public class Form_CollectDataAnalyzeViewDA:BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select Period,cast(MoreMax10 as int) as MoreMax10,cast(MoreMax30 as int) as MoreMax30,
cast(MoreMax50 as int) as MoreMax50,cast(MoreMax100 as int) as MoreMax100,cast(LessMin10 as int) as LessMin10,cast(LessMin30 as int) as LessMin30,cast(LessMin50 as int) as LessMin50,cast(LessMin100 as int) as LessMin100
from v_sm_collectdata_analyze where 1=1 ";
            var param = new
            {
                MasterDeviceName = QueryBuilder.Equal(ref sql, entity, "MasterDeviceName", "MasterDeviceName"),
                SlaveDeviceName = QueryBuilder.Equal(ref sql, entity, "SlaveDeviceName", "SlaveDeviceName"),
                LineName = QueryBuilder.Equal(ref sql, entity, "LineName", "LineName"),
                Category = QueryBuilder.Equal(ref sql, entity, "Category", "Category"),
                ProductDate = QueryBuilder.Equal(ref sql, entity, "ProductDate", "ProductDate")
            };
            //sql += " group by LineName,MasterDeviceName,SlaveDeviceName,ProductDate,Category";
            var list = GetList(entity, ref count, start, limit, sql, "order by Period", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}