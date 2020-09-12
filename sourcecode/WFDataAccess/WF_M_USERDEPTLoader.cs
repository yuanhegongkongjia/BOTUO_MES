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
    public class WF_M_USERDEPTLoader
    {
        public static WF_M_USERDEPT Get(string UserDeptId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERDEPT where 1=1";
                sql += " and UserDeptId=@UserDeptId";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserDeptId = UserDeptId
                };
                return db.Query<WF_M_USERDEPT>(sql, parameters).FirstOrDefault();
            }
        }

        public static List<VM_WF_M_USERDEPT> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERDEPT where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserDeptId"]))
                {
                    sql += " and UserDeptId=@UserDeptId";
                }
                if (!string.IsNullOrWhiteSpace(dict["UserId"]))
                {
                    sql += " and UserId=@UserId";
                }
                if (!string.IsNullOrWhiteSpace(dict["DeptId"]))
                {
                    sql += " and DeptId=@DeptId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserDeptId = dict["UserDeptId"],
                    UserId=dict["UserId"],
                    DeptId=dict["DeptId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<VM_WF_M_USERDEPT>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<VM_WF_M_USERDEPT> GetListForUpdate(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,(case when c.DeptId is not null then 1 else 0 end) as selected from WF_M_DEPT a
LEFT OUTER JOIN WF_M_USERDEPT c ON a.DeptId=c.DeptId AND c.UserId=@UserId";
                if (!string.IsNullOrWhiteSpace(dict["UserDeptId"]))
                {
                    sql += " and c.UserDeptId=@UserDeptId";
                }
                if (!string.IsNullOrWhiteSpace(dict["UserId"]))
                {
                    sql += " and c.UserId=@UserId";
                }
                if (!string.IsNullOrWhiteSpace(dict["DeptId"]))
                {
                    sql += " and c.DeptId=@DeptId";
                }
                sql += " order by a.Extend01";
                var parameters = new
                {
                    UserDeptId = dict["UserDeptId"],
                    UserId = dict["UserId"],
                    DeptId = dict["DeptId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<VM_WF_M_USERDEPT>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_USERDEPT> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USERDEPT where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserDeptId"]))
                {
                    sql += " and UserDeptId=@UserDeptId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserDeptId = dict["UserDeptId"]
                };
                return db.Query<WF_M_USERDEPT>(sql, parameters).ToList();
            }
        }


        public static void Update(WF_M_USERDEPT entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_USERDEPT>(entity);
            }
        }

        public static void Insert(WF_M_USERDEPT entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_USERDEPT>(entity);
            }
        }

        public static void Delete(List<WF_M_USERDEPT> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_USERDEPT where 1=1";
                sql += " and PK_GUID=@PK_GUID";
                db.Execute(sql, list.Select(a => new
                {
                    PK_GUID = a.PK_GUID
                }));
            }
        }

        public static void DeleteByUserId(string UserId)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_USERDEPT where 1=1";
                sql += " and UserId=@UserId";
                db.Execute(sql, new { UserId = UserId });
            }
        }
    }
}
