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
    public class Form_DeviceOEEDA:BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from v_sm_oee_bydate where 1=1";
            var param = new
            {
                MasterDeviceName = QueryBuilder.Like(ref sql, entity, "MasterDeviceName", "MasterDeviceName"),
                ProductDate = QueryBuilder.Like(ref sql, entity, "ProductDate", "ProductDate")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by MasterDeviceName", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}