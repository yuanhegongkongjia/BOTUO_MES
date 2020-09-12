using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.VM;
using WFCore;
using DynamicForm.Core;

namespace WFDataAccess
{
    public class WF_M_USERROLELoader
    {
        public static WF_M_USERROLE Get(string UserRoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                sql += " and UserRoleId=@UserRoleId";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserRoleId = UserRoleId
                };
                return db.Query<WF_M_USERROLE>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_WF_M_USERROLE> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserRoleId"]))
                {
                    sql += " and UserRoleId=@UserRoleId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserRoleId = dict["UserRoleId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                return db.Query<VM_WF_M_USERROLE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_USERROLE> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserRoleId"]))
                {
                    sql += " and UserRoleId=@UserRoleId";
                }
                if (!string.IsNullOrWhiteSpace(dict["RoleId"]))
                {
                    sql += " and RoleId=@RoleId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserRoleId = dict["UserRoleId"],
                    RoleId = dict["RoleId"]
                };
                return db.Query<WF_M_USERROLE>(sql, parameters).ToList();
            }
        }


        public static void Update(WF_M_USERROLE entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_USERROLE>(entity);
            }
        }

        public static void Insert(WF_M_USERROLE entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_USERROLE>(entity);
            }
        }

        public static void Delete(List<WF_M_USERROLE> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_USERROLE where 1=1";
                sql += " and UserRoleId=@UserRoleId";
                db.Execute(sql, list.Select(a => new
                {
                    UserRoleId = a.UserRoleId
                }));
            }
        }
    }
}
