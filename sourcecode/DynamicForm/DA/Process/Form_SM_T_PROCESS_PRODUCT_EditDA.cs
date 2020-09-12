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
    public class Form_SM_T_PROCESS_PRODUCT_EditDA : CommonBaseDA
    {
        public override string TableName
        {
            get { return "SM_T_PROCESS_PRODUCT"; }
        }
        public override string CurrentUserName
        {
            get { return Util.GetCurrentUser().UserName; }
        }
        public override object GetParam(DFDictionary entity)
        {
            return new { PKId = entity["PKId"] };
        }


        public override void CheckInput(FormM form, DFDictionary entity)
        {
            base.CheckInput(form, entity);
            var XianJing = ParseHelper.ParseDecimal(entity["XianJing"]);
            if (!XianJing.HasValue)
            {
                throw new WFException("线径必须是数字");
            }

            var GenShu = ParseHelper.ParseDecimal(entity["GenShu"]);
            if (!GenShu.HasValue)
            {
                throw new WFException("根数必须是数字");
            }

            var Speed = ParseHelper.ParseDecimal(entity["Speed"]);
            if (!Speed.HasValue)
            {
                throw new WFException("速度必须是数字");
            }
            if (string.IsNullOrWhiteSpace(entity["EditMode"]))
            {
                using (var db = Pub.DB)
                {
                    var sql = "select count(1) from sm_t_process_product where InstanceId=@InstanceId and LinePosition=@LinePosition";
                    var item = db.Query<int>(sql, new { InstanceId = entity["InstanceId"], LinePosition = entity["LinePosition"] }).FirstOrDefault();
                    if (item != 0)
                    {
                        throw new WFException(string.Format("{0}的信息已经存在", entity["LinePosition"]));
                    }


                }
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
                db.Update<SM_T_PROCESS_PRODUCT>(newEntity.To<SM_T_PROCESS_PRODUCT>());
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
                db.Insert<SM_T_PROCESS_PRODUCT>(newEntity.To<SM_T_PROCESS_PRODUCT>());
            }
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            //sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}
