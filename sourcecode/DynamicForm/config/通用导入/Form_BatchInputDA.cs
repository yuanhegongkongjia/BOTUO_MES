using Dapper;
using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_BatchInputDA : BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            SetSelectDataSource(form, "ConfigFile",
            Directory.GetFiles(GetImportConfigXmlPath(string.Empty), "*.xml")
                .Select(a => new DFSelectItem(Path.GetFileNameWithoutExtension(a), DFPub.PhysicalToRelative(a))).ToList());
            base.SetAccess(form, entity);
        }

        public override int Insert(FormM form, DFDictionary entity, ref string message)
        {
            form.GetControlM("SQL").MustInput = string.Empty;
            CheckInput(form, entity);
            var path = DFPub.RelativeToPhysical(entity["SqlFilePath"]);
            var configPath = DFPub.RelativeToPhysical(entity["ConfigFile"]);
            var dt = ReadExcel(path);
            if (dt.Columns.Contains("Result"))
            {
                dt.Columns.Remove("Result");
            }
            dt.Columns.Add("Result", typeof(string));
            var tables = GetTableConfig(configPath);

            ProcessData(dt, tables, entity);

            var failed = dt.AsEnumerable().Count(a => a.GetValue("Result").Length > 0);
            var success = dt.AsEnumerable().Count(a => a.GetValue("Result").Length == 0);
            ExcelReader.WriteDataTableToExcel(dt, path);
            if (failed > 0)
            {
                message = string.Format("成功导入 {0}，导入失败 {1}，请查看失败日志<br><a href='{2}'>{2}</a>", success, failed, DFPub.PhysicalToRelative(path));
                return DFPub.EXECUTE_ERROR;
            }
            else
            {
                message = string.Format("成功导入 {0}", success);
                return DFPub.EXECUTE_SUCCESS;
            }
        }

        public virtual void ProcessData(DataTable dt, List<VM_TableMetadata> tables, DFDictionary entity)
        {
            var user = Util.GetCurrentUser().UserName;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    var drDict = entity.Merge(dr.ToDFDictionary());
                    foreach (var table in tables)
                    {
                        var dict = ConvertDFDictionary(table.Columns, drDict);
                        CheckData(table.Columns, dict, user, null);
                        SaveData(table, dict);
                    }
                }
                catch (WFException ex)
                {
                    dr.SetField<string>("Result", ex.Message);
                }
            }
        }

        /// <summary>
        /// 直接执行sql语句下载excel
        /// </summary>
        /// <param name="form"></param>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            form.GetControlM("ConfigFile").MustInput = string.Empty;
            form.GetControlM("SqlFilePath").MustInput = string.Empty;
            CheckInput(form, entity);
            var sql = entity["SQL"];
            DataTable dt = null;
            using (var db = Pub.DB)
            {
                dt = db.ExecuteDataTable(sql);
            }
            var inputResultPath = ExportHelper.ExportDataTable(dt);
            var url = DFPub.PhysicalToRelative(inputResultPath);
            base.WriteScript(string.Format("downloadFile('{0}')", url), ref message);
            return DFPub.EXECUTE_SUCCESS;
        }
    }
}