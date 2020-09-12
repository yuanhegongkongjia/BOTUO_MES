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
    public class Form_EnergyQueryOnly4DA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            return QueryByDay(form, entity, vm, start, limit, ref message);
        }

        public int QueryByDay(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {

            var sql = "";
            var count = 0;
            if (string.IsNullOrWhiteSpace(entity["CollectDateFrom"]) && string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
            {
                sql = @"select * from SM_T_DAYENERGY where Category=@Category and CollectDate>=convert(nvarchar(10),dateadd(day,-7,getdate()),120) 
   ";
                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"] });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select * from SM_T_DAYENERGY where Category=@Category ";

                var param = new
                {
                    Category = entity["Category"],
                    CollectDateTo = QueryBuilder.DateTo(ref sql, entity, "CollectDate", "CollectDateTo"),
                    CollectDateFrom = QueryBuilder.DateFrom(ref sql, entity, "CollectDate", "CollectDateFrom"),
                };

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", param);
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }

        public int QueryByYear(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {

            var sql = "";
            var count = 0;
            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {
                sql = @"select sum(TotalValue) as TotalValue,CollectYear as CollectDate from SM_T_DAYENERGY where Category=@Category 
                    and CollectYear>=convert(nvarchar(4),getdate(),120)-7
                    group by CollectYear                
                    order by collectyear desc
                    ";



                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"] });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(TotalValue) as TotalValue,CollectYear from sm_t_dayenergy 
            where Category=@Category ";

                var param = new
                {
                    CollectYearTo = QueryBuilder.DateTo(ref sql, entity, "CollectYear", "CollectYearTo"),
                    CollectYearFrom = QueryBuilder.DateFrom(ref sql, entity, "CollectYear", "CollectYearFrom"),
                };
                sql = sql + " group by CollectYear ";
                var list = GetList(entity, ref count, start, limit, sql, "order by CollectYear desc", param);
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }


        public int QueryByMonth(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {

            var sql = "";
            var count = 0;
            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                sql = @"select sum(TotalValue) as TotalValue,Line,CollectYear + '-' + CollectMonth as CollectDate from sm_t_dayenergy
                where Category = @Category and  cast(collectdate as nvarchar(7)) >= convert(nvarchar(7), dateadd(month, -7, getdate()), 120)
group by Line,collectyear,collectMonth
                ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"] });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(TotalValue),Line,CollectYear+'-'+CollectMonth as CollectDate from sm_t_dayenergy 
            where Category=@Category ";
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                    sql = sql + " and cast(collectdate as nvarchar(7))>=@CollectMonthFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and cast(collectdate as nvarchar(7))<=@CollectMonthTo ";
                }

                sql = sql + " group by Line,collectyear,collectMonth ";

                var list = GetList(entity, ref count, start, limit, sql, "order by collectdate desc", new { Category = entity["Category"], CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }

    }
}