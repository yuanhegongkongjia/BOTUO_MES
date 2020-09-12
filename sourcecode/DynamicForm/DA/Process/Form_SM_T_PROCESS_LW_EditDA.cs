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
    public class Form_SM_T_PROCESS_LW_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_LW"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { PKId = entity["PKId"] };
        }

        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
            var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
            if (oldEntity != null)
            {
                this.Model.Add("EditMode", "Edit");
            }
        }

        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            if (entity["Line"] == "L01")
            {
                if (string.IsNullOrWhiteSpace(entity["Z6Standard"]) || string.IsNullOrWhiteSpace(entity["Z6Max"]) || string.IsNullOrWhiteSpace(entity["Z6Min"]))
                {
                    throw new WFException("Z6温度信息必须填写");
                }
            }


            //炉压设定检查
            var Z1Standard = ParseHelper.ParseDecimal(entity["Z1Standard"]);
            if (!Z1Standard.HasValue)
            {
                throw new WFException("Z1标准值必须是数字");
            }
            var Z1Max = ParseHelper.ParseDecimal(entity["Z1Max"]);
            if (!Z1Max.HasValue)
            {
                throw new WFException("Z1最大值必须是数字");
            }
            var Z1Min = ParseHelper.ParseDecimal(entity["Z1Min"]);
            if (!Z1Min.HasValue)
            {
                throw new WFException("Z1最小值必须是数字");
            }
            var Z2Standard = ParseHelper.ParseDecimal(entity["Z2Standard"]);
            if (!Z2Standard.HasValue)
            {
                throw new WFException("Z2标准值必须是数字");
            }
            var Z2Max = ParseHelper.ParseDecimal(entity["Z2Max"]);
            if (!Z2Max.HasValue)
            {
                throw new WFException("Z2最大值必须是数字");
            }
            var Z2Min = ParseHelper.ParseDecimal(entity["Z2Min"]);
            if (!Z2Min.HasValue)
            {
                throw new WFException("Z2最小值必须是数字");
            }
            var Z3Standard = ParseHelper.ParseDecimal(entity["Z3Standard"]);
            if (!Z3Standard.HasValue)
            {
                throw new WFException("Z3标准值必须是数字");
            }
            var Z3Max = ParseHelper.ParseDecimal(entity["Z3Max"]);
            if (!Z3Max.HasValue)
            {
                throw new WFException("Z3最大值必须是数字");
            }
            var Z3Min = ParseHelper.ParseDecimal(entity["Z3Min"]);
            if (!Z3Min.HasValue)
            {
                throw new WFException("Z3最小值必须是数字");
            }
            var Z4Standard = ParseHelper.ParseDecimal(entity["Z4Standard"]);
            if (!Z4Standard.HasValue)
            {
                throw new WFException("Z4标准值必须是数字");
            }
            var Z4Max = ParseHelper.ParseDecimal(entity["Z4Max"]);
            if (!Z4Max.HasValue)
            {
                throw new WFException("Z4最大值必须是数字");
            }
            var Z4Min = ParseHelper.ParseDecimal(entity["Z4Min"]);
            if (!Z4Min.HasValue)
            {
                throw new WFException("Z4最小值必须是数字");
            }
            var Z5Standard = ParseHelper.ParseDecimal(entity["Z5Standard"]);
            if (!Z5Standard.HasValue)
            {
                throw new WFException("Z5标准值必须是数字");
            }
            var Z5Max = ParseHelper.ParseDecimal(entity["Z5Max"]);
            if (!Z5Max.HasValue)
            {
                throw new WFException("Z5最大值必须是数字");
            }
            var Z5Min = ParseHelper.ParseDecimal(entity["Z5Min"]);
            if (!Z5Min.HasValue)
            {
                throw new WFException("Z5最小值必须是数字");
            }
            var Z6Standard = ParseHelper.ParseDecimal(entity["Z6Standard"]);
            if (!Z6Standard.HasValue)
            {
                throw new WFException("Z6标准值必须是数字");
            }
            var Z6Max = ParseHelper.ParseDecimal(entity["Z6Max"]);
            if (!Z6Max.HasValue)
            {
                throw new WFException("Z6最大值必须是数字");
            }
            var Z6Min = ParseHelper.ParseDecimal(entity["Z6Min"]);
            if (!Z6Min.HasValue)
            {
                throw new WFException("Z6最小值必须是数字");
            }
        }


        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["EditMode"]))
            {
                return Insert(form, entity, ref message);
            }
            var oldEntity = Get(GetSelectSql(TableName), GetParam(entity));
            if (oldEntity == null)
            {
                throw new WFException("要更新的记录不存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = oldEntity.ToDFDictionary().Merge(entity);

            CheckData(TableName, newEntity, CurrentUserName);
            using (var db = Pub.DB)
            {
                db.Update<SM_T_PROCESS_LW>(newEntity.To<SM_T_PROCESS_LW>());
            }
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }
        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            var item = Get(GetSelectSql(TableName), GetParam(entity));
            if (item != null)
            {
                throw new WFException("记录已经存在".GetRes());
            }
            CheckInput(form, entity);
            var newEntity = entity;
            CheckData(TableName, newEntity, CurrentUserName);
            using (var db = Pub.DB)
            {
                db.Insert<SM_T_PROCESS_LW>(newEntity.To<SM_T_PROCESS_LW>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
