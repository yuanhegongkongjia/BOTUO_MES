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
    public class WFCore_ModelDA : BaseDA
    {

        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
            if (data == null)
            {
                throw new WFException("无效的参数data".GetRes());
            }

            var success = 0;
            var failed = 0;
            foreach (var a in data)
            {
                // 删除关联性检查
                var ModelId = string.Format("{0}", a["ModelId"]).Trim();
                if (WFDA.Instance.DeleteModel(ModelId))
                {
                    success++;
                }
                else
                {
                    failed++;
                }

            }
            message = string.Format("成功删除 {0} 条记录，失败 {1}。".GetRes(), success, failed);
            return DFPub.EXECUTE_SUCCESS;
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_MODEL where 1=1";
                var parameters = new
                {
                };
                sql += " order by ModelName";
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_M_MODEL>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

        public override int Insert(DynamicForm.Core.FormM form, DynamicForm.Core.DFDictionary entity, ref string message)
        {
            try
            {
                var currentUser = Util.GetCurrentUser().UserName;
                var list = base.GetGridClientData<VM_WF_M_MODEL>(entity, "grid1");
                if (list == null || list.Count == 0)
                {
                    throw new WFException("没有得到客户端 grid 的数据，请检查 gridid 是否正确？");
                }
                foreach (var item in list.Where(a => a.selected))
                {
                    WFDA.Instance.Copy(item.ModelId, currentUser);
                }
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }

    }
}
