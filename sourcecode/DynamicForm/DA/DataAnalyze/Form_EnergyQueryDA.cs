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
    public class Form_EnergyQueryDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select Line, (case when Category='YANSUAN' then N'盐酸'
                                   when Category='LINSUAN' then N'磷酸'
								   when Category='CHUNJIAN' then N'纯碱'
								   when Category='ZILAISHUI' then N'自来水'
								   when Category='CHUNSHUI' then N'纯水'
								   when Category='ZHENGQI' then N'蒸汽'
								   when Category='TIANRANQI' then N'天然气'
								   when Category='POWER' then N'电'
                                   else   N'其他' end) as Category,CollectTime,CollectValue,Position from SM_T_ENERGY_COLLECT
where 1=1";
            var param = new
            {
                Line = QueryBuilder.Like(ref sql, entity, "Line", "Line"),
                Category = QueryBuilder.Like(ref sql, entity, "Category", "Category"),
               
                CollectTimeFrom = QueryBuilder.DateFrom(ref sql, entity, "CollectTime", "CollectTimeFrom"),
                CollectTimeTo = QueryBuilder.DateTo(ref sql, entity, "CollectTime", "CollectTimeTo"),
            };

            //sql = sql + " group by Category,Line";
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectTime desc", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}