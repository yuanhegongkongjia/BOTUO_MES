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
    public class WF_M_METADATALoader
    {
        public static WF_M_METADATA Get(string TableName, string ColumnName)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_METADATA where 1=1";
                sql += " and TableName=@TableName";
                sql += " and ColumnName=@ColumnName";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    TableName = TableName,
                    ColumnName = ColumnName
                };
                return db.Query<WF_M_METADATA>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_WF_M_METADATA> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_METADATA where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["TableName"]))
                {
                    sql += " and TableName=@TableName";
                }
                if (!string.IsNullOrWhiteSpace(dict["ColumnName"]))
                {
                    sql += " and ColumnName=@ColumnName";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    TableName = dict["TableName"],
                    ColumnName = dict["ColumnName"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<VM_WF_M_METADATA>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_METADATA> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_METADATA where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["TableName"]))
                {
                    sql += " and TableName=@TableName";
                }
                if (!string.IsNullOrWhiteSpace(dict["ColumnName"]))
                {
                    sql += " and ColumnName=@ColumnName";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    TableName = dict["TableName"],
                    ColumnName = dict["ColumnName"]
                };
                return db.Query<WF_M_METADATA>(sql, parameters).ToList();
            }
        }


        public static void Update(WF_M_METADATA entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_METADATA>(entity);
            }
        }

        public static void Insert(WF_M_METADATA entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_METADATA>(entity);
            }
        }

        public static void Delete(List<WF_M_METADATA> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_METADATA where 1=1";
                sql += " and TableName=@TableName";
                sql += " and ColumnName=@ColumnName";
                db.Execute(sql, list.Select(a => new
                {
                    TableName = a.TableName,
                    ColumnName = a.ColumnName
                }));
            }
        }

        public static List<WF_M_METADATA> QueryByTableName(string TableName)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_METADATA where 1=1";
                sql += " and TableName=@TableName";
                sql += " order by ColumnOrder";
                var parameters = new
                {
                    TableName = TableName
                };
                return db.Query<WF_M_METADATA>(sql, parameters).ToList();
            }
        }
    }
}
