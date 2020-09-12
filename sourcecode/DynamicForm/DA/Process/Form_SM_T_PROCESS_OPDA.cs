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
    public class Form_SM_T_PROCESS_OPDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }
            Delete("update  SM_T_PROCESS set ProcessStatus='RECOVER',RecoverTime=getdate() where InstanceId=@InstanceId", data.Select(a => new { InstanceId = a["InstanceId"] }).ToList());
            message = "回收成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select p.*,(case when p.ProcessStatus='INFLOW' then N'使用中' when p.ProcessStatus='RECOVER' then N'回收' else ''  end ) as ProcessStatusText from SM_T_PROCESS p
left join WF_T_INSTANCE i on i.InstanceId=p.InstanceId
where i.InstanceStatus ='Finished' ";
            var param = new
            {
                ProcessName = QueryBuilder.Like(ref sql, entity, "ProcessName", "ProcessName"),
                InstanceId = QueryBuilder.Like(ref sql, entity, "p.InstanceId", "InstanceId"),
                ProcessStatus = QueryBuilder.Like(ref sql, entity, "p.ProcessStatus", "ProcessStatus")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}