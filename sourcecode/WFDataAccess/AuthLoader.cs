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
using System.Data;


namespace WFDataAccess
{
    public class AuthLoader
    {
        public static void SaveAuthData(string RoleId, string DeptTreeSelectedNode, string currentUser)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_AUTH_DATA where 1=1";
                sql += " and RoleId=@RoleId";
                db.Execute(sql, new { RoleId = RoleId });

                if (!string.IsNullOrWhiteSpace(DeptTreeSelectedNode))
                {
                    var list = DeptTreeSelectedNode.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (list != null && list.Count > 0)
                    {
                        db.Insert(list.Select(a => new WF_M_AUTH_DATA
                        {
                            AuthId = Guid.NewGuid().ToString(),
                            DeptId = a,
                            RoleId = RoleId,
                            CreateUser = currentUser,
                            CreateTime = DateTime.Now,
                            LastModifyUser = currentUser,
                            LastModifyTime = DateTime.Now
                        }));
                    }
                }
            }
        }

        public static void SaveAuthModule(string RoleId, string FunctionTreeSelectedNode, string currentUser)
        {
            using (var db = Pub.DB)
            {
                var sql = "delete from WF_M_AUTH_MODULE where 1=1";
                sql += " and RoleId=@RoleId";
                db.Execute(sql, new { RoleId = RoleId });

                if (!string.IsNullOrWhiteSpace(FunctionTreeSelectedNode))
                {
                    var list = FunctionTreeSelectedNode.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (list != null && list.Count > 0)
                    {
                        db.Insert(list.Select(a => new WF_M_AUTH_MODULE
                        {
                            AuthId = Guid.NewGuid().ToString(),
                            ModuleId = a,
                            RoleId = RoleId,
                            CreateUser = currentUser,
                            CreateTime = DateTime.Now,
                            LastModifyUser = currentUser,
                            LastModifyTime = DateTime.Now
                        }));
                    }
                }
            }
        }

        public static List<WF_M_AUTH_DATA> QueryAuthData(string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_AUTH_DATA where RoleId=@RoleId";
                return db.Query<WF_M_AUTH_DATA>(sql, new { RoleId = RoleId }).ToList();
            }
        }

        public static List<WF_M_AUTH_MODULE> QueryAuthModule(string RoleId)
        {
            using (var db = Pub.DB)
            {
                var sql = "select * from WF_M_AUTH_MODULE where RoleId=@RoleId";
                return db.Query<WF_M_AUTH_MODULE>(sql, new { RoleId = RoleId }).ToList();
            }
        }

        public static bool CheckMenuAccess(string userId, string formName)
        {
            var Href = string.Format("DFIndex.aspx?DF_FORMNAME={0}", formName);
            var form = MenuLoader.Query(string.Empty, string.Empty, Href).FirstOrDefault();
            if (form == null)
                return true;

            var list = QueryAuthModuleByUserId(userId);
            return list.Any(a => string.Compare(a.ModuleId, form.MenuId, true) == 0);
        }

        public static bool CheckFunctionAccess(List<WF_M_AUTH_MODULE> listFunction, List<WF_M_MENU> listMenu, string MenuName)
        {
            var menu = listMenu.FirstOrDefault(a => a.MenuId == MenuName);
            if (menu == null)
                return false;
            return listFunction.Any(a => string.Compare(a.ModuleId, menu.MenuId, true) == 0);

        }

        /// <summary>
        /// 查找用户所拥有的权限模块
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<WF_M_AUTH_MODULE> QueryAuthModuleByUserId(string userId)
        {
            if (userId == "admin")
            {
                return WF_M_MODULELoader.Query().Select(a => new WF_M_AUTH_MODULE() { ModuleId = a.ModuleId }).ToList();
            }
            return QueryAuthModule(UserRoleLoader.Query(userId).Select(a => a.RoleId).Distinct().ToList());
        }

        private static List<WF_M_AUTH_MODULE> QueryAuthModule(List<string> RoleIds)
        {
            using (var db = Pub.DB)
            {
                var sql = @"select a.* from WF_M_AUTH_MODULE a where 1=1";
                sql += " and a.RoleId in @RoleIds";
                // 在 Oracle 中，如果 RoleIds 里面没有任何项目，会报错，但是在 SQLServer 中是不报错的
                if (RoleIds.Count == 0)
                {
                    RoleIds.Add("-1");
                }
                return db.Query<WF_M_AUTH_MODULE>(sql, new { RoleIds = RoleIds }).ToList();
            }
        }

        internal static void DeleteAuthData(IDbConnection db, List<string> list)
        {
            var sql = "delete from WF_M_AUTH_DATA where RoleId=@RoleId";
            db.Execute(sql, list.Select(a => new { RoleId = a }));
        }

        internal static void DeleteAuthModule(IDbConnection db, List<string> list)
        {
            var sql = "delete from WF_M_AUTH_MODULE where RoleId=@RoleId";
            db.Execute(sql, list.Select(a => new { RoleId = a }));
        }

        /// <summary>
        /// 检查用户是否拥有某个模块的权限
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckFunctionAccess(string moduleName, string userId)
        {
            var item = WF_M_MODULELoader.Query(string.Empty, moduleName).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            return QueryAuthModuleByUserId(userId).Any(a => a.ModuleId == item.ModuleId);
        }
    }
}
