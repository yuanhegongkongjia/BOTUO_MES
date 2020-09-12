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

namespace WFDataAccess
{
    public class RoleLoader
    {
        public static List<WF_M_ROLE> Query(string RoleId = "", string RoleName = "", string RoleType = "")
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(RoleId))
                {
                    sql += " and RoleId=@RoleId";
                }
                if (!string.IsNullOrWhiteSpace(RoleName))
                {
                    sql += " and RoleName=@RoleName";
                }
                if (!string.IsNullOrWhiteSpace(RoleType))
                {
                    sql += " and RoleType=@RoleType";
                }
                return db.Query<WF_M_ROLE>(sql, new
                {
                    RoleId = RoleId,
                    RoleName = RoleName,
                    RoleType = RoleType
                }).ToList();
            }
        }

        public static void Delete(List<string> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_ROLE where RoleId=@RoleId";
                db.Execute(sql, list.Select(a => new { RoleId = a }));

                AuthLoader.DeleteAuthData(db, list);
                AuthLoader.DeleteAuthModule(db, list);
            }
        }

        public static bool GetByUserId(string UserId,string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select * from WF_M_USERROLE where RoleId=@RoleId and UserId=@UserId";
                var item = db.Query<WF_M_USERROLE>(sql,new { RoleId=RoleId,UserId=UserId}).FirstOrDefault();
                if (item == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
