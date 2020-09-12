using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;

namespace DynamicForm.DA
{
    public class WFCore_StepDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["ModelId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_MODEL where 1=1";
                    sql += " and ModelId=@ModelId";
                    var parameters = new
                    {
                        ModelId = entity["ModelId"]
                    };
                    var item = db.Query<WF_M_MODEL>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_M_MODEL>(item);
                    }
                }
            }
            return dict;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["action"] == "querylist")
            {
                if (entity["subAction"] == "queryVariables")
                {
                    var data1 = WFBusinessData.AddPrefix("WF_T_INSTANCE_", WFBusinessData.CreateInstance<WF_T_INSTANCE>(new WFInstance()));
                    var data2 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEP_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEP>(new WFInstanceStep()));
                    var data3 = WFBusinessData.AddPrefix("WF_T_INSTANCESTEPEXECUTOR_", WFBusinessData.CreateInstance<WF_T_INSTANCESTEPEXECUTOR>(new WF_T_INSTANCESTEPEXECUTOR()));
                    WFBusinessData.Merge(data2, data1);
                    WFBusinessData.Merge(data3, data1);
                    var businessData = data1;
                    vm.rows = businessData.Keys.OrderBy(a => a).Select(a => new WFItem() { text = string.Format("@{0}", a), value = string.Format("@{0}", a) }).ToList();
                    return DFPub.EXECUTE_SUCCESS;
                }
                else
                {
                    var list = new List<WFItem>();
                    var model = WFDA.Instance.GetModelById(entity["ModelId"]);
                    if (model != null)
                    {
                        list.Add(new WFItem() { text = model.ModelName, value = string.Format("{0}", model.ModelName).Trim() });
                    }
                    list.AddRange(WFDA.Instance.GetSteps(entity["ModelId"]).Select(a =>
                        new WFItem() { text = a.StepName, value = string.Format("{0}", a.StepName).Trim() }).ToList());

                    vm.rows = list;
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_STEP where 1=1";
                sql += " and ModelId=@ModelId";
                sql += " order by StepOrder";
                var parameters = new
                {
                    ModelId = entity["ModelId"]
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_STEP>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

    }
}
