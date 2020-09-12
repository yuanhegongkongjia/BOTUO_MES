using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using System.Data;
using System.IO;
using WFDataAccess;

namespace DynamicForm.DA
{
    public class Form_WFDataReportDA:BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var newDict = new Dictionary<string, string>();
            var dict = JsonSerializeHelper.DeserializeObject<Dictionary<string, string>>(form.GetControlM("TableName").Options);

            //foreach (var item in dict)
            //{
            //    if (!string.IsNullOrWhiteSpace(item.Value))
            //    {
            //        if (AuthLoader.CheckFunctionAccess(item.Value, Util.GetCurrentUser().UserId))
            //        {
            //            newDict.Add(item.Key, item.Value);
            //        }
            //    }
            //}
            base.SetSelectDataSource(form, "TableName",
                dict.Select(a => new DFSelectItem() { Value = a.Key, Text = a.Value }).ToList(), true);
            base.SetAccess(form, entity);
        }

        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            if (string.IsNullOrWhiteSpace(entity["TableName"]))
            {
                throw new WFException("请选择流程".GetRes());
            }


            DataTable dt;
            using (var db = Pub.DB)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE 1=1", entity["TableName"]);
                //if (!string.IsNullOrWhiteSpace(entity["Requestor"]))
                //{
                //    sql += " and (申请人姓名 like @Requestor or 申请人中文姓名 like @Requestor)";
                //}
                var p = new
                {
                    //DeptId = QueryBuilder.In(ref sql, entity, "部门", "DeptId"),
                    //InstanceStatus = QueryBuilder.In(ref sql, entity, "流程状态", "InstanceStatus"),
                    RequestTime_From = QueryBuilder.DateFrom(ref sql, entity, "申请时间", "RequestTime_From"),
                    RequestTime_To = QueryBuilder.DateTo(ref sql, entity, "申请时间", "RequestTime_To"),
                    //Requestor = string.Format("%{0}%", entity["Requestor"])
                };

                sql += " order by 流水号 desc";
                dt = db.ExecuteDataTable(sql, p);
                if (dt.Rows.Count == 0)
                {
                    message = "没有数据".GetRes();
                    return DFPub.EXECUTE_ERROR;
                }
                dt.TableName = "T1";
                var ds = new DataSet();
                ds.Tables.Add(dt);
                this.ReportDataSource = ds;
                this.ReportPath = Path.Combine(new DirectoryInfo(CurrentFolderHelper.GetCurrentFolder()).Parent.FullName, "Reports", "WFDataReport.frx");
            }

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}