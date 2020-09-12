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
    public class Form_PowerQuery_CurrentDA:BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["QueryType"] == "Signle")
            {
                return QuerySignle(form, entity, vm, start, limit, ref message);
            }
            else
            {
                return QueryLine(form, entity, vm, start, limit, ref message);
            }
            //return DFPub.EXECUTE_SUCCESS;

            
        }

        public int QuerySignle(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select CollectValue,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CollectTime,Position from SM_T_ENERGY_COLLECT 
                        where Position=@Position  
                        and CollectTime>=@CollectTime";

            var CollectTime = "";
            if (DateTime.Now.Hour > 8)
            {
                //取当天数据
                CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
            }
            else
            {
                //取前一天八点开始的数据
                CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
            }
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectTime desc", new { Position = entity["Position"],  CollectTime = CollectTime });
            vm.results = count;
            vm.rows = list;


            return DFPub.EXECUTE_SUCCESS;
        }

        public int QueryLine(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select Line as Position,CollectTime,sum(CollectValue) as CollectValue  from (select Line,SUBSTRING(convert(nvarchar(50),CollectTime,120),12,5) as CollectTime,CollectValue from SM_T_ENERGY_COLLECT 
                        where  Line=@Line and Category='POWER'
                        and CollectTime>=@CollectTime) b group by Line,CollectTime";

            var CollectTime = "";
            if (DateTime.Now.Hour > 8)
            {
                //取当天数据
                CollectTime = DateTime.Now.ToString("yyyy-MM-dd 08:00:00");
            }
            else
            {
                //取前一天八点开始的数据
                CollectTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 08:00:00");
            }
            var list = GetList(entity, ref count, start, limit, sql, "order by CollectTime desc", new { Line = entity["Line"], CollectTime = CollectTime });
            vm.results = count;
            vm.rows = list;


            return DFPub.EXECUTE_SUCCESS;
        }
    }
}