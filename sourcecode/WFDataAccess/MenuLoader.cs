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
    public class MenuLoader
    {
        public static List<Menu1VM> GetConfig(string currentUserId)
        {
            var auth = AuthLoader.QueryAuthModuleByUserId(currentUserId);
            using (var db = Pub.DB)
            {
                var menu1List = WF_M_MENULoader.Query(null, null, "ROOT").Select(a => new Menu1VM()
                    {
                        id = a.MenuId,
                        ModuleId = a.ModuleId,
                        text = a.ModuleDisplayText.GetRes(),
                        items = new List<Menu2VM>(),
                        icon = a.Icon,
                        collapsed = a.Expanded.GetValueOrDefault() == 1 ? false : true
                    }).ToList();
                Filter(menu1List, auth);
                foreach (var menu1 in menu1List)
                {
                    // 查询一级菜单的子菜单
                    menu1.items = WF_M_MENULoader.Query(null, null, menu1.id).Select(a => new Menu2VM()
                        {
                            childs = new List<Menu2VM>(),
                            href = a.ModuleLink,
                            id = a.MenuId,
                            ModuleId = a.ModuleId,
                            text = a.ModuleDisplayText.GetRes()
                        }).ToList();
                    Filter(menu1.items, auth);
                    foreach (var menu2 in menu1.items)
                    {
                        // 查询二级菜单的子菜单
                        menu2.childs = WF_M_MENULoader.Query(null, null, menu2.id).Select(a => new Menu2VM()
                        {
                            childs = new List<Menu2VM>(),
                            href = a.ModuleLink,
                            id = a.MenuId,
                            ModuleId = a.ModuleId,
                            text = a.ModuleDisplayText.GetRes()
                        }).ToList();
                        Filter(menu2.childs, auth);
                    }
                }
                return menu1List;
            }
        }
        private static void Filter(List<Menu1VM> list1, List<WF_M_AUTH_MODULE> auth)
        {
            var removeList = list1.Where(a => !auth.Any(b => b.ModuleId == a.ModuleId)).ToList();
            foreach (var item in removeList)
            {
                list1.Remove(item);
            }
        }
        private static void Filter(List<Menu2VM> list1, List<WF_M_AUTH_MODULE> auth)
        {
            var removeList = list1.Where(a => !auth.Any(b => b.ModuleId == a.ModuleId)).ToList();
            foreach (var item in removeList)
            {
                list1.Remove(item);
            }
        }
        public static List<WF_M_MENU> Query(string MenuId = "", string MenuLabel = "", string Href = "")
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_MENU where 1=1";
                if (!string.IsNullOrWhiteSpace(MenuId))
                {
                    sql += " and MenuId=@MenuId";
                }
                if (!string.IsNullOrWhiteSpace(MenuLabel))
                {
                    sql += " and MenuLabel like @MenuLabel";
                }
                if (!string.IsNullOrWhiteSpace(Href))
                {
                    sql += " and Href=@Href";
                }
                sql += " order by MenuLabel";
                return db.Query<WF_M_MENU>(sql, new
                {
                    MenuId = MenuId,
                    Href = Href,
                    MenuLabel = string.Format("{0}___", MenuLabel),
                }).ToList();
            }
        }
    }
}
