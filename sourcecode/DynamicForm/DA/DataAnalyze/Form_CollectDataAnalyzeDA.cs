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
namespace DynamicForm.DA
{
    public class Form_CollectDataAnalyzeDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select LineName,MasterDeviceName,SlaveDeviceName,ProductDate,Category,sum(cast(MoreMax10 as int)) as MoreMax10,sum(cast(MoreMax30 as int)) as MoreMax30,
sum(cast(MoreMax50 as int)) as MoreMax50,sum(cast(MoreMax100 as int)) as MoreMax100,sum(cast(LessMin10 as int)) as LessMin10,sum(cast(LessMin30 as int)) as LessMin30,sum(cast(LessMin50 as int)) as LessMin50,Sum(cast(LessMin100 as int)) as LessMin100
from v_sm_collectdata_analyze where 1=1";
            var param = new
            {
                MasterDeviceName = QueryBuilder.Like(ref sql, entity, "MasterDeviceName", "MasterDeviceName"),
                SlaveDeviceName = QueryBuilder.Like(ref sql, entity, "SlaveDeviceName", "SlaveDeviceName"),
                LineName = QueryBuilder.Like(ref sql, entity, "LineName", "LineName"),
                Category = QueryBuilder.Like(ref sql, entity, "Category", "Category"),
                ProductDateFrom = QueryBuilder.DateFrom(ref sql, entity, "ProductDate", "ProductDateFrom"),
                ProductDateTo = QueryBuilder.DateFrom(ref sql, entity, "ProductDate", "ProductDateTo")
            };
            sql += " group by LineName,MasterDeviceName,SlaveDeviceName,ProductDate,Category";
            var list = GetList(entity, ref count, start, limit, sql, "order by MasterDeviceName", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}