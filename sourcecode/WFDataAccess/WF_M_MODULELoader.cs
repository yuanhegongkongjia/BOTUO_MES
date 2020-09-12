using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCore;
using Dapper;
using DapperExtensions;
using WFCommon;

namespace WFDataAccess
{
    public class WF_M_MODULELoader
    {
        public static List<WF_M_MODULE> Query(string ModuleId = "", string ModuleName = "", string ModuleLabel = "")
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_MODULE where 1=1";
                if (!string.IsNullOrWhiteSpace(ModuleId))
                {
                    sql += " and ModuleId=@ModuleId";
                }
                if (!string.IsNullOrWhiteSpace(ModuleName))
                {
                    sql += " and ModuleName=@ModuleName";
                }
                if (!string.IsNullOrWhiteSpace(ModuleLabel))
                {
                    sql += " and ModuleLabel like @ModuleLabel";
                }
                var parameters = new
                {
                    ModuleId = ModuleId,
                    ModuleName = ModuleName,
                    ModuleLabel = string.Format("{0}___", ModuleLabel)
                };
                sql += " order by ModuleOrder";

                return db.Query<WF_M_MODULE>(sql, parameters).ToList();
            }
        }
        public static void Insert(WF_M_MODULE entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_MODULE>(entity);
            }
        }
        public static void Update(WF_M_MODULE entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_MODULE>(entity);
            }
        }
        public static void Delete(List<string> moduleIdList)
        {
            var list = Query();
            var sql = "delete from WF_M_MODULE where ModuleLabel like @ModuleLabel";
            using (var db = Pub.DB)
            {
                foreach (var item in moduleIdList)
                {
                    var entity = list.FirstOrDefault(a => a.ModuleId == item);
                    if (entity != null)
                    {
                        db.Execute(sql, new { ModuleLabel = string.Format("{0}%", entity.ModuleLabel) });
                    }
                }
            }
        }
    }
}
