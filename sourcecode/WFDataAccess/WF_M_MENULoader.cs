using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCore;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.VM;

namespace WFDataAccess
{
    public class WF_M_MENULoader
    {
        public static List<VM_WF_M_MENU> Query(string MenuId = "", string MenuLabel = "", string PMenuId = "")
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.*,b.ModuleName,b.ModuleDisplayText,b.ModuleLink from WF_M_MENU a
join WF_M_MODULE b on a.ModuleId=b.ModuleId where 1=1";
                if (!string.IsNullOrWhiteSpace(MenuId))
                {
                    sql += " and a.MenuId=@MenuId";
                }
                if (!string.IsNullOrWhiteSpace(MenuLabel))
                {
                    sql += " and a.MenuLabel like @MenuLabel";
                }
                if (!string.IsNullOrWhiteSpace(PMenuId))
                {
                    sql += " and a.PMenuId=@PMenuId";
                }
                var parameters = new
                {
                    MenuId = MenuId,
                    PMenuId = PMenuId,
                    MenuLabel = string.Format("{0}___", MenuLabel)
                };
                sql += " order by a.MenuOrder";

                return db.Query<VM_WF_M_MENU>(sql, parameters).ToList();
            }
        }
        public static void Insert(WF_M_MENU entity)
        {
            using (var db = Pub.DB)
            {
                db.Insert<WF_M_MENU>(entity);
            }
        }
        public static void Update(WF_M_MENU entity)
        {
            using (var db = Pub.DB)
            {
                db.Update<WF_M_MENU>(entity);
            }
        }
        public static void Delete(List<string> MenuIdList)
        {
            var list = Query();
            var sql = "delete from WF_M_MENU where MenuLabel like @MenuLabel";
            using (var db = Pub.DB)
            {
                foreach (var item in MenuIdList)
                {
                    var entity = list.FirstOrDefault(a => a.MenuId == item);
                    if (entity != null)
                    {
                        db.Execute(sql, new { MenuLabel = string.Format("{0}%", entity.MenuLabel) });
                    }
                }
            }
        }
    }
}
