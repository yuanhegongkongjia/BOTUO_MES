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
    public class Form_WF_M_MSG_TEMPLATEDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("delete from WF_M_MSG_TEMPLATE where PK_GUID=@PK_GUID and IsSystem<>1", data.Select(a => new { PK_GUID = a["PK_GUID"] }).ToList());
            message = "删除成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select * from WF_M_MSG_TEMPLATE where 1=1";
            var param = new
            {
                TemplateName = QueryBuilder.Like(ref sql, entity, "TemplateName", "TemplateName")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by TemplateName", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
