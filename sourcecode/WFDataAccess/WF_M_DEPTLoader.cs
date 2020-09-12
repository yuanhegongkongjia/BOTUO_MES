using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCore;
using Dapper;
using DapperExtensions;
using WFCommon;
using DynamicForm.Core;

namespace WFDataAccess
{
    public class WF_M_DEPTLoader
    {
        public static List<WF_M_DEPT> Query(string DeptId = "", string DeptName = "", string DeptLabel = "")
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_DEPT where 1=1";
                if (!string.IsNullOrWhiteSpace(DeptId))
                {
                    sql += " and DeptId=@DeptId";
                }
                if (!string.IsNullOrWhiteSpace(DeptName))
                {
                    sql += " and DeptName=@DeptName";
                }
                if (!string.IsNullOrWhiteSpace(DeptLabel))
                {
                    sql += " and DeptLabel like @DeptLabel";
                }
                var parameters = new
                {
                    DeptId = DeptId,
                    DeptName = DeptName,
                    DeptLabel = string.Format("{0}___", DeptLabel)
                };
                sql += " order by DeptOrder";

                return db.Query<WF_M_DEPT>(sql, parameters).ToList();
            }
        }
        public static void Insert(WF_M_DEPT entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_DEPT>(entity);
            }
        }
        public static void Update(WF_M_DEPT entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_DEPT>(entity);
            }
        }

        public static List<WF_M_DEPT> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_DEPT where 1=1";

                var parameters = new
                {
                    DeptId = QueryBuilder.Like(ref sql, dict, "DeptId", "DeptId"),
                    DeptName = QueryBuilder.Like(ref sql, dict, "DeptName", "DeptName")
                };
                sql += " order by LastModifyTime desc";
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<WF_M_DEPT>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static void Delete(List<string> moduleIdList)
        {
            var list = Query();
            var sql = "delete from WF_M_DEPT where DeptLabel like @DeptLabel";
            using (var db = Pub.DB)
            {
                foreach (var item in moduleIdList)
                {
                    var entity = list.FirstOrDefault(a => a.DeptId == item);
                    if (entity != null)
                    {
                        db.Execute(sql, new { DeptLabel = string.Format("{0}%", entity.DeptLabel) });
                    }
                }
            }
        }
    }
}
