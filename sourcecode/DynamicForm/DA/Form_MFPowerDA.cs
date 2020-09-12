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
    public class Form_MFPowerDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"exec sp_mfpower @Line=@Line,@CollectTimeFrom=@CollectTimeFrom,@CollectTimeTo=@CollectTimeTo";
            var param = new
            {
                Line = entity["Line"],


                CollectTimeFrom = entity["CollectTimeFrom"],
                CollectTimeTo = entity["CollectTimeTo"],
            };

            //sql = sql + " group by Category,Line";
            using (var db = Pub.DB)
            {
                var dt = db.ExecuteDataTable(sql, param);
                vm.results = count;
                vm.rows = dt;
                //AutoGenerateColumns(form, entity, vm, dt);
            }
            //vm.results = count;
            //vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}