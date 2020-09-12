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
    public class Form_PowerDataAnalyze_Summary_DayDA : BaseDA
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
            var sql = "";
            var count = 0;
            var str = entity["Position"].Split(',');
            if (string.IsNullOrWhiteSpace(entity["CollectDateFrom"]) && string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
            {

                sql = @"select sum(CollectValue) as CollectValue,Position,CollectDate from SM_T_POWER
              where Position in @Position and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Position,CollectDate
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray() });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Position,CollectDate from SM_T_POWER 
            where Position in @Position ";
                if (!string.IsNullOrWhiteSpace(entity["CollectDateFrom"]))
                {
                    sql = sql + " and CollectDate>=@CollectDateFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
                {
                    sql = sql + " and CollectDate<=@CollectDateTo ";
                }


                sql = sql + " group by Position,CollectDate ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Position = str.ToArray(), CollectDateFrom = entity["CollectDateFrom"], CollectDateTo = entity["CollectDateTo"] });
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
            var str = entity["Line"].Split(',');
            if (string.IsNullOrWhiteSpace(entity["CollectDateFrom"]) && string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
            {

                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectDate from SM_T_POWER
              where Line in @Line and CollectDate>= convert(nvarchar(10), getdate()-7, 120) 
group by Line,CollectDate
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray() });
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(CollectValue) as CollectValue,Line as Position,CollectDate from SM_T_POWER
              where Line in @Line";
                if (!string.IsNullOrWhiteSpace(entity["CollectDateFrom"]))
                {
                    sql += " and CollectDate>=@CollectDateFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectDateTo"]))
                {
                    sql += " and CollectDate<=@CollectDateTo";
                }

                sql += " group by Line,CollectDate ";
                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Line = str.ToArray(), CollectDateFrom = entity["CollectDateFrom"], CollectDateTo = entity["CollectDateTo"] });
                vm.results = count;
                vm.rows = list;
            }

            return DFPub.EXECUTE_SUCCESS;
        }

    }
}