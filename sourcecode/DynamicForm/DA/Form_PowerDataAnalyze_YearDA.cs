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
    public class Form_PowerDataAnalyze_YearDA: BaseDA
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

            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Position = @Position and CollectYear>=convert(nvarchar(4), getdate(), 120) - 7
group by Position,PeriodName,CollectYear
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"], });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Position = @Position ";

                if (!string.IsNullOrWhiteSpace(entity["CollectYearFrom"]))
                {
                    sql = sql + " and CollectYear>=@CollectYearFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
                {
                    sql = sql + " and CollectYear<=@CollectYearTo  ";
                }


                sql = sql + " group by Position,PeriodName,CollectYear ";


                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"], CollectYearFrom = entity["CollectYearFrom"], CollectYearTo = entity["CollectYearTo"] });
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
            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Line = @Line and CollectYear>=convert(nvarchar(4), getdate(), 120) - 7
group by Line,PeriodName,CollectYear
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"], });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectYear as CollectDate from SM_T_POWER
              where Line = @Line ";

                if (!string.IsNullOrWhiteSpace(entity["CollectYearFrom"]))
                {
                    sql = sql + " and CollectYear>=@CollectYearFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
                {
                    sql = sql + " and CollectYear<=@CollectYearTo  ";
                }


                sql = sql + " group by Line,PeriodName,CollectYear ";


                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"], CollectYearFrom = entity["CollectYearFrom"], CollectYearTo = entity["CollectYearTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }

       
    }
}