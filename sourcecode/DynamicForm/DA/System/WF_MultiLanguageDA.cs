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
    public class WF_MultiLanguageDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                using (var db = Pub.DB)
                {
                    var sql = "delete from WF_M_RES where ResId=@ResId";
                    db.Execute(sql, data.Select(a => new { ResId = a["ResId"] }));
                    message = "删除成功";
                }
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            WFRes.Instance.Load();
            message = "刷新界面语言成功".GetRes();
            return base.Update(form, entity, ref message);
        }
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            try
            {
                /*基本查询语句*/
                var sql = @"select a.* from WF_M_RES a where 1=1";

                /*查询条件*/
                if (!string.IsNullOrWhiteSpace(entity["ResId"]))
                {
                    sql += " and a.ResId like @ResId";
                }
                sql += " order by a.ResId";
                using (var db = Pub.DB)
                {
                    var parameters = new
                    {
                        ResId = string.Format("%{0}%", entity["ResId"])
                    };
                    vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                    var list = db.Query<VM_WF_M_RES>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                    vm.rows = list;
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = ex.Message;
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
    }
}
