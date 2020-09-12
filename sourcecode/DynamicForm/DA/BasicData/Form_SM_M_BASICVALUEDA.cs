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
    public class Form_SM_M_BASICVALUEDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from SM_M_BASICVALUE where PKId=@PKId", data.Select(a => new { PKId = a["PKId"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from SM_M_BASICVALUE where 1=1";
            var param = new
            {
                LineName = QueryBuilder.Like(ref sql, entity, "LineName", "LineName"),
                Station = QueryBuilder.Like(ref sql, entity, "LineName", "LineName"),
                SubStation = QueryBuilder.Like(ref sql, entity, "LineName", "LineName"),
                Category = QueryBuilder.Like(ref sql, entity, "LineName", "LineName")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by LineName", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
