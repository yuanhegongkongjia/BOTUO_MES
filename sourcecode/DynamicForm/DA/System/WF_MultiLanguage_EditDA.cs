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
    public class WF_MultiLanguage_EditDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] != "Edit")
            {
                return Insert(form, entity, ref  message);
            }
            try
            {
                var currentUser = Util.GetCurrentUser();
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = "select * from WF_M_RES where 1=1";

                /*查询条件*/
                sql += " and ResId=@ResId";

                using (var db = Pub.DB)
                {
                    var parameters = new { ResId = entity["ResId"] };
                    var oldEntity = db.Query<WF_M_RES>(sql, parameters).FirstOrDefault();
                    if (oldEntity == null)
                    {
                        message = "记录已经不存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }

                    oldEntity.English = entity["English"];
                    oldEntity.ChineseSimplified = entity["ChineseSimplified"];
                    oldEntity.ChineseTraditional = entity["ChineseTraditional"];
                    oldEntity.LastModifyUser = currentUser.UserName;
                    oldEntity.LastModifyTime = DateTime.Now;
                    db.Update(oldEntity);
                    message = "保存成功".GetRes();
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity["ResId"]))
                {
                    throw new Exception("资源编号".GetRes());
                }
                var currentUser = Util.GetCurrentUser();
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = "select * from WF_M_RES where 1=1";

                /*查询条件*/
                sql += " and ResId=@ResId";

                using (var db = Pub.DB)
                {
                    var parameters = new { ResId = entity["ResId"] };
                    var oldEntity = db.Query<WF_M_RES>(sql, parameters).FirstOrDefault();
                    if (oldEntity != null)
                    {
                        message = "记录已经存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }

                    var newEntity = entity.To<WF_M_RES>();
                    newEntity.ResId = entity["ResId"];
                    newEntity.English = entity["English"];
                    newEntity.ChineseSimplified = entity["ChineseSimplified"];
                    newEntity.ChineseTraditional = entity["ChineseTraditional"];
                    newEntity.CreateUser = currentUser.UserName;
                    newEntity.CreateTime = DateTime.Now;
                    newEntity.LastModifyUser = currentUser.UserName;
                    newEntity.LastModifyTime = DateTime.Now;
                    db.Insert(newEntity);

                    message = "新增成功".GetRes();
                    return DFPub.EXECUTE_SUCCESS;

                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = @"select a.* from WF_M_RES a where 1=1";

                /*查询条件*/
                sql += " and a.ResId=@ResId";
                if (!string.IsNullOrWhiteSpace(entity["ResId"]))
                {
                    using (var db = Pub.DB)
                    {
                        var parameters = new { ResId = entity["ResId"] };
                        var item = db.Query<VM_WF_M_RES>(sql, parameters).FirstOrDefault();
                        if (item != null)
                        {
                            dict = DFDictionary.Create<VM_WF_M_RES>(item);
                        }
                    }
                }
                return dict;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
        }
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            // 编辑的时候ResId是不能修改的
            if (entity["EditMode"] == "Edit")
            {
                var c = form.GetControlM("ResId");
                if (null != c)
                {
                    c.Readonly = true;
                }
            }

        }
    }
}
