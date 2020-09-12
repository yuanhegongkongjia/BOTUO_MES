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
    public class WF_PublicCodeDA : BaseDA
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
                    var sql = "delete from WF_M_PUBLICCODE where CodeId=@CodeId";
                    db.Execute(sql, data.Select(a => new { CodeId = a["CodeId"] }));
                    message = "删除成功".GetRes();
                }
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["action"] == "querylist")
            {
                vm.rows = WF_M_PUBLICCODELoader.Query(entity["CodeType"]).Select(a =>
                    new WFItem() { text = a.CodeName, value = string.Format("{0}{1}", a.CodeValue, a.CodeLongValue) }).ToList();
                return DFPub.EXECUTE_SUCCESS;
            }


            /*基本查询语句*/
            var sql = @"select a.* from WF_M_PUBLICCODE a where 1=1";

            /*查询条件*/
            if (!string.IsNullOrWhiteSpace(entity["CodeType"]))
            {
                sql += " and a.CodeType like @CodeType";
            }
            sql += " order by a.CodeType";
            using (var db = Pub.DB)
            {
                var parameters = new
                {
                    CodeType = string.Format("%{0}%", entity["CodeType"])
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<WF_M_PUBLICCODE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}
