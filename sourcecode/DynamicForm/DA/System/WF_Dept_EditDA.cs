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
    public class WF_Dept_EditDA : BaseDA
    {
        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var item = Get(GetSelectSql("WF_M_DEPT"), new { DeptId = entity["DeptId"] });
            if (item != null)
            {
                dict = item.ToDFDictionary();
            }
            return dict;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            var oldEntity = Get(GetSelectSql("WF_M_DEPT"), new { DeptId = entity["DeptId"] });
            if (oldEntity == null)
            {
                return Insert(form, entity, ref message);
            }
            CheckInput(form, entity);
            var newEntity = oldEntity.ToDFDictionary().Merge(entity);
            CheckData("WF_M_DEPT", newEntity, Util.GetCurrentUser().UserName);

            var parent = WF_M_DEPTLoader.Query(newEntity["PDeptId"]).FirstOrDefault();
            var siblings = WF_M_DEPTLoader.Query(null, null, parent.DeptLabel).ToList();
            newEntity.Add("DeptLabel", LabelHelper.GetNextLabelUsingSubLabelList(parent.DeptLabel, siblings.Select(a => a.DeptLabel).ToList()));

            SaveData("WF_M_DEPT", newEntity, IMPORT_TYPE_UPDATE);
            
            message = "保存成功".GetRes();
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (entity["action"] == "querylist")
            {
                vm.rows = WF_M_DEPTLoader.Query(entity["DeptId"]);
                return DFPub.EXECUTE_SUCCESS;
            }
            throw new Exception("无效的 action".GetRes());
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            var item = Get(GetSelectSql("WF_M_DEPT"), new { DeptId = entity["DeptId"] });
            if (item != null)
            {
                throw new WFException("记录已经存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = entity;

            CheckData("WF_M_DEPT", newEntity, Util.GetCurrentUser().UserName);

            var parent = WF_M_DEPTLoader.Query(newEntity["PDeptId"]).FirstOrDefault();
            var siblings = WF_M_DEPTLoader.Query(null, null, parent.DeptLabel).ToList();
            newEntity.Add("DeptLabel", LabelHelper.GetNextLabelUsingSubLabelList(parent.DeptLabel, siblings.Select(a => a.DeptLabel).ToList()));

            SaveData("WF_M_DEPT", newEntity, IMPORT_TYPE_INSERT);

            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');".GetRes());
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }

    }
}
