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
    public class Form_EnergyDataAnalyze_YearDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = "";

            if (string.IsNullOrWhiteSpace(entity["CollectYearFrom"]) && string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
            {

                sql = @"select sum(TotalValue) as TotalValue,Line,CollectYear as CollectDate from sm_t_dayenergy
              where Category = @Category and CollectYear>= convert(nvarchar(4), getdate(), 120) - 7
group by Line,CollectYear
";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"]});
                vm.results = count;
                vm.rows = list;
            }
            else
            {
                sql = @"select sum(TotalValue) as TotalValue,Line,CollectYear as CollectDate from sm_t_dayenergy 
            where Category=@Category ";
                if (!string.IsNullOrWhiteSpace(entity["CollectYearFrom"]))
                {
                    sql = sql + " and CollectYear>=@CollectYearFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectYearTo"]))
                {
                    sql = sql + " and CollectYear<=@CollectYearTo ";
                }

    
                sql = sql + " group by Line,collectyear ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"], CollectYearFrom = entity["CollectYearFrom"], CollectYearTo = entity["CollectYearTo"]});
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}