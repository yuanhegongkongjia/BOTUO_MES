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
using DynamicForm;
using WFCommon.VM;

namespace WFDataAccess
{
    public class UserLoader
    {
        public static string GetUserHash(string user)
        {
            using (var db = Pub.DB)
            {
                var entityUser = db.Query<WF_M_USER>("select * from WF_M_USER where UserId=@UserId", new { UserId = user }).FirstOrDefault();
                if (entityUser == null)
                {
                    return string.Empty;
                }
                else
                {
                    return entityUser.Password;
                }
            }
        }

        public static VM_WF_M_USER GetUserById(string userId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,b.DeptName,b.DeptLabel from WF_M_USER a
left outer join WF_M_DEPT b on a.DeptId=b.DeptId where 1=1 and a.UserId=@UserId";
                var entity = db.Query<VM_WF_M_USER>(sql, new { UserId = userId }).FirstOrDefault();
                if (entity == null)
                {
                    entity = new VM_WF_M_USER();
                }
                return entity;
            }
        }

        public static VM_WF_M_USER GetUserByName(string userName)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USER where UserName=@UserName";
                var entity = db.Query<VM_WF_M_USER>(sql, new { UserName = userName }).FirstOrDefault();
                return entity;
            }
        }

        public static void Update(string userId, DateTime lastLoginTime)
        {
            using (var db = Pub.DB)
            {
                var sql = "update WF_M_USER set LastLoginTime=@LastLoginTime where UserId=@UserId";
                db.Execute(sql, new { LastLoginTime = lastLoginTime, UserId = userId });
            }
        }

        public static List<VM_WF_M_USER> QueryUsersByDeptId(string DeptId)
        {
            using (var db = Pub.DB)
            {
                var dept = DeptLoader.Query(DeptId).FirstOrDefault();
                if (dept == null)
                    return new List<VM_WF_M_USER>();

                var list = DeptLoader.Query(string.Empty, dept.DeptLabel).Select(a => a.DeptId).ToList();
                if (list.Count == 0)
                    list.Add("-1");

                var sql = @"select u.*,e.EmployeeName from WF_M_USER u
left outer join WF_M_EMPLOYEE e on e.EmployeeId=u.EmployeeId where 1=1 and e.DeptId in :DeptId";
                return db.Query<VM_WF_M_USER>(sql, new { DeptId = list }).ToList();
            }
        }

        public static List<WF_M_USER> Query(string EmployeeId = "")
        {
            using (var db = Pub.DB)
            {
                var sql = @"select * from WF_M_USER where 1=1";
                if (!string.IsNullOrWhiteSpace(EmployeeId))
                {
                    sql += " and EmployeeId=@EmployeeId";
                }
                return db.Query<WF_M_USER>(sql, new { EmployeeId = EmployeeId }).ToList();
            }
        }
    }
}
