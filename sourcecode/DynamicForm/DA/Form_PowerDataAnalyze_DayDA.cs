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
    public class Form_PowerDataAnalyze_DayDA : BaseDA
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
            var sql = "";
            var count = 0;
            if (string.IsNullOrWhiteSpace(entity["CollectDateFrom"]) && string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
            {

                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectDate from SM_T_POWER
              where Position = @Position and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Position,PeriodName,CollectDate
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"] });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,PeriodName,CollectDate from SM_T_POWER 
            where Position=@Position ";
                if (!string.IsNullOrWhiteSpace(entity["CollectDateFrom"]))
                {
                    sql = sql + " and CollectDate>=@CollectDateFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
                {
                    sql = sql + " and CollectDate<=@CollectDateTo ";
                }


                sql = sql + " group by Position,PeriodName,CollectDate ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = entity["Position"], CollectDateFrom = entity["CollectDateFrom"], CollectDateTo = entity["CollectDateTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
            return DFPub.EXECUTE_SUCCESS;
        }


        public int QueryLine(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var sql = "";
            var count = 0;
            if (string.IsNullOrWhiteSpace(entity["CollectDateFrom"]) && string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
            {

                sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectDate from SM_T_POWER
              where Line = @Line and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Line,PeriodName,CollectDate
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"] });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,PeriodName,CollectDate from SM_T_POWER
              where Line = @Line";
                if (!string.IsNullOrWhiteSpace(entity["CollectDateFrom"]))
                {
                    sql += " and CollectDate>=@CollectDateFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
                {
                    sql += " and CollectDate<=@CollectDateTo";
                }

                sql += " group by Line,PeriodName,CollectDate ";
                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = entity["Line"],CollectDateFrom=entity["CollectDateFrom"],CollectDateTo=entity["CollectDateTo"] });
                vm.results = count;
                vm.rows = list;
            }

            return DFPub.EXECUTE_SUCCESS;
        }

    }
}