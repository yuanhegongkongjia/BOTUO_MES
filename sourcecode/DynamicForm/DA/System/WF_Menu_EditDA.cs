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
using WFCommon.VM;

namespace DynamicForm.DA
{
    public class WF_Menu_EditDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(entity["MenuId"]))
                {
                    var item = WF_M_MENULoader.Query(entity["MenuId"]).FirstOrDefault();
                    if (item != null)
                    {
                        return DFDictionary.Create<VM_WF_M_MENU>(item);
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return null;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] != "Edit")
            {
                return Insert(form, entity, ref  message);
            }
            try
            {
                if (string.IsNullOrWhiteSpace(entity["MenuId"]))
                {
                    throw new Exception("请传入 MenuId 参数".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["ModuleId"]))
                {
                    throw new Exception("请选择对应的模块".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["PMenuId"]))
                {
                    throw new Exception("请选择父菜单".GetRes());
                }
                var oldEntity = WF_M_MENULoader.Query(entity["MenuId"]).FirstOrDefault();
                if (oldEntity == null)
                {
                    throw new Exception("找不到要更新的记录".GetRes());
                }

                // 先将数据库查出来的老的实体转成字典，然后把客户端传过来的字典合并进去，这样就实现了数据的更新
                var newEntity = DFDictionary.Create<WF_M_MENU>(oldEntity).Merge(entity).To<WF_M_MENU>();
                var user = Util.GetCurrentUser();
                var parent = WF_M_MENULoader.Query(entity["PMenuId"]).FirstOrDefault();
                var siblings = WF_M_MENULoader.Query(null, parent.MenuLabel).ToList();
                newEntity.MenuLabel = LabelHelper.GetNextLabelUsingSubLabelList(parent.MenuLabel, siblings.Select(a => a.MenuLabel).ToList());
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;
                newEntity.Expanded = ParseHelper.ParseInt(entity["Expanded"]);

                WF_M_MENULoader.Update(newEntity);
                message = "保存成功".GetRes();
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
            if (entity["action"] == "querylist")
            {
                vm.rows = WF_M_MENULoader.Query(entity["MenuId"]);
                return DFPub.EXECUTE_SUCCESS;
            }
            throw new Exception("无效的 action.GetRes()");
        }

        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity["ModuleId"]))
                {
                    throw new Exception("请选择模块".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["PMenuId"]))
                {
                    throw new Exception("请选择父菜单".GetRes());
                }

                var parent = WF_M_MENULoader.Query(entity["PMenuId"]).FirstOrDefault();
                var siblings = WF_M_MENULoader.Query(null, parent.MenuLabel).ToList();

                var newEntity = entity.To<WF_M_MENU>();
                newEntity.MenuId = Guid.NewGuid().ToString();
                var user = Util.GetCurrentUser();
                newEntity.CreateTime = DateTime.Now;
                newEntity.CreateUser = user.UserName;
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;
                newEntity.MenuLabel = LabelHelper.GetNextLabelUsingSubLabelList(parent.MenuLabel, siblings.Select(a => a.MenuLabel).ToList());

                WF_M_MENULoader.Insert(newEntity);
                message = "新增成功".GetRes();
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
