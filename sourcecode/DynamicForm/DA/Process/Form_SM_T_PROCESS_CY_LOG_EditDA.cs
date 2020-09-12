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
    public class Form_SM_T_PROCESS_CY_LOG_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_CY_LOG"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { LogId = entity["LogId"] };
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
                db.Update<SM_T_PROCESS_CY_LOG>(newEntity.To<SM_T_PROCESS_CY_LOG>());
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
                db.Insert<SM_T_PROCESS_CY_LOG>(newEntity.To<SM_T_PROCESS_CY_LOG>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            if (entity["EditMode"] == "Edit")
            {
                //是编辑
                return base.Get(form,entity,ref message);

            }
            using (var db = Pub.DB)
            {
                //不是编辑 是新增 是第一次调整 则选择原有的记录作为默认值
                //如果不是第一次调整 则选择最新的一条记录作为默认值
                var s = "select top 1 * from sm_t_process_cy_log where InstanceId=@InstanceId order by LastModifyTime desc";
                var sItem = db.Query<SM_T_PROCESS_CY>(s,new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                if (sItem == null)
                {
                    var sql = "select * from sm_t_process_cy where InstanceId=@InstanceId";
                    var item = db.Query<SM_T_PROCESS_CY>(sql, new { InstanceId = entity["InstanceId"] }).FirstOrDefault();
                    if (item != null)
                    {
                        entity.Merge(item.ToDFDictionary());
                    }
                }
                else
                {
                    entity.Merge(sItem.ToDFDictionary());
                }
            }
            return entity;
        }

        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            //A面镀槽
            var ADCStandard = ParseHelper.ParseDecimal(entity["ADCStandard"]);
            if (!ADCStandard.HasValue)
            {
                throw new WFException("A面镀槽标准值必须是数字");
            }
            var ADCMax = ParseHelper.ParseDecimal(entity["ADCMax"]);
            if (!ADCMax.HasValue)
            {
                throw new WFException("A面镀槽最大值必须是数字");
            }
            var ADCMin = ParseHelper.ParseDecimal(entity["ADCMin"]);
            if (!ADCMin.HasValue)
            {
                throw new WFException("A面镀槽最小值必须是数字");
            }


            //A面调节槽
            var ATJCStandard = ParseHelper.ParseDecimal(entity["ATJCStandard"]);
            if (!ATJCStandard.HasValue)
            {
                throw new WFException("A面调节槽标准值必须是数字");
            }
            var ATJCMax = ParseHelper.ParseDecimal(entity["ATJCMax"]);
            if (!ATJCMax.HasValue)
            {
                throw new WFException("A面调节槽最大值必须是数字");
            }
            var ATJCMin = ParseHelper.ParseDecimal(entity["ATJCMin"]);
            if (!ATJCMin.HasValue)
            {
                throw new WFException("A面调节槽最小值必须是数字");
            }

            //A面Izn
            var AIznStandard = ParseHelper.ParseDecimal(entity["AIznStandard"]);
            if (!AIznStandard.HasValue)
            {
                throw new WFException("A面Izn标准值必须是数字");
            }
            var AIznMax = ParseHelper.ParseDecimal(entity["AIznMax"]);
            if (!AIznMax.HasValue)
            {
                throw new WFException("A面Iz最大值必须是数字");
            }
            var AIznMin = ParseHelper.ParseDecimal(entity["AIznMin"]);
            if (!AIznMin.HasValue)
            {
                throw new WFException("A面Izn最小值必须是数字");
            }

            //A面MF1
            var AMF1Standard = ParseHelper.ParseDecimal(entity["AMF1Standard"]);
            if (!AMF1Standard.HasValue)
            {
                throw new WFException("A面MF1标准值必须是数字");
            }
            var AMF1Max = ParseHelper.ParseDecimal(entity["AMF1Max"]);
            if (!AMF1Max.HasValue)
            {
                throw new WFException("A面MF1最大值必须是数字");
            }
            var AMF1Min = ParseHelper.ParseDecimal(entity["AMF1Min"]);
            if (!AMF1Min.HasValue)
            {
                throw new WFException("A面MF1最小值必须是数字");
            }

            //A面MF2
            var AMF2Standard = ParseHelper.ParseDecimal(entity["AMF2Standard"]);
            if (!AMF2Standard.HasValue)
            {
                throw new WFException("A面MF2标准值必须是数字");
            }
            var AMF2Max = ParseHelper.ParseDecimal(entity["AMF2Max"]);
            if (!AMF2Max.HasValue)
            {
                throw new WFException("A面MF2最大值必须是数字");
            }
            var AMF2Min = ParseHelper.ParseDecimal(entity["AMF2Min"]);
            if (!AMF2Min.HasValue)
            {
                throw new WFException("A面MF2最小值必须是数字");
            }

            //B面镀槽
            var BDCStandard = ParseHelper.ParseDecimal(entity["BDCStandard"]);
            if (!BDCStandard.HasValue)
            {
                throw new WFException("B面镀槽标准值必须是数字");
            }
            var BDCMax = ParseHelper.ParseDecimal(entity["BDCMax"]);
            if (!BDCMax.HasValue)
            {
                throw new WFException("B面镀槽最大值必须是数字");
            }
            var BDCMin = ParseHelper.ParseDecimal(entity["BDCMin"]);
            if (!BDCMin.HasValue)
            {
                throw new WFException("B面镀槽最小值必须是数字");
            }


            //B面调节槽
            var BTJCStandard = ParseHelper.ParseDecimal(entity["BTJCStandard"]);
            if (!BTJCStandard.HasValue)
            {
                throw new WFException("B面调节槽标准值必须是数字");
            }
            var BTJCMax = ParseHelper.ParseDecimal(entity["BTJCMax"]);
            if (!BTJCMax.HasValue)
            {
                throw new WFException("B面调节槽最大值必须是数字");
            }
            var BTJCMin = ParseHelper.ParseDecimal(entity["BTJCMin"]);
            if (!BTJCMin.HasValue)
            {
                throw new WFException("B面调节槽最小值必须是数字");
            }

            //B面Izn
            var BIznStandard = ParseHelper.ParseDecimal(entity["BIznStandard"]);
            if (!BIznStandard.HasValue)
            {
                throw new WFException("B面Izn标准值必须是数字");
            }
            var BIznMax = ParseHelper.ParseDecimal(entity["BIznMax"]);
            if (!BIznMax.HasValue)
            {
                throw new WFException("B面Iz最大值必须是数字");
            }
            var BIznMin = ParseHelper.ParseDecimal(entity["BIznMin"]);
            if (!BIznMin.HasValue)
            {
                throw new WFException("B面Izn最小值必须是数字");
            }

            //B面MF1
            var BMF1Standard = ParseHelper.ParseDecimal(entity["BMF1Standard"]);
            if (!BMF1Standard.HasValue)
            {
                throw new WFException("B面MF1标准值必须是数字");
            }
            var BMF1Max = ParseHelper.ParseDecimal(entity["BMF1Max"]);
            if (!BMF1Max.HasValue)
            {
                throw new WFException("B面MF1最大值必须是数字");
            }
            var BMF1Min = ParseHelper.ParseDecimal(entity["BMF1Min"]);
            if (!BMF1Min.HasValue)
            {
                throw new WFException("B面MF1最小值必须是数字");
            }

            //B面MF2
            var BMF2Standard = ParseHelper.ParseDecimal(entity["BMF2Standard"]);
            if (!BMF2Standard.HasValue)
            {
                throw new WFException("B面MF2标准值必须是数字");
            }
            var BMF2Max = ParseHelper.ParseDecimal(entity["BMF2Max"]);
            if (!BMF2Max.HasValue)
            {
                throw new WFException("B面MF2最大值必须是数字");
            }
            var BMF2Min = ParseHelper.ParseDecimal(entity["BMF2Min"]);
            if (!BMF2Min.HasValue)
            {
                throw new WFException("B面MF2最小值必须是数字");
            }
        }
    }
}
