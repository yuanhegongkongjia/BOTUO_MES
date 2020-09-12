using Dapper;
using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WFCommon.Utility;
using WFCore;

namespace DynamicForm.DA
{
    public class FormImportConfigDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            SetSelectDataSource(form, "ConfigFile",
            Directory.GetFiles(GetImportConfigXmlPath(string.Empty), "*.xml")
                .Select(a => new DFSelectItem(Path.GetFileNameWithoutExtension(a), DFPub.PhysicalToRelative(a))).ToList());
            IDBHelper dbHelper;
            using (var db = Pub.DB)
            {
                dbHelper = DBHelper.GetDBHelper(db);
            }
            SetSelectDataSource(form, "SearchTableName", dbHelper.LoadTables().Select(a => new DFSelectItem(a.TableName, a.TableName)).ToList(), true);
            base.SetAccess(form, entity);

        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            if (IsEmptyQuery(entity))
            {
                return EmptyQuery(vm);
            }
            ArgumentCheck.CheckMustInput(entity, "TableName");
            var tableName = entity["TableName"];
            List<VM_ColumnMetadata> list = null;

            // 如果是客户端点击查询按钮，或者回传回的数据没有客户端数据
            IDBHelper dbHelper;
            using (var db = Pub.DB)
            {
                dbHelper = DBHelper.GetDBHelper(db);
            }
            list = dbHelper.LoadColumns(tableName);
            vm.results = list.Count;
            vm.rows = Merge(tableName, list.Skip(start).Take(limit).ToList());
            return DFPub.EXECUTE_SUCCESS;
        }

        /// <summary>
        /// 从 xml 中把数据装载到 list 中
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<VM_ColumnMetadata> Merge(string tableName, List<VM_ColumnMetadata> list)
        {
            var list2 = GetColumnConfig(GetImportConfigXmlPath(tableName), tableName);
            foreach (var item in list)
            {
                // 只匹配名称,不区分大小写
                var item2 = list2.FirstOrDefault(a => string.Compare(a.ColumnName, item.ColumnName, true) == 0);
                if (item2 != null)
                {
                    item2.DataType = item.DataType;
                    item2.IsIdentity = item.IsIdentity;
                    item2.IsNullable = item.IsNullable;
                    item2.IsPrimaryKey = item.IsPrimaryKey;
                }
                else
                {
                    list2.Add(item);
                }
            }

            foreach (var item in list2)
            {
                if (string.IsNullOrWhiteSpace(item.ColumnText))
                {
                    item.ColumnText = item.ColumnName;
                }
                item.PK_GUID = item.ColumnName;
            }
            return list2;
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (GetSubmitButton(entity) == "btnSave")
            {
                return btnSave(form, entity, ref message);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        /// <summary>
        /// 保存到xml中
        /// </summary>
        /// <param name="form"></param>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private int btnSave(FormM form, DFDictionary entity, ref string message)
        {
            var list = GetGridClientData<VM_ColumnMetadata>(entity);
            var tableName = entity["TableName"];
            var xmlFileName = entity["XmlFileName"];
            ArgumentCheck.CheckMustInput(entity, "XmlFileName");
            Save(xmlFileName, tableName, list);

            // 更新界面上的配置文件下拉框,因为如果新增的话,就多了一个xml
            SetSelectDataSource(form, "ConfigFile",
            Directory.GetFiles(GetImportConfigXmlPath(string.Empty), "*.xml")
                .Select(a => new DFSelectItem(Path.GetFileNameWithoutExtension(a), DFPub.PhysicalToRelative(a))).ToList());
            return DFPub.EXECUTE_SUCCESS;
        }
        /// <summary>
        /// 把其他信息保存到 xml 中
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="tableName"></param>
        /// <param name="list"></param>
        private void Save(string xmlFileName, string tableName, List<VM_ColumnMetadata> list)
        {
            // 需要先读取之前的xml配置
            string path = GetImportConfigXmlPath(xmlFileName);

            if (File.Exists(path))
            {
                // 需要保留之前的配置信息
                var tableConfigList = GetTableConfig(path);
                // 查找里面的table配置节点
                var table = tableConfigList.FirstOrDefault(a => string.Compare(a.TableName, tableName, true) == 0);
                if (table == null)
                {
                    table = GetTableMetadata(tableName, list);
                    tableConfigList.Add(table);
                }
                table.Columns = AddCheckInfo(list);
                var el = new XElement("tables", tableConfigList.Select(a => a.ToXml()));
                el.Save(path);
            }
            else
            {
                VM_TableMetadata table = GetTableMetadata(tableName, list);
                var el = new XElement("tables", table.ToXml());
                el.Save(path);
            }
        }

        /// <summary>
        /// 得到 TableMetadata 元数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private VM_TableMetadata GetTableMetadata(string tableName, List<VM_ColumnMetadata> list)
        {
            AddCheckInfo(list);
            var table = new VM_TableMetadata();
            table.TableName = tableName;
            table.Columns = list;
            return table;
        }

        private List<VM_ColumnMetadata> AddCheckInfo(List<VM_ColumnMetadata> list)
        {
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item.ColumnCheck))
                {
                    var dict = JsonSerializeHelper.DeserializeObject<DFDictionary>(item.ColumnCheck);
                    item.CheckMustInput = dict["CheckMustInput"];
                    item.CheckNumber = dict["CheckNumber"];
                    item.CheckMaxLength = dict["CheckMaxLength"];
                    item.CheckDate = dict["CheckDate"];
                    item.CheckDateTime = dict["CheckDateTime"];
                    item.CheckTime = dict["CheckTime"];
                    item.DefaultValue = dict["DefaultValue"];
                    item.GetValue = dict["GetValue"];
                    item.IgnoreGetValueError = dict["IgnoreGetValueError"];
                }
            }
            return list;
        }
    }
}