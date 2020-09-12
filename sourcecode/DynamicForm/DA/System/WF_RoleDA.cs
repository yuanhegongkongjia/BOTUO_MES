using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFCommon.VM;

namespace DynamicForm.DA
{
    public class WF_RoleDA : BaseDA
    {
        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                using (var db = Pub.DB)
                {
                    var sql = "delete from WF_M_ROLE where RoleId=@RoleId";
                    db.Execute(sql, data.Select(a => new { RoleId = a["RoleId"] }));
                    message = "删除成功".GetRes();

                }
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            try
            {
                /*基本查询语句*/
                var sql = @"select a.* from WF_M_ROLE a where 1=1";

                /*查询条件*/
                if (!string.IsNullOrWhiteSpace(entity["RoleName"]))
                {
                    sql += " and (a.RoleId like @RoleName or a.RoleName like @RoleName)";
                }
                sql += " order by RoleId";

                using (var db = Pub.DB)
                {
                    var parameters = new
                    {
                        RoleName = string.Format("%{0}%", entity["RoleName"])
                    };
                    vm.results = db.Query<int>(DFPub.GetCountSql(sql), parameters).FirstOrDefault();
                    var list = db.Query<VM_WF_M_ROLE>(DFPub.GetPageSql(sql, start + 1, start + limit), parameters).ToList();
                    vm.rows = list;
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                vm.hasError = true;
                vm.error = ex.Message;
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var dict = new DFDictionary();
                /*基本查询语句*/
                var sql = "select * from WF_M_ROLE where 1=1";

                /*查询条件*/

                using (var db = Pub.DB)
                {
                    var parameters = entity.To<WF_M_ROLE>();
                    var oldEntity = db.Query<WF_M_ROLE>(sql, parameters).FirstOrDefault();
                    if (oldEntity == null)
                    {
                        message = "记录已经不存在".GetRes();
                        return DFPub.EXECUTE_ERROR;
                    }
                    var newDict = DFDictionary.Create<WF_M_ROLE>(oldEntity);
                    // 把更新的值更新进去
                    newDict.Merge(entity);
                    // 将 Dictionary 转成 Entity
                    var newEntity = newDict.To<WF_M_ROLE>();
                    db.Update<WF_M_ROLE>(newEntity);
                    return DFPub.EXECUTE_SUCCESS;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
    }
}
