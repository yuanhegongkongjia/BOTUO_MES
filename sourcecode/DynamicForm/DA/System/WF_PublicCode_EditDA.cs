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
    public class WF_PublicCode_EditDA : BaseDA
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
                var sql = "select * from WF_M_PUBLICCODE where 1=1";

                /*查询条件*/
                sql += " and CodeId=@CodeId";

                using (var db = Pub.DB)
                {
                    var parameters = new { CodeId = entity["CodeId"] };
                    var oldEntity = db.Query<WF_M_PUBLICCODE>(sql, parameters).FirstOrDefault();
                    if (oldEntity == null)
                    {
                        message = "记录已经不存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }

                    oldEntity.CodeType = entity["CodeType"];
                    oldEntity.CodeName = entity["CodeName"];
                    oldEntity.CodeValue = entity["CodeValue"];
                    oldEntity.CodeLongValue = entity["CodeLongValue"];
                    oldEntity.CodeOrder = ParseHelper.ParseInt(entity["CodeOrder"]);
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
                if (string.IsNullOrWhiteSpace(entity["CodeType"]))
                {
                    throw new Exception("代码分类必须输入".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["CodeName"]))
                {
                    throw new Exception("代码名称必须输入".GetRes());
                }
                var currentUser = Util.GetCurrentUser();
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = "select * from WF_M_PUBLICCODE where 1=1";

                /*查询条件*/
                sql += " and CodeType=@CodeType and CodeName=@CodeName";

                using (var db = Pub.DB)
                {
                    var parameters = new { CodeName = entity["CodeName"], CodeType = entity["CodeType"] };
                    var oldEntity = db.Query<WF_M_PUBLICCODE>(sql, parameters).FirstOrDefault();
                    if (oldEntity != null)
                    {
                        message = "代码已经存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }

                    var newEntity = entity.To<WF_M_PUBLICCODE>();
                    newEntity.CodeId = Guid.NewGuid().ToString();
                    newEntity.CodeType = entity["CodeType"];
                    newEntity.CodeName = entity["CodeName"];
                    newEntity.CodeValue = entity["CodeValue"];
                    newEntity.CodeLongValue = entity["CodeLongValue"];
                    newEntity.CodeOrder = ParseHelper.ParseInt(entity["CodeOrder"]);
                    newEntity.CreateUser = currentUser.UserName;
                    newEntity.CreateTime = DateTime.Now;
                    newEntity.LastModifyUser = currentUser.UserName;
                    newEntity.LastModifyTime = DateTime.Now;
                    db.Insert(newEntity);

                    // 默认为 role_user 角色组用户
                    UserRoleLoader.Create(newEntity.CodeId, Constants.ROLE_USER, currentUser.UserName);

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
                var sql = @"select a.* from WF_M_PUBLICCODE a where 1=1";

                /*查询条件*/
                sql += " and a.CodeId=@CodeId";
                if (!string.IsNullOrWhiteSpace(entity["CodeId"]))
                {
                    using (var db = Pub.DB)
                    {
                        var parameters = new { CodeId = entity["CodeId"] };
                        var item = db.Query<WF_M_PUBLICCODE>(sql, parameters).FirstOrDefault();
                        if (item != null)
                        {
                            dict = DFDictionary.Create<WF_M_PUBLICCODE>(item);
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
            // 编辑的时候 CodeType CodeName 是不能修改的
            if (entity["EditMode"] == "Edit")
            {
                var c = form.GetControlM("CodeType");
                if (null != c)
                {
                    c.Readonly = true;
                }
                c = form.GetControlM("CodeName");
                if (null != c)
                {
                    c.Readonly = true;
                }
            }

        }
    }
}
