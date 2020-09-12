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
    public class Form_EnergyQuery_CurrentDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select Position,Line,CollectValue,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CollectTime from SM_T_ENERGY_COLLECT 
                        where Category=@Category and Line=@Line 
                        and CollectTime>=@CollectTime";
    
            var CollectTime = "";
            if (DateTime.Now.Hour > 8)
            {
                //取当天数据
                CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
            }
            else {
                //取前一天八点开始的数据
                CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
            }
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectTime desc", new { Category = entity["Category"], Line=entity["Line"],CollectTime= CollectTime });
            vm.results = count;
            vm.rows = list;


            return DFPub.EXECUTE_SUCCESS;
        }
    }
}