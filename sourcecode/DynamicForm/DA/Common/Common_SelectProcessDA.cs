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
using System.Data;

namespace DynamicForm.DA
{
    public class Common_SelectProcessDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            //var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            //if (data == null)
            //{
            //    throw new WFException("无效的参数data".GetRes());
            //}
            var InstanceId = entity["InstanceId"];
            var NewInstanceId = entity["NewInstanceId"];
            var currentUser = Util.GetCurrentUser().UserName;
            var msg = "";
            using (var db = Pub.DB)
            {
                var sql= "sp_processduplicate";


                DynamicParameters dp = new DynamicParameters();
                dp.Add("@NewInstanceId", NewInstanceId);
                dp.Add("@InstanceId", InstanceId);
                dp.Add("@CreateUser", currentUser);
                dp.Add("@msg", msg, DbType.String, ParameterDirection.Output);


                db.Execute(sql, dp, null, null, CommandType.StoredProcedure);
                msg = dp.Get<string>("@msg");
            }
                //Delete("exec sp_processduplicate @NewInstanceId=@NewInstanceId,@InstanceId=@InstanceId,@CreateUser=@CreateUser,@msg=@msg", new { NewInstanceId = NewInstanceId, InstanceId = InstanceId, CreateUser = currentUser });
            message = msg.GetRes();
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
                InstanceId = QueryBuilder.Like(ref sql, entity, "InstanceId", "InstanceId"),
                //ProcessStauts = QueryBuilder.Like(ref sql, entity, "ProcessStauts", "ProcessStauts")
            };
            var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);
            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}