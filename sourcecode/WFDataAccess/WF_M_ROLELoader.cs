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
    public class WF_M_ROLELoader
    {
        public static WF_M_ROLE Get(string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE where 1=1";
                sql += " and RoleId=@RoleId";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    RoleId = RoleId
                };
                return db.Query<WF_M_ROLE>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_WF_M_ROLE> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["RoleId"]))
                {
                    sql += " and RoleId=@RoleId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    RoleId = dict["RoleId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                return db.Query<VM_WF_M_ROLE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_ROLE> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["RoleId"]))
                {
                    sql += " and RoleId=@RoleId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    RoleId = dict["RoleId"]
                };
                return db.Query<WF_M_ROLE>(sql, parameters).ToList();
            }
        }


        public static void Update(WF_M_ROLE entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_ROLE>(entity);
            }
        }

        public static void Insert(WF_M_ROLE entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_ROLE>(entity);
            }
        }

        public static void Delete(List<WF_M_ROLE> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_ROLE where 1=1";
                sql += " and RoleId=@RoleId";
                db.Execute(sql, list.Select(a => new
                {
                    RoleId = a.RoleId
                }));
            }
        }
    }
}
