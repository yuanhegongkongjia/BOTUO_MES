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
    public class Form_BT_POWER_MONTHDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from BT_POWER_MONTH where PKID=@PKID", data.Select(a => new { PKID = a["PKID"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            
           

            var sql = @"select * from BT_POWER_MONTH where 1=1";

            var param = new
            {
                PKID = QueryBuilder.Like(ref sql, entity, "PKID", "PKID"),
                //POSITION = QueryBuilder.Like(ref sql, entity, "POSITION", "POSITION"),
                //CollectMonthFrom = QueryBuilder.DateFrom(ref sql, entity, "REMARK2", "CollectMonthFrom"),
                //CollectMonthTo = QueryBuilder.DateTo(ref sql, entity, "REMARK2", "CollectMonthTo"),
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by PKID", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
