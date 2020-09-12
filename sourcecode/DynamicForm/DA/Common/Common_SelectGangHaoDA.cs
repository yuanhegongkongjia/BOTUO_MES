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
    public class Common_SelectGangHaoDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from SM_M_QD where 1=1";
            var param = new
            {
                GangHao = QueryBuilder.Like(ref sql, entity, "GangHao", "GangHao"),
                QDLevel = QueryBuilder.Like(ref sql, entity, "QDLevel", "QDLevel")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by GangHao", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}