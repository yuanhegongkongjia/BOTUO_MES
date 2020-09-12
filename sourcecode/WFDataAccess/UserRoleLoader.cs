using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using WFCore;
using DynamicForm.Core;
using WFCommon;
using WFCommon.Utility;
using System.Text;
using WFCommon.VM;

namespace WFDataAccess
{
    public class UserRoleLoader
    {
        public static bool IsUserInRole(string UserId, string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                sql += " and UserId=@UserId and RoleId=@RoleId";
                var list = db.Query<WF_M_USERROLE>(sql, new { UserId = UserId, RoleId = RoleId }).ToList();
                return list.Count > 0;
                
                
            }
        }
        public static List<VM_WF_M_USERROLE> QueryByUserId(string UserId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select a.*,b.RoleName from WF_M_USERROLE a left outer join WF_M_ROLE b on a.RoleId=b.RoleId where 1=1";
                if (!string.IsNullOrWhiteSpace(UserId))
                {
                    sql += " and a.UserId=@UserId";
                }
                var list = db.Query<VM_WF_M_USERROLE>(sql, new { UserId = UserId }).ToList();
                return list;
            }
        }

        public static List<WF_M_USERROLE> Query(string UserId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(UserId))
                {
                    sql += " and UserId=@UserId";
                }
                var list = db.Query<WF_M_USERROLE>(sql, new { UserId = UserId }).ToList();
                return list;
            }
        }
        public static List<WF_M_USERROLE> QueryByRoleId(string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(RoleId))
                {
                    sql += " and RoleId=@RoleId";
                }
                var list = db.Query<WF_M_USERROLE>(sql, new { RoleId = RoleId }).ToList();
                return list;
            }
        }

        public static void Create(string UserId, string RoleId, string currentUserName)
        {
            using (var db = Pub.DB)
            {
                if (IsUserInRole(UserId, RoleId))
                {
                    return;
                }

                var entity = new WF_M_USERROLE();
                entity.UserRoleId = Guid.NewGuid().ToString();
                entity.UserId = UserId;
                entity.RoleId = RoleId;
                entity.CreateTime = DateTime.Now;
                entity.CreateUser = currentUserName;
                entity.LastModifyTime = DateTime.Now;
                entity.LastModifyUser = currentUserName;
                db.Insert<WF_M_USERROLE>(entity);
            }
        }
    }
}
