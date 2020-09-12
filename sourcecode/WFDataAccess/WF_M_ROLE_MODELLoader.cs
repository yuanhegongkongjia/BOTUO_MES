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
    public class WF_M_ROLE_MODELLoader
    {
        public static WF_M_ROLE_MODEL Get(string ModelId, string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE_MODEL where 1=1";
                sql += " and ModelId=@ModelId";
                sql += " and RoleId=@RoleId";
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    ModelId = ModelId,
                    RoleId = RoleId
                };
                return db.Query<WF_M_ROLE_MODEL>(sql, parameters).FirstOrDefault();
            }
        }
        public static List<VM_WF_M_ROLE_MODEL> GetList(DFDictionary dict, ref int count, int start, int limit)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select rm.*,m.modelname,r.rolename from WF_M_ROLE_MODEL rm
                    left join wf_m_model m on rm.modelid=m.modelid
                    left join wf_m_role r on r.roleid=rm.roleid        
                    where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["ModelId"]))
                {
                    sql += " and rm.ModelId=@ModelId";
                }
                if (!string.IsNullOrWhiteSpace(dict["RoleId"]))
                {
                    sql += " and rm.RoleId=@RoleId";
                }
                sql += " order by m.ModelName";
                var parameters = new
                {
                    ModelId = dict["ModelId"],
                    RoleId = dict["RoleId"]
                };
                count = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();

                return db.Query<VM_WF_M_ROLE_MODEL>(DFPub.GetPageSql(db, sql, start + 1, start + limit), parameters).ToList();
            }
        }

        public static List<WF_M_ROLE_MODEL> Query(DFDictionary dict)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_ROLE_MODEL where 1=1";
                if (!string.IsNullOrWhiteSpace(dict["ModelId"]))
                {
                    sql += " and ModelId=@ModelId";
                }
                if (!string.IsNullOrWhiteSpace(dict["RoleId"]))
                {
                    sql += " and RoleId=@RoleId";
                }
                sql += " order by LastModifyTime desc";
                var parameters = new
                {
                    ModelId = dict["ModelId"],
                    RoleId = dict["RoleId"]
                };
                return db.Query<WF_M_ROLE_MODEL>(sql, parameters).ToList();
            }
        }

        /// <summary>
        /// 根据用户编号得到用户的有权限的流程列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<string> GetRoleModel(string UserId)
        {
            using (var db = Pub.DB)
            {
                var sql = @"SELECT distinct a.ModelId FROM dbo.WF_M_ROLE_MODEL a
LEFT OUTER JOIN dbo.WF_M_USERROLE b ON a.RoleId=b.RoleId
WHERE b.UserId=@UserId";
                var parameters = new
                {
                    UserId = UserId
                };
                var list = db.Query<string>(sql, parameters).ToList();
                if (UserId == "admin")
                {
                    sql = "SELECT ModelId FROM dbo.WF_M_MODEL";
                    list = db.Query<string>(sql).ToList();
                }
                return list;
            }
        }

        public static void Update(WF_M_ROLE_MODEL entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_ROLE_MODEL>(entity);
            }
        }

        public static void Insert(WF_M_ROLE_MODEL entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_ROLE_MODEL>(entity);
            }
        }

        public static void Delete(List<WF_M_ROLE_MODEL> list)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_ROLE_MODEL where 1=1";
                sql += " and ModelId=@ModelId";
                sql += " and RoleId=@RoleId";
                db.Execute(sql, list.Select(a => new
                {
                    ModelId = a.ModelId,
                    RoleId = a.RoleId
                }));
            }
        }
    }
}
