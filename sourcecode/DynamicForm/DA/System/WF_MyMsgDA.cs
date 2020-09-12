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
    public class WF_MyMsgDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var currentUser = Util.GetCurrentUser();
            if (!string.IsNullOrWhiteSpace(entity["MsgId"]))
            {
                MsgCenter.MarkRead(new List<string>() { entity["MsgId"] }, currentUser.UserName);
                MsgCenter.RefreshUserMessage(currentUser.UserId);
                return EmptyQuery(vm);
            }
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_T_MSG where 1=1";
                sql += " and UserId=@UserId";

                if (!string.IsNullOrWhiteSpace(entity["IsRead"]))
                {
                    sql += " and IsRead=@IsRead";
                }
                if (!string.IsNullOrWhiteSpace(entity["CreateTimeFrom"]))
                {
                    sql += " and CreateTime>=@CreateTimeFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["CreateTimeTo"]))
                {
                    sql += " and CreateTime<=@CreateTimeTo";
                }
                sql += " order by CreateTime desc";

                var parameters = new
                {
                    UserId = currentUser.UserId,
                    IsRead = ParseHelper.ParseInt(entity["IsRead"]).GetValueOrDefault(),
                    CreateTimeFrom = ParseHelper.ParseDate(entity["CreateTimeFrom"]).GetValueOrDefault(),
                    CreateTimeTo = ParseHelper.ParseDate(entity["CreateTimeTo"]).GetValueOrDefault().AddDays(1).AddSeconds(-1),
                };
                vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                var list = db.Query<VM_WF_T_MSG>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                vm.rows = list;
                return DFPub.EXECUTE_SUCCESS;
            }
        }

        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var currentUser = Util.GetCurrentUser();
                var subAction = entity["subAction"];
                if (subAction == "MarkAllRead")
                {
                    MsgCenter.MarkAllRead(currentUser.UserId, currentUser.UserName);
                    MsgCenter.RefreshUserMessage(currentUser.UserId);
                    message = "全部标记已读成功";
                }
                else
                {
                    var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
                    if (data == null)
                    {
                        throw new ArgumentNullException("data");
                    }
                    if (subAction == "MarkRead")
                    {
                        MsgCenter.MarkRead(data.Select(a => a["MsgId"]).ToList(), currentUser.UserName);
                        MsgCenter.RefreshUserMessage(currentUser.UserId);
                        message = "标记已读成功";
                    }
                    else
                    {
                        MsgCenter.DeleteMessage(data.Select(a => a["MsgId"]).ToList());
                        MsgCenter.RefreshUserMessage(currentUser.UserId);
                        message = "删除成功";
                    }
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