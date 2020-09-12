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
    public class Form_SM_T_PROCESS_YC_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_YC"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { PKId = entity["PKId"] };
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
                db.Update<SM_T_PROCESS_YC>(newEntity.To<SM_T_PROCESS_YC>());
            }
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }

        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            //脱脂槽
            var TZCStandard = ParseHelper.ParseDecimal(entity["TZCStandard"]);
            if (!TZCStandard.HasValue)
            {
                throw new WFException("脱脂槽标准信息必须是数字");
            }
            var TZCMax = ParseHelper.ParseDecimal(entity["TZCMax"]);
            if (!TZCMax.HasValue)
            {
                throw new WFException("脱脂槽最大信息必须是数字");
            }
            var TZCMin = ParseHelper.ParseDecimal(entity["TZCMin"]);
            if (!TZCMin.HasValue)
            {
                throw new WFException("脱脂槽最小信息必须是数字");
            }

            //AQ
            var AQStandard = ParseHelper.ParseDecimal(entity["AQStandard"]);
            if (!AQStandard.HasValue)
            {
                throw new WFException("AQ标准信息必须是数字");
            }
            var AQMax = ParseHelper.ParseDecimal(entity["AQMax"]);
            if (!AQMax.HasValue)
            {
                throw new WFException("AQ最大信息必须是数字");
            }
            var AQMin = ParseHelper.ParseDecimal(entity["AQMin"]);
            if (!AQMin.HasValue)
            {
                throw new WFException("AQ最小信息必须是数字");
            }

            //碱洗
            var JXStandard = ParseHelper.ParseDecimal(entity["JXStandard"]);
            if (!JXStandard.HasValue)
            {
                throw new WFException("碱洗标准信息必须是数字");
            }
            var JXMax = ParseHelper.ParseDecimal(entity["JXMax"]);
            if (!JXMax.HasValue)
            {
                throw new WFException("碱洗最大信息必须是数字");
            }
            var JXMin = ParseHelper.ParseDecimal(entity["JXMin"]);
            if (!JXMin.HasValue)
            {
                throw new WFException("碱洗最小信息必须是数字");
            }
            //主酸洗
            var ZSXStandard = ParseHelper.ParseDecimal(entity["ZSXStandard"]);
            if (!ZSXStandard.HasValue)
            {
                throw new WFException("主酸洗标准信息必须是数字");
            }
            var ZSXMax = ParseHelper.ParseDecimal(entity["ZSXMax"]);
            if (!ZSXMax.HasValue)
            {
                throw new WFException("主酸洗最大信息必须是数字");
            }
            var ZSXMin = ParseHelper.ParseDecimal(entity["ZSXMin"]);
            if (!ZSXMin.HasValue)
            {
                throw new WFException("主酸洗最小信息必须是数字");
            }

            //焦铜
            var JTStandard = ParseHelper.ParseDecimal(entity["JTStandard"]);
            if (!JTStandard.HasValue)
            {
                throw new WFException("焦铜标准信息必须是数字");
            }
            var JTMax = ParseHelper.ParseDecimal(entity["JTMax"]);
            if (!JTMax.HasValue)
            {
                throw new WFException("焦铜最大信息必须是数字");
            }
            var JTMin = ParseHelper.ParseDecimal(entity["JTMin"]);
            if (!JTMin.HasValue)
            {
                throw new WFException("焦铜最小信息必须是数字");
            }

            //硫酸锌
            var LSXStandard = ParseHelper.ParseDecimal(entity["LSXStandard"]);
            if (!LSXStandard.HasValue)
            {
                throw new WFException("硫酸锌标准信息必须是数字");
            }
            var LSXMax = ParseHelper.ParseDecimal(entity["LSXMax"]);
            if (!LSXMax.HasValue)
            {
                throw new WFException("硫酸锌最大信息必须是数字");
            }
            var LSXMin = ParseHelper.ParseDecimal(entity["LSXMin"]);
            if (!LSXMin.HasValue)
            {
                throw new WFException("硫酸锌最小信息必须是数字");
            }

            //热水洗
            var RSXStandard = ParseHelper.ParseDecimal(entity["RSXStandard"]);
            if (!RSXStandard.HasValue)
            {
                throw new WFException("热水洗标准信息必须是数字");
            }
            var RSXMax = ParseHelper.ParseDecimal(entity["RSXMax"]);
            if (!RSXMax.HasValue)
            {
                throw new WFException("热水洗最大信息必须是数字");
            }
            var RSXMin = ParseHelper.ParseDecimal(entity["RSXMin"]);
            if (!RSXMin.HasValue)
            {
                throw new WFException("热水洗最小信息必须是数字");
            }

            //MF冷却水
            var MFLQSStandard = ParseHelper.ParseDecimal(entity["MFLQSStandard"]);
            if (!MFLQSStandard.HasValue)
            {
                throw new WFException("MF冷却水标准信息必须是数字");
            }
            var MFLQSMax = ParseHelper.ParseDecimal(entity["MFLQSMax"]);
            if (!MFLQSMax.HasValue)
            {
                throw new WFException("MF冷却水最大信息必须是数字");
            }
            var MFLQSMin = ParseHelper.ParseDecimal(entity["MFLQSMin"]);
            if (!MFLQSMin.HasValue)
            {
                throw new WFException("MF冷却水最小信息必须是数字");
            }

            //冷却槽
            var LQCStandard = ParseHelper.ParseDecimal(entity["LQCStandard"]);
            if (!LQCStandard.HasValue)
            {
                throw new WFException("冷却槽标准信息必须是数字");
            }
            var LQCMax = ParseHelper.ParseDecimal(entity["LQCMax"]);
            if (!LQCMax.HasValue)
            {
                throw new WFException("冷却槽最大信息必须是数字");
            }
            var LQCMin = ParseHelper.ParseDecimal(entity["LQCMin"]);
            if (!LQCMin.HasValue)
            {
                throw new WFException("冷却槽最小信息必须是数字");
            }


            //磷酸
            var LSStandard = ParseHelper.ParseDecimal(entity["LSStandard"]);
            if (!LSStandard.HasValue)
            {
                throw new WFException("磷酸标准信息必须是数字");
            }
            var LSMax = ParseHelper.ParseDecimal(entity["LSMax"]);
            if (!LSMax.HasValue)
            {
                throw new WFException("磷酸最大信息必须是数字");
            }
            var LSMin = ParseHelper.ParseDecimal(entity["LSMin"]);
            if (!LSMin.HasValue)
            {
                throw new WFException("磷酸最小信息必须是数字");
            }

            //皂浸
            var ZJStandard = ParseHelper.ParseDecimal(entity["ZJStandard"]);
            if (!ZJStandard.HasValue)
            {
                throw new WFException("皂浸标准信息必须是数字");
            }
            var ZJMax = ParseHelper.ParseDecimal(entity["ZJMax"]);
            if (!ZJMax.HasValue)
            {
                throw new WFException("皂浸最大信息必须是数字");
            }
            var ZJMin = ParseHelper.ParseDecimal(entity["ZJMin"]);
            if (!ZJMin.HasValue)
            {
                throw new WFException("皂浸最小信息必须是数字");
            }
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
                db.Insert<SM_T_PROCESS_YC>(newEntity.To<SM_T_PROCESS_YC>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
