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
    public class ColumnManagerDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var path = DFPub.RelativeToPhysical(entity["ConfigFile"]);
            var configs = GetTableConfig(path);
            SetSelectDataSource(form, "TableName", configs.Select(a => new DFSelectItem(a.TableName, a.TableName)).ToList());

            if (string.IsNullOrWhiteSpace(entity["TableName"]))
            {
                entity.Add("TableName", configs.FirstOrDefault().TableName);
            }

            if (!IsPostBack() || GetPostbackControl(entity) == "TableName")
            {
                var t = configs.FirstOrDefault(a => a.TableName == entity["TableName"]);
                if (t != null)
                {
                    this.Model.Add("ImportType", t.ImportType);
                    this.Model.Add("Insert", t.Insert);
                    this.Model.Add("Update", t.Update);
                    this.Model.Add("CheckExist", t.CheckExist);
                }
            }
        }

        public override int Query(FormM form, DFDictionary entity, DataGridVM vm, int start, int limit, ref string message)
        {
            var path = DFPub.RelativeToPhysical(entity["ConfigFile"]);
            var configs = GetTableConfig(path);

            var t = configs.FirstOrDefault(a => a.TableName == entity["TableName"]);
            var list = new List<VM_ColumnMetadata>();
            if (t != null)
            {
                list = t.Columns;
            }
            vm.results = list.Count;
            vm.rows = list;
            return DFPub.EXECUTE_SUCCESS;
        }


        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (GetSubmitButton(entity) == "btnGenerate")
            {
                return btnGenerate(form, entity, ref message);
            }
            if (GetSubmitButton(entity) == "btnSave")
            {
                return btnSave(form, entity, ref message);
            }
            return DFPub.EXECUTE_SUCCESS;
        }

        private int btnSave(FormM form, DFDictionary entity, ref string message)
        {
            var path = DFPub.RelativeToPhysical(entity["ConfigFile"]);
            var tables = GetTableConfig(path);
            var tableName = entity["TableName"];
            var t = tables.FirstOrDefault(a => a.TableName == tableName);
            t.ImportType = entity["ImportType"];
            t.Insert = entity["Insert"];
            t.Update = entity["Update"];
            t.CheckExist = entity["CheckExist"];
            var list = GetGridClientData<VM_ColumnMetadata>(entity);
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
                    item.ExecuteDataTable = dict["ExecuteDataTable"];
                    item.ExecuteSql = dict["ExecuteSql"];
                    item.DefaultSort = dict["DefaultSort"];
                    item.ConnectionString = dict["ConnectionString"];
                    item.IgnoreGetValueError = dict["IgnoreGetValueError"];
                    item.DatabaseType = dict["DatabaseType"];
                }
            }
            t.Columns = list;
            var el = new XElement("tables", tables.Select(a => a.ToXml()));
            el.Save(path);
            return DFPub.EXECUTE_SUCCESS;
        }

        private int btnGenerate(FormM form, DFDictionary entity, ref string message)
        {
            var list = GetGridClientData<VM_ColumnMetadata>(entity).Where(a => a.selected).ToList();
            if (list.Count == 0)
            {
                throw new WFException("请选择列");
            }

            var tableName = entity["TableName"];
            var GenerateOption = entity["GenerateOption"];
            var sql = string.Empty;
            if (GenerateOption.Contains("CreateTable"))
            {
                sql = string.Format("CREATE TABLE {0}({1});", tableName,
                    string.Join("," + Environment.NewLine, list.Select(a => string.Format("{0} {1}{2}", a.ColumnName, a.DataType, a.IsNullable == "Y" ? "" : " NOT NULL"))));

                sql += Environment.NewLine + Environment.NewLine
                    + string.Format("ALTER TABLE {0} ADD PRIMARY KEY({1});", tableName, string.Join(",", list.Where(a => a.IsPrimaryKey == "Y").Select(a => a.ColumnName)));
            }

            if (GenerateOption.Contains("Alter"))
            {
                sql += Environment.NewLine + Environment.NewLine
                    + string.Join(Environment.NewLine, list.Select(a => string.Format("ALTER TABLE {0} ADD {1} {2}{3}{4}{5};",
                        tableName,
                        a.ColumnName,
                        a.DataType,
                        a.IsNullable == "Y" ? string.Empty : " NOT NULL",
                        a.IsPrimaryKey == "Y" ? " PRIMARY KEY" : string.Empty,
                        a.IsIdentity == "Y" ? " IDENTITY(1,1)" : string.Empty)));
            }

            if (GenerateOption.Contains("Insert"))
            {
                var listCopy = list;
                var insertSql = string.Format("INSERT INTO {0}({1}) VALUES({2})", tableName, string.Join(",", listCopy.Select(a => a.ColumnName)),
                    string.Join(",", listCopy.Select(a => string.Format("@{0}", a.ColumnName))));
                if (entity["SyncText"] == "1")
                {
                    this.Model.Add("Insert", insertSql);
                }
                sql += Environment.NewLine + Environment.NewLine + insertSql;
            }

            if (GenerateOption.Contains("Update"))
            {
                var listCopy = list;
                if (entity["SpecialOptions"] == "1")
                {
                    listCopy = list.Where(a => a.ColumnName.ToLower() != "createuser" && a.ColumnName.ToLower() != "createtime").ToList();
                }
                var updateSql = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, string.Join(",", listCopy.Where(a => a.IsPrimaryKey != "Y").Select(a => string.Format("{0}=@{0}", a.ColumnName))),
                    string.Join(" and ", listCopy.Where(a => a.IsPrimaryKey == "Y").Select(a => string.Format("{0}=@{0}", a.ColumnName))));
                if (entity["SyncText"] == "1")
                {
                    this.Model.Add("Update", updateSql);
                }
                sql += Environment.NewLine + Environment.NewLine + updateSql;
            }
            if (GenerateOption.Contains("Exist"))
            {
                var existSql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, string.Join(" and ", list.Where(a => a.IsPrimaryKey == "Y").Select(a => string.Format("{0}=@{0}", a.ColumnName))));
                if (entity["SyncText"] == "1")
                {
                    this.Model.Add("CheckExist", existSql);
                }
                sql += Environment.NewLine + Environment.NewLine + existSql;
            }

            this.Model.Add("SQL", sql);
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}