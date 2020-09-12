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
    public class Form_PowerDataAnalyze_Summary_MonthDA : BaseDA
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
            var CollectMonthFrom = "";
            var CollectMonthTo = "";

            var str = entity["Position"].Split(',');

            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                CollectMonthTo = DateTime.Now.ToString("yyyy-MM");
                sql = @"select sum(CollectValue) as CollectValue,Position,CollectMonth as CollectDate from SM_T_POWER
              where Position in @Position and  collectMonth>=@CollectMonthFrom
               and collectMonth<=@CollectMonthTo
group by Position,CollectMonth
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray(),CollectMonthFrom=CollectMonthFrom,CollectMonthTo=CollectMonthTo });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,CollectMonth as CollectDate from SM_T_POWER
              where Position in @Position ";

                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                    sql = sql + " and  CollectMonth>=@CollectMonthFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and  CollectMonth<=@CollectMonthTo ";
                }


                sql = sql + " group by Position,CollectMonth ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray(), CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
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
            var str = entity["Line"].Split(',');

            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                CollectMonthTo = DateTime.Now.ToString("yyyy-MM");

                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectMonth as CollectDate from SM_T_POWER
              where Line in @Line and  collectMonth>=@CollectMonthFrom
               and collectMonth<=@CollectMonthTo
group by Line,CollectMonth
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray(),CollectMonthFrom=CollectMonthFrom,CollectMonthTo=CollectMonthTo });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectMonth as CollectDate from SM_T_POWER
              where Line in @Line ";

                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                    sql = sql + " and  CollectMonth>=@CollectMonthFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and  CollectMonth<=@CollectMonthTo ";
                }


                sql = sql + " group by Line,CollectMonth ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray(), CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}