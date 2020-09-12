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
    public class WFCore_Step_EditDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["StepId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_STEP where 1=1";
                    sql += " and StepId=@StepId";
                    var parameters = new
                    {
                        StepId = entity["StepId"]
                    };
                    var item = db.Query<WF_M_STEP>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_M_STEP>(item);
                    }
                }
            }
            return dict;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] != "Edit")
            {
                return Insert(form, entity, ref  message);
            }
            var user = Util.GetCurrentUser();
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_STEP where 1=1";
                sql += " and StepId=@StepId";
                var parameters = new
                {
                    StepId = entity["StepId"]
                };
                var oldEntity = db.Query<WF_M_STEP>(sql, parameters).FirstOrDefault();
                if (oldEntity == null)
                {
                    throw new WFException("找不到要更新的记录".GetRes());
                }

                // 先将数据库查出来的老的实体转成字典，然后把客户端传过来的字典合并进去，这样就实现了数据的更新
                var newEntity = DFDictionary.Create<WF_M_STEP>(oldEntity).Merge(entity).To<WF_M_STEP>();
                newEntity.IsSendMessage = ParseHelper.ParseInt(entity["IsSendMessage"]).GetValueOrDefault();
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;
                db.Update<WF_M_STEP>(newEntity);
                message = "保存成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}
