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
    public class Form_BT_PaiGongDanDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from BT_PaiGongDan where ID=@ID", data.Select(a => new { ID = a["ID"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from BT_PaiGongDan where 1=1";
            var param = new
            {
                ID = QueryBuilder.Like(ref sql, entity, "ID", "ID"),
                ORDER_NUMBER = QueryBuilder.Like(ref sql, entity, "ORDER_NUMBER", "ORDER_NUMBER"),
                PRODUCT_NAME = QueryBuilder.Like(ref sql, entity, "PRODUCT_NAME", "PRODUCT_NAME"),
                PROJECT_NAME = QueryBuilder.Like(ref sql, entity, "PROJECT_NAME", "PROJECT_NAME"),
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by CreateTime Desc", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
