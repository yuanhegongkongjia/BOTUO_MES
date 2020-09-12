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
    public class Form_PowerDataAnalyze_Summary_YearDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
            this.Model.Add("Line", "L01,L02,L03,L04");
        }
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
            var str = entity["Position"].Split(',');
            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,CollectYear as CollectDate from SM_T_POWER
              where Position in @Position and CollectYear>=convert(nvarchar(4), getdate(), 120) - 7
group by Position,CollectYear
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray() });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,CollectYear as CollectDate from SM_T_POWER
              where Position in @Position ";

                if (!string.IsNullOrWhiteSpace(entity["CollectYearFrom"]))
                {
                    sql = sql + " and CollectYear>=@CollectYearFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
                {
                    sql = sql + " and CollectYear<=@CollectYearTo  ";
                }


                sql = sql + " group by Position,CollectYear ";


                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray(), CollectYearFrom = entity["CollectYearFrom"], CollectYearTo = entity["CollectYearTo"] });
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
            var str = entity["Line"].Split(',');
            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectYear as CollectDate from SM_T_POWER
              where Line in @Line and CollectYear>=convert(nvarchar(4), getdate(), 120) - 7
group by Line,CollectYear
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray() });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectYear as CollectDate from SM_T_POWER
              where Line in @Line ";

                if (!string.IsNullOrWhiteSpace(entity["CollectYearFrom"]))
                {
                    sql = sql + " and CollectYear>=@CollectYearFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
                {
                    sql = sql + " and CollectYear<=@CollectYearTo  ";
                }


                sql = sql + " group by Line,CollectYear ";


                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray(), CollectYearFrom = entity["CollectYearFrom"], CollectYearTo = entity["CollectYearTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}