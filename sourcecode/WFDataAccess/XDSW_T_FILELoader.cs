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
    public class XDSW_T_FILELoader
    {
        /// <summary>
        /// …Ë÷√ FileName
        /// </summary>
        /// <param name="FileId"></param>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        public static void SetFileName(string FileId, DFDictionary dict, string key)
        {
            var entity = Get(FileId);
            if (entity != null)
            {
                dict.Add(key, entity.FileName);
            }
        }

        public static XDSW_T_FILE Get(string FileId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select top 1 * from XDSW_T_FILE where 1=1";
                sql += " and FileId=@FileId";
                var parameters = new
                {
                    FileId = FileId
                };
                return db.Query<XDSW_T_FILE>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_XDSW_T_FILE> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from XDSW_T_FILE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["FileId"]))
                {
                    sql += " and FileId=@FileId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    FileId = dict["FileId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<VM_XDSW_T_FILE>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<XDSW_T_FILE> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from XDSW_T_FILE where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["FileId"]))
                {
                    sql += " and FileId=@FileId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    FileId = dict["FileId"]
                };
                return db.Query<XDSW_T_FILE>(sql, parameters).ToList();
            }
        }


        public static void Update(XDSW_T_FILE entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<XDSW_T_FILE>(entity);
            }
        }

        public static void Insert(XDSW_T_FILE entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<XDSW_T_FILE>(entity);
            }
        }

        public static void Delete(List<XDSW_T_FILE> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from XDSW_T_FILE where 1=1";
                sql += " and FileId=@FileId";
                db.Execute(sql, list.Select(a => new
                {
                    FileId = a.FileId
                }));
            }
        }
    }
}
