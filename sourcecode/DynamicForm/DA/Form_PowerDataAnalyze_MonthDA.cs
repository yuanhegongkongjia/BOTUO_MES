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
    public class Form_PowerDataAnalyze_MonthDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["QueryType"] == "Signle")
            {
                return QuerySignle(form, entity, vm, start, limit, ref message);
            }
            else if (entity["QueryType"] == "Line")
            {
                return QueryLine(form, entity, vm, start, limit, ref message);
            }
            return DFPub.EXECUTE_SUCCESS;

        }

        public int QuerySignle(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = "";
            var CollectMonthFrom = "";
            var CollectMonthTo = "";

            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                CollectMonthTo = DateTime.Now.ToString("yyyy-MM");
                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Position = @Position and  collectMonth>=@CollectMonthFrom
               and collectMonth<=@CollectMonthTo
group by Position,PeriodName,CollectYear,CollectMonth
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"], CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectMonth as CollectDate from SM_T_POWER
              where Position = @Position ";

                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                    sql = sql + " and  CollectMonth>=@CollectMonthFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and  CollectMonth<=@CollectMonthTo ";
                }


                sql = sql + " group by Position,PeriodName,CollectYear,CollectMonth ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"], CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }


        public int QueryLine(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = "";
            var CollectMonthFrom = "";
            var CollectMonthTo = "";

            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                sql = @"select sum(CollectValue) as CollectValue,PeriodName,Line as Position,CollectMonth as CollectDate from SM_T_POWER
              where Line = @Line and  collectMonth>=@CollectMonthFrom
               and collectMonth<=@CollectMonthTo
group by Line,PeriodName,CollectYear,CollectMonth
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"], CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,PeriodName,Line as Position,CollectMonth as CollectDate from SM_T_POWER
              where Line = @Line ";

                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                    sql = sql + " and  CollectMonth>=@CollectMonthFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and  CollectMonth<=@CollectMonthTo ";
                }


                sql = sql + " group by Line,PeriodName,CollectYear,CollectMonth ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"], CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}