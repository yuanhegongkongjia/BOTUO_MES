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
    public class Form_ProcessDataDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from sm_t_process_collect
where 1=1";
            var param = new
            {
                Line = QueryBuilder.Like(ref sql, entity, "Line", "Line"),
                Position = QueryBuilder.Like(ref sql, entity, "Position", "Position"),
                ParamName = QueryBuilder.Like(ref sql, entity, "ParamName", "ParamName"),
                CollectTimeFrom = QueryBuilder.DateTimeFrom(ref sql, entity, "CollectTime", "CollectTimeFrom"),
                CollectTimeTo = QueryBuilder.DateTimeTo(ref sql, entity, "CollectTime", "CollectTimeTo"),
            };

            //sql = sql + " group by Category,Line";
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectTime desc", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}