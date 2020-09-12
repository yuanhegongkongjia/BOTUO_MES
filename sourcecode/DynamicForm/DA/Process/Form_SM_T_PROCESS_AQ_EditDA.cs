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
    public class Form_SM_T_PROCESS_AQ_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_AQ"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { InstanceId = entity["InstanceId"] };
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
                db.Update<SM_T_PROCESS_AQ>(newEntity.To<SM_T_PROCESS_AQ>());
            }
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
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
                db.Insert<SM_T_PROCESS_AQ>(newEntity.To<SM_T_PROCESS_AQ>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }

        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            var AQ1 = ParseHelper.ParseDecimal(entity["AQ1"]);
            if (!AQ1.HasValue)
            {
                throw new WFException("AQ1必须是数字");
            }
            var AQ2 = ParseHelper.ParseDecimal(entity["AQ2"]);
            if (!AQ2.HasValue)
            {
                throw new WFException("AQ2必须是数字");
            }

            var AQ3 = ParseHelper.ParseDecimal(entity["AQ3"]);
            if (!AQ3.HasValue)
            {
                throw new WFException("AQ3必须是数字");
            }

            var AQ4 = ParseHelper.ParseDecimal(entity["AQ4"]);
            if (!AQ4.HasValue)
            {
                throw new WFException("AQ4必须是数字");
            }

            var AQ5 = ParseHelper.ParseDecimal(entity["AQ5"]);
            if (!AQ5.HasValue)
            {
                throw new WFException("AQ5必须是数字");
            }

            var AQ6 = ParseHelper.ParseDecimal(entity["AQ6"]);
            if (!AQ6.HasValue)
            {
                throw new WFException("AQ6必须是数字");
            }

            var AQ7 = ParseHelper.ParseDecimal(entity["AQ7"]);
            if (!AQ7.HasValue)
            {
                throw new WFException("AQ7必须是数字");
            }

            var AQ8 = ParseHelper.ParseDecimal(entity["AQ8"]);
            if (!AQ8.HasValue)
            {
                throw new WFException("AQ8必须是数字");
            }

            var AKK = ParseHelper.ParseDecimal(entity["AKK"]);
            if (!AKK.HasValue)
            {
                throw new WFException("空开距离A必须是数字");
            }
            var BKK = ParseHelper.ParseDecimal(entity["BKK"]);
            if (!BKK.HasValue)
            {
                throw new WFException("空开距离B必须是数字");
            }

            //var BWT2 = ParseHelper.ParseDecimal(entity["BWT2"]);
            //if (!BWT2.HasValue)
            //{
            //    throw new WFException("WT2B必须是数字");
            //}

            //var AWT2 = ParseHelper.ParseDecimal(entity["AWT2"]);
            //if (!AWT2.HasValue)
            //{
            //    throw new WFException("WT2A必须是数字");
            //}
        }
    }
}
