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
    public class Form_EnergyDataAnalyze_MonthDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = "";

            if (string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]) && string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
            {
                var CollectMonthFrom = DateTime.Now.AddMonths(-7).ToString("yyyy-MM");
                //var MonthFrom = DateTime.Now.AddMonths(-7).Month;
                var CollectMonthTo = DateTime.Now.ToString("yyyy-MM");
                var MonthTo = DateTime.Now.Month;
                

                sql = @"select TotalValue,Line, CollectDate
from v_sm_t_energy_month where Category = @Category 
                    and CollectDate>=@CollectMonthFrom and CollectDate<=@CollectMonthTo  ";

                var list = GetList(entity, ref count, start, limit, sql, "order by CollectDate desc", new { Category = entity["Category"], CollectMonthFrom = CollectMonthFrom, CollectMonthTo = CollectMonthTo });
                vm.results = count;
                vm.rows = list;
            }
            else
            {

                sql = @"select TotalValue,Line,CollectDate from v_sm_t_energy_month 
            where Category = @Category ";


                if (!string.IsNullOrWhiteSpace(entity["CollectMonthFrom"]))
                {
                   
                    sql = sql + " and CollectDate>=@CollectMonthFrom ";
                }
                if (!string.IsNullOrWhiteSpace(entity["CollectMonthTo"]))
                {
                    sql = sql + " and CollectDate<=@CollectMonthTo ";
                }


                var list = GetList(entity, ref count, start, limit, sql, "order by collectdate desc", new { Category = entity["Category"], CollectMonthFrom = entity["CollectMonthFrom"], CollectMonthTo = entity["CollectMonthTo"] });
                vm.results = count;
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }

            return DFPub.EXECUTE_SUCCESS;
        }


    }
}