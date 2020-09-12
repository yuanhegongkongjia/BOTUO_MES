using Dapper;
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
    /// 在流程中使用这个 WFBaseDA
    /// 如果记录存在，就更新，如果记录不存在，就插入
    /// </summary>
    public abstract class WFBaseDA : BaseDA
    {
        public abstract string TableName { get; }
        public abstract string CurrentUserName { get; }
        public abstract object GetParam(DFDictionary entity);
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            base.SetAccess(form, entity);
        }

        /// <summary>
        /// 检查数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Count(string sql, object param = null)
        {
            using (var db = Pub.DB)
            {
                return db.Query<int?>(sql, param).FirstOrDefault().GetValueOrDefault();
            }
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
            CheckInput(form, entity);
            var item = Get(GetSelectSql(TableName), GetParam(entity));
            if (item != null)
            {
                var newEntity = item.ToDFDictionary().Merge(entity);
                CheckData(TableName, newEntity, CurrentUserName);
                SaveData(TableName, newEntity, IMPORT_TYPE_UPDATE);
            }
            else
            {
                var newEntity = entity;
                CheckData(TableName, newEntity, CurrentUserName);
                SaveData(TableName, newEntity, IMPORT_TYPE_INSERT);
            }
            message = "保存成功";
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}