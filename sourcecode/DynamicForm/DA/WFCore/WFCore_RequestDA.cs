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
    public class WFCore_RequestDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            if (!AuthLoader.CheckFunctionAccess("代理申请", Util.GetCurrentUser().UserId))
            {
                form.GetControlM("btnProxyRequest").Visible = false;
            }
            base.SetAccess(form, entity);
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_MODEL where 1=1";
                //sql += " and ModelId in @ModelId";
                if (!string.IsNullOrWhiteSpace(entity["ModelName"]))
                {
                    sql += " and ModelName like @ModelName";
                }
                var parameters = new
                {
                    ModelName = string.Format("%{0}%", entity["ModelName"]),
                    //ModelId = WF_M_ROLE_MODELLoader.GetRoleModel(Util.GetCurrentUser().UserId)
                };

                sql += " order by ModelName";
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<WF_M_MODEL>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                list.ForEach(a => a.ModelName = a.ModelName.GetRes());
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

    }
}
