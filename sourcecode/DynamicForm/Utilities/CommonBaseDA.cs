using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm
{
    /// <summary>
    /// 非流程中使用这个
    /// 更新归更新，插入归插入
    /// </summary>
    public abstract class CommonBaseDA : BaseDA
    {
        public abstract string TableName { get; }
        public abstract string CurrentUserName { get; }
        public abstract object GetParam(DFDictionary entity);
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
        }

        /// <summary>
        /// 处理附件
        /// </summary>
        /// <param name="form"></param>
        /// <param name="entity"></param>
        public void ProcessAttachment(FormM form, ref DFDictionary entity)
        {
            foreach (var item in form.QueryControlMs("upload_to_db"))
            {
                XDSW_T_FILELoader.SetFileName(entity[item.Name], entity, item.Name + "_FileName");
            }
        }

        public override DFDictionary Get(FormM form, DFDictionary entity, ref string message)
        {
            var dict = new DFDictionary();
            var item = Get(GetSelectSql(TableName), GetParam(entity));
            if (item != null)
            {
                dict = item.ToDFDictionary();
            }
            ProcessAttachment(form, ref dict);
            return dict;
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
            SaveData(TableName, newEntity, IMPORT_TYPE_UPDATE);
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
            SaveData(TableName, newEntity, IMPORT_TYPE_INSERT);
            //SaveData(GetTableConfig(GetImportConfigXmlPath(TableName))[0], newEntity, IMPORT_TYPE_INSERT);
            var sb = new System.Text.StringBuilder(DFPub.DF_SCRIPT);
            sb.AppendFormat("alert('保存成功');");
            sb.AppendFormat("closeSelfDialog();");
            message = sb.ToString();

            return DFPub.EXECUTE_SUCCESS;
        }

       
    }
}