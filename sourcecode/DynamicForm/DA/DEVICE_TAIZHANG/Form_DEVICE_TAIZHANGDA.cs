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
    public class Form_DEVICE_TAIZHANGDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from DEVICE_TAIZHANG where DEVICE_ID=@DEVICE_ID", data.Select(a => new { DEVICE_ID = a["DEVICE_ID"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from DEVICE_TAIZHANG where 1=1";
            var param = new
            {
                DEVICE_ID = QueryBuilder.Like(ref sql, entity, "DEVICE_ID", "DEVICE_ID")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by DEVICE_ID", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
