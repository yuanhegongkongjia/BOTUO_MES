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
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WFCore_AllWFDEPTDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
            var user = Util.GetCurrentUser();

            // 为下拉框提供查询条件
            this.Model.Add("UserId", user.UserId);

            // 检查按钮权限
            //form.GetControlM("btnDelete").Visible = false;
            //form.GetControlM("btnCancelWorkflow").Visible = false;
            //var user = Util.GetCurrentUser();
            //if (AuthLoader.CheckFunctionAccess("WFCore_AllWF.btnDelete", user.UserId))
            //{
            //    form.GetControlM("btnDelete").Visible = true;
            //}
            //if (AuthLoader.CheckFunctionAccess("WFCore_AllWF.btnCancelWorkflow", user.UserId))
            //{
            //    form.GetControlM("btnCancelWorkflow").Visible = true;
            //}
        }
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var user = Util.GetCurrentUser();
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new Exception("无效的参数 data".GetRes());
            }
            if (entity["subAction"] == "DeleteWorkflow")
            {
                using (var db = Pub.DB)
                {
                    var sql = "exec wfsp_delete_instance @InstanceId";
                    db.Execute(sql, data.Select(a => new { InstanceId = a["InstanceId"] }));
                    message = "删除成功".GetRes();
                }
            }
            else if (entity["subAction"] == "CancelWorkflow")
            {
                var engine = NinjectHelper.Get<IEngine>();
                if (engine == null)
                {
                    throw new Exception("找不到 IEngine".GetRes());
                }
                var failed = 0;
                var success = 0;
                foreach (var item in data)
                {
                    var InstanceId = item["InstanceId"];
                    if (!string.IsNullOrWhiteSpace(InstanceId))
                    {
                        var Instance = WFDA.Instance.GetInstance(InstanceId);
                        if (null != Instance && Instance.InstanceStatus == Pub.Running)
                        {
                            if (engine.AbortWF(Instance.InstanceId, user.UserId, user.UserName))
                            {
                                AbortWF(Instance.InstanceId);
                                success++;
                            }
                            else
                            {
                                failed++;
                            }
                        }
                        else
                        {
                            failed++;
                        }
                    }
                }
                message = string.Format("成功取消流程 {0} 个，失败 {1} 个", success, failed);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private static void AbortWF(string instanceId)
        {
            var model = WFDA.Instance.GetModelByInstanceId(instanceId);
            if (model == null)
            {
                throw new WFException(string.Format("根据工作流实例编号 {0} 不能找到对应的工作流模型定义", instanceId));
            }
            var f = DFPub.GetFormM(model.DFFormName);
            var da = NinjectHelper.Get<IDA>(f.DAImp);
            if (da == null)
            {
                throw new WFException(string.Format("根据 {0} 不能创建 IDA 接口", f.DAImp));
            }
            var d = new DFDictionary();
            d.Add("InstanceId", instanceId);
            var msg = string.Empty;
            (da as BaseDA).AbortWF(f, d, ref msg);
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select ins.*,u.chinesename,u.EmployeeId from WF_T_INSTANCE ins 
            left join wf_m_user u on ins.Requestor=u.userid
                  left join wf_m_userdept ud on  u.deptid=ud.deptid
                where 1=1 and ud.UserId=@UserId";
                if (!string.IsNullOrWhiteSpace(entity["DeptId"]))
                {
                    sql += " and ud.DeptId in (SELECT DeptId FROM dbo.WF_M_DEPT WHERE DeptLabel LIKE (SELECT DeptLabel+'%' FROM dbo.WF_M_DEPT WHERE DeptId=@DeptId))";
                }
                if (!string.IsNullOrWhiteSpace(entity["InstanceStatus"]))
                {
                    sql += " and InstanceStatus in @InstanceStatus";
                }
                if (!string.IsNullOrWhiteSpace(entity["InstanceId"]))
                {
                    sql += " and InstanceId like @InstanceId";
                }
                if (!string.IsNullOrWhiteSpace(entity["ModelName"]))
                {
                    sql += " and ModelName like @ModelName";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestTimeFrom"]))
                {
                    sql += " and RequestTime>=@RequestTimeFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestTimeTo"]))
                {
                    sql += " and RequestTime<=@RequestTimeTo";
                }
                if (!string.IsNullOrWhiteSpace(entity["RequestorName"]))
                {
                    sql += " and (RequestorName like @RequestorName or u.EmployeeId like @RequestorName or u.ChineseName like @RequestorName)";
                }
                if (!string.IsNullOrWhiteSpace(entity["EndDateFrom"]))
                {
                    sql += " and (ins.instancestatus='Finished' and ins.lastmodifytime>=@EndDateFrom)";
                }
                if (!string.IsNullOrWhiteSpace(entity["EndDateTo"]))
                {
                    sql += " and (ins.instancestatus='Finished' and ins.lastmodifytime<=@EndDateTo)";
                }

                sql += " order by ins.LastModifyTime desc";
                var parameters = new
                {
                    UserId = Util.GetCurrentUser().UserId,
                    DeptId = entity["DeptId"],
                    InstanceStatus = entity["InstanceStatus"].Split(',').ToList(),
                    InstanceId = string.Format("%{0}%", entity["InstanceId"]),
                    ModelName = string.Format("%{0}%", entity["ModelName"]),
                    RequestTimeFrom = ParseHelper.ParseDate(entity["RequestTimeFrom"]).GetValueOrDefault().ToString("yyyy-MM-dd"),
                    RequestTimeTo = ParseHelper.ParseDate(entity["RequestTimeTo"]).GetValueOrDefault().ToString("yyyy-MM-dd 23:59:59.999"),
                    RequestorName = "%" + entity["RequestorName"] + "%",
                    EndDateFrom = ParseHelper.ParseDate(entity["EndDateFrom"]).GetValueOrDefault().ToString("yyyy-MM-dd"),
                    EndDateTo = ParseHelper.ParseDate(entity["EndDateTo"]).GetValueOrDefault().ToString("yyyy-MM-dd 23:59:59.999")
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_INSTANCE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                foreach (var l in list)
                {
                    l.CurrentExecutorName = StepExecutorLoader.GetCurrentExecutorName(l.InstanceId);
                }
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}