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
    public class Form_SM_T_PROCESS_LOGDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
            var currentUser = Util.GetCurrentUser().UserId;
            if (UserRoleLoader.IsUserInRole(currentUser, "ProcessAdmin"))
            {
                form.GetControlM("btnEdit").Visible=true;
            }
            else {
                form.GetControlM("btnEdit").Visible = false;
            }
        }
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var count = 0;
            var sql = @"select p.* from wf_t_instance i left join sm_t_process p on i.instanceid=p.instanceid where 1=1
                    and i.instancestatus='Finished' and p.processstatus='INFLOW'";
            var param = new
            {
                InstanceId = QueryBuilder.Like(ref sql, entity, "p.InstanceId", "InstanceId")
            };
            //从存储过程得到信息
            var list = GetList(entity, ref count, start, limit, sql, "order by InstanceId", param);


            vm.results = count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }

        
    }
}