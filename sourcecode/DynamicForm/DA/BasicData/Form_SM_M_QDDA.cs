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
    public class Form_SM_M_QDDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from SM_M_QD where PKId=@PKId", data.Select(a => new { PKId = a["PKId"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

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
