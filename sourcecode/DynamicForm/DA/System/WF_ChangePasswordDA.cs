using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;
using System.Text;

namespace DynamicForm.DA
{
    public class WF_ChangePasswordDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity["OldPassword"]))
                {
                    throw new Exception("请输入旧密码".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["NewPassword"]))
                {
                    throw new Exception("请输入新密码".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["ConfirmNewPassword"]))
                {
                    throw new Exception("请输入确认新密码");
                }
                if (entity["NewPassword"] != entity["ConfirmNewPassword"])
                {
                    throw new Exception("新密码和确认新密码不一致".GetRes());
                }

                var currentUser = Util.GetCurrentUser();
                using (var db = Pub.DB)
                {
                    var sql = "select * from WF_M_USER where 1=1";
                    sql += " and UserId=@UserId";
                    var parameters = new { UserId = currentUser.UserId };
                    var oldEntity = db.Query<WF_M_USER>(sql, parameters).FirstOrDefault();
                    if (oldEntity == null)
                    {
                        message = "记录已经不存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }
                    if (HashHelper.GenerateUserHash(currentUser.UserName, entity["OldPassword"]) != oldEntity.Password)
                    {
                        throw new Exception("旧密码错误".GetRes());
                    }
                    oldEntity.Password = HashHelper.GenerateUserHash(currentUser.UserName, entity["NewPassword"]);
                    oldEntity.LastModifyTime = DateTime.Now;
                    oldEntity.LastModifyUser = currentUser.UserName;
                    db.Update<WF_M_USER>(oldEntity);

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
    }
}
