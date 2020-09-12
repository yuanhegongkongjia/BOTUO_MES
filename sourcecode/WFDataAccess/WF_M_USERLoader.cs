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
    public class WF_M_USERLoader
    {
        public static WF_M_USER Get(string UserId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USER where 1=1";
                sql += " and UserId=@UserId";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserId = UserId
                };
                return db.Query<WF_M_USER>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_WF_M_USER> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select u.*,d.DeptName from WF_M_USER u
                            left join WF_M_DEPT d on d.DeptId=u.DeptId
                        where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserId"]))
                {
                    sql += " and u.UserId=@UserId";
                }
                if (!string.IsNullOrWhiteSpace(dict["UserName"]))
                {
                    sql += " and u.UserName like @UserName";
                }
                if (!string.IsNullOrWhiteSpace(dict["DeptId"]))
                {
                    sql += " and d.DeptId=@DeptId";
                }
                sql += " order by u.LastModifyTime desc";
                var parameters = new
                {
                    UserId = dict["UserId"],
                    DeptId=dict["DeptId"],
                    UserName=string.Format("%{0}%",dict["UserName"])
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                return db.Query<VM_WF_M_USER>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_USER> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_USER where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["UserId"]))
                {
                    sql += " and UserId=@UserId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    UserId = dict["UserId"]
                };
                return db.Query<WF_M_USER>(sql, parameters).ToList();
            }
        }


        public static void Update(WF_M_USER entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_USER>(entity);
            }
        }

        public static void Insert(WF_M_USER entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_USER>(entity);
            }
        }

        public static void Delete(List<WF_M_USER> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_USER where 1=1";
                sql += " and UserId=@UserId";
                db.Execute(sql, list.Select(a => new
                {
                    UserId = a.UserId
                }));
            }
        }
    }
}
