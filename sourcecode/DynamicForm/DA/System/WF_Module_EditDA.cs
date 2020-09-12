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
    public class WF_Module_EditDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(entity["ModuleId"]))
                {
                    var item = WF_M_MODULELoader.Query(entity["ModuleId"]).FirstOrDefault();
                    if (item != null)
                    {
                        return DFDictionary.Create<WF_M_MODULE>(item);
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
                if (string.IsNullOrWhiteSpace(entity["ModuleId"]))
                {
                    throw new Exception("请传入 ModuleId 参数".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["ModuleName"]))
                {
                    throw new Exception("模块名称不能为空".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["PModuleId"]))
                {
                    throw new Exception("请选择父模块".GetRes());
                }
                var oldEntity = WF_M_MODULELoader.Query(entity["ModuleId"]).FirstOrDefault();
                if (oldEntity == null)
                {
                    throw new Exception("找不到要更新的记录".GetRes());
                }

                // 先将数据库查出来的老的实体转成字典，然后把客户端传过来的字典合并进去，这样就实现了数据的更新
                var newEntity = DFDictionary.Create<WF_M_MODULE>(oldEntity).Merge(entity).To<WF_M_MODULE>();
                var user = Util.GetCurrentUser();
                var parent = WF_M_MODULELoader.Query(entity["PModuleId"]).FirstOrDefault();
                var siblings = WF_M_MODULELoader.Query(null, null, parent.ModuleLabel).ToList();
                newEntity.ModuleLabel = LabelHelper.GetNextLabelUsingSubLabelList(parent.ModuleLabel, siblings.Select(a => a.ModuleLabel).ToList());
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;

                WF_M_MODULELoader.Update(newEntity);
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
                vm.rows = WF_M_MODULELoader.Query(entity["ModuleId"]);
                return DFPub.EXECUTE_SUCCESS;
            }
            throw new Exception("无效的 action".GetRes());
        }

        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(entity["ModuleName"]))
                {
                    throw new Exception("模块名称不能为空".GetRes());
                }
                if (string.IsNullOrWhiteSpace(entity["PModuleId"]))
                {
                    throw new Exception("请选择父模块".GetRes());
                }
                var oldEntity = WF_M_MODULELoader.Query(null, entity["ModuleName"]).FirstOrDefault();
                if (oldEntity != null)
                {
                    throw new Exception("模块名称已经存在".GetRes());
                }

                var parent = WF_M_MODULELoader.Query(entity["PModuleId"]).FirstOrDefault();
                var siblings = WF_M_MODULELoader.Query(null, null, parent.ModuleLabel).ToList();

                var newEntity = entity.To<WF_M_MODULE>();
                newEntity.ModuleId = Guid.NewGuid().ToString();
                var user = Util.GetCurrentUser();
                newEntity.CreateTime = DateTime.Now;
                newEntity.CreateUser = user.UserName;
                newEntity.LastModifyTime = DateTime.Now;
                newEntity.LastModifyUser = user.UserName;
                newEntity.ModuleLabel = LabelHelper.GetNextLabelUsingSubLabelList(parent.ModuleLabel, siblings.Select(a => a.ModuleLabel).ToList());

                WF_M_MODULELoader.Insert(newEntity);
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
