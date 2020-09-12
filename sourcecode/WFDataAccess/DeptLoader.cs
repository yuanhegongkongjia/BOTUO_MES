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
    public class DeptLoader
    {
        public static WF_M_DEPT Get(string DeptId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_DEPT where 1=1";
                if (!string.IsNullOrWhiteSpace(DeptId))
                {
                    sql += " and DeptId=@DeptId";
                }
              
                sql += " order by DeptLabel";
                return db.Query<WF_M_DEPT>(sql, new
                {
                    DeptId = DeptId
                }).FirstOrDefault();
            }
        }
        
        
        public static List<WF_M_DEPT> Query(string DeptId = "", string DeptLabel = "")
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_DEPT where 1=1";
                if (!string.IsNullOrWhiteSpace(DeptId))
                {
                    sql += " and DeptId=@DeptId";
                }
                if (!string.IsNullOrWhiteSpace(DeptLabel))
                {
                    sql += " and DeptLabel like @DeptLabel";
                }
                sql += " order by DeptLabel";
                return db.Query<WF_M_DEPT>(sql, new
                {
                    DeptId = DeptId,
                    DeptLabel = string.Format("{0}%", DeptLabel)
                }).ToList();
            }
        }

        /// <summary>
        /// 得到部门列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="includeSelf">是否包含自身，默认是不包含</param>
        /// <param name="recursive">是否递归子部门</param>
        /// <returns></returns>
        public static List<WF_M_DEPT> GetList(string deptId, bool includeSelf = false, bool recursive = false)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_DEPT where DeptId=@DeptId";
                var dept = db.Query<WF_M_DEPT>(sql, new { DeptId = deptId }).FirstOrDefault();
                if (dept == null)
                    return new List<WF_M_DEPT>();

                sql = "select * from WF_M_DEPT where DeptLabel like @DeptLabel order by DeptLabel";
                var label = recursive ? string.Format("{0}%", dept.DeptLabel) : dept.DeptLabel;
                var list = db.Query<WF_M_DEPT>(sql, new { DeptLabel = label }).ToList();
                if (!includeSelf)
                {
                    var self = list.FirstOrDefault(a => a.DeptId == deptId);
                    if (self != null) list.Remove(self);
                }
                return list;
            }
        }
    }
}
