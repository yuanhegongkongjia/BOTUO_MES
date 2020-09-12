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
    public class WF_Role_EditDA : BaseDA
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
                var sql = "select * from WF_M_ROLE where 1=1";

                /*查询条件*/
                sql += " and RoleId=@RoleId";

                using (var db = Pub.DB)
                {
                    var parameters = new { RoleId = entity["RoleId"] };
                    var oldEntity = db.Query<WF_M_ROLE>(sql, parameters).FirstOrDefault();
                    if (oldEntity == null)
                    {
                        message = "记录已经不存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }
                    oldEntity.RoleName = entity["RoleName"];
                    oldEntity.Remark = entity["Remark"];
                    oldEntity.LastModifyUser = currentUser.UserName;
                    oldEntity.LastModifyTime = DateTime.Now;
                    db.Update(oldEntity);
                    message = "保存成功".GetRes();
                    return DFPub.EXECUTE_ERROR;
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
                var currentUser = Util.GetCurrentUser();
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = "select * from WF_M_ROLE where 1=1";

                /*查询条件*/
                sql += " and RoleId=@RoleId";

                using (var db = Pub.DB)
                {
                    var parameters = new { RoleId = entity["RoleId"] };
                    var oldEntity = db.Query<WF_M_ROLE>(sql, parameters).FirstOrDefault();
                    if (oldEntity != null)
                    {
                        message = "该角色编号已经存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }

                    var newEntity = entity.To<WF_M_ROLE>();
                    newEntity.RoleId = entity["RoleId"];
                    newEntity.RoleName = entity["RoleName"];
                    newEntity.Remark = entity["Remark"];

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
                var sql = "select * from WF_M_ROLE where 1=1";

                /*查询条件*/
                sql += " and RoleId=@RoleId";
                if (!string.IsNullOrWhiteSpace(entity["RoleId"]))
                {
                    using (var db = Pub.DB)
                    {
                        var parameters = new { RoleId = entity["RoleId"] };
                        var item = db.Query<WF_M_ROLE>(sql, parameters).FirstOrDefault();
                        if (item != null)
                        {
                            dict = DFDictionary.Create<WF_M_ROLE>(item);
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
            // 编辑的时候用户名是不能修改的
            if (entity["EditMode"] == "Edit")
            {
                var c = form.GetControlM("RoleId");
                if (null != c)
                {
                    c.Readonly = true;
                }
            }
        }
    }
}
