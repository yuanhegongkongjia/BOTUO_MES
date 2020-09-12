using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm.DA
{
    public class WF_LogMgmtDA : BaseDA
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
                    var sql = "delete from WF_T_LOG where LogId=@LogId";
                    db.Execute(sql, data.Select(a => new { LogId = a["LogId"] }));
                }
                message = "删除成功".GetRes();
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
            /*基本查询语句*/
            var sql = @"select * from WF_T_LOG where 1=1";
            /*查询条件*/
            if (!string.IsNullOrWhiteSpace(entity["FuncModule"]))
            {
                sql += " and FuncModule like @FuncModule";
            }
            if (!string.IsNullOrWhiteSpace(entity["LogLevel"]))
            {
                sql += " and LogLevel like @LogLevel";
            }
            if (!string.IsNullOrWhiteSpace(entity["UserName"]))
            {
                sql += " and UserName like @UserName";
            }
            if (!string.IsNullOrWhiteSpace(entity["CreateTimeFrom"])) // 日志时间从
            {
                sql += " and CreateTime>=@CreateTimeFrom";
            }

            if (!string.IsNullOrWhiteSpace(entity["CreateTimeTo"])) // 日志时间到
            {
                sql += " and CreateTime<=@CreateTimeTo";
            }
            sql += " order by CreateTime desc";
            using (var db = Pub.DB)
            {
                var parameters = new
                {
                    FuncModule = string.Format("%{0}%", entity["FuncModule"]),
                    UserName = string.Format("%{0}%", entity["UserName"]),
                    LogLevel = string.Format("%{0}%", entity["LogLevel"]),
                    CreateTimeFrom = ParseHelper.ParseDate(entity["CreateTimeFrom"]).GetValueOrDefault(),
                    CreateTimeTo = ParseHelper.ParseDate(entity["CreateTimeTo"]).GetValueOrDefault().AddDays(1).AddSeconds(-1),
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<WF_T_LOG>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}
