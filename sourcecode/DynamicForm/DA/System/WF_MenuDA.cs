using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class WF_MenuDA : BaseDA
    {
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["action"] == "querylist")
            {
                vm.rows = QueryTree(entity);
                return DFPub.EXECUTE_SUCCESS;
            }
            throw new NotSupportedException("不支持动作".GetRes());
        }


        private List<WFTreeNode> QueryTree(DFDictionary dict)
        {
            var root = WF_M_MENULoader.Query("ROOT").FirstOrDefault();
            var nodes = WF_M_MENULoader.Query(string.Empty).OrderBy(a => a.MenuOrder.GetValueOrDefault()).Select(a => new WFTreeNode()
            {
                pid = a.PMenuId,
                id = a.MenuId,
                text = string.Format("{0} {1} {2}", a.MenuOrder.GetValueOrDefault(), a.ModuleName, a.ModuleDisplayText.GetRes())
            }).ToList();
            var listStatus = JsonSerializeHelper.DeserializeObject<List<WFTreeNode>>(Base64StringHelper.ConvertFromBase64String(dict["DF_TREE_VIEWSTATE"]));
            var list = WFTreeHelper.GenerateTree("ROOT", nodes);
            WFTreeHelper.SetStatus(list, listStatus);
            return list;
        }

        public override int Delete(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                var data = JsonSerializeHelper.DeserializeObject<List<Dictionary<string, string>>>(entity["data"]);
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                WF_M_MENULoader.Delete(data.Select(a => a["MenuId"]).ToList());
                message = "删除成功".GetRes();
                return DFPub.EXECUTE_SUCCESS;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return DFPub.EXECUTE_ERROR;
            }
        }
    }
}
