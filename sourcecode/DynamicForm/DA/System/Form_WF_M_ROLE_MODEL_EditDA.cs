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
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_WF_M_ROLE_MODEL_EditDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
        }

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var item = WF_M_ROLE_MODELLoader.Get(entity["ModelId"], entity["RoleId"]);
            if (item != null)
            {
                dict = DFDictionary.Create<WF_M_ROLE_MODEL>(item);
            }
            return dict;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] != "Edit")
            {
                return Insert(form, entity, ref message);
            }
            var oldEntity = WF_M_ROLE_MODELLoader.Get(entity["ModelId"], entity["RoleId"]);
            if (oldEntity == null)
            {
                //return Insert(form, entity, ref message);
                throw new WFException("要更新的记录不存在");
            }

            var newEntity = DFDictionary.Create<WF_M_ROLE_MODEL>(oldEntity).Merge(entity).To<WF_M_ROLE_MODEL>();
            var user = string.Empty;
            user = Util.GetCurrentUser().UserName;
            newEntity.LastModifyTime = DateTime.Now;
            newEntity.LastModifyUser = user;
            WF_M_ROLE_MODELLoader.Update(newEntity);
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            var item = WF_M_ROLE_MODELLoader.Get(entity["ModelId"], entity["RoleId"]);
            if (item != null)
            {
                throw new WFException("记录已经存在");
            }
            var newEntity = entity.To<WF_M_ROLE_MODEL>();

            var user = string.Empty;
            user = Util.GetCurrentUser().UserName;
            newEntity.CreateTime = DateTime.Now;
            newEntity.CreateUser = user;
            newEntity.LastModifyTime = DateTime.Now;
            newEntity.LastModifyUser = user;
            WF_M_ROLE_MODELLoader.Insert(newEntity);
            message = "新增成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
