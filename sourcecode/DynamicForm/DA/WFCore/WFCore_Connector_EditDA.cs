﻿using System;
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
    public class WFCore_Connector_EditDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            if (!string.IsNullOrWhiteSpace(entity["ConnectorId"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_CONNECTOR where 1=1";
                    sql += " and ConnectorId=@ConnectorId";
                    var parameters = new
                    {
                        ConnectorId = entity["ConnectorId"]
                    };
                    var item = db.Query<WF_M_CONNECTOR>(sql, parameters).FirstOrDefault();
                    if (item != null)
                    {
                        dict = DFDictionary.Create<WF_M_CONNECTOR>(item);
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
                var sql = "select * from WF_M_CONNECTOR where 1=1";
                sql += " and ConnectorId=@ConnectorId";
                var parameters = new
                {
                    ConnectorId = entity["ConnectorId"]
                };
                var oldEntity = db.Query<WF_M_CONNECTOR>(sql, parameters).FirstOrDefault();
                if (oldEntity == null)
                {
                    throw new WFException("找不到要更新的记录".GetRes());
                }

                // 先将数据库查出来的老的实体转成字典，然后把客户端传过来的字典合并进去，这样就实现了数据的更新
                var newEntity = DFDictionary.Create<WF_M_CONNECTOR>(oldEntity).Merge(entity).To<WF_M_CONNECTOR>();
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;

                db.Update<WF_M_CONNECTOR>(newEntity);
                message = "保存成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
        }
    }
}
