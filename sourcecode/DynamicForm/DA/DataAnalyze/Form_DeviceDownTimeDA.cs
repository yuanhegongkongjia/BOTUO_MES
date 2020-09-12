using System;
using System.Linq;
using System.Collections.Generic;
using DynamicForm.Core;
using Dapper;
using DapperExtensions;
using WFCommon;
using WFCommon.Utility;
using WFCore;
using WFDataAccess;
using System.Data;
using System.IO;

namespace DynamicForm.DA
{
    public class Form_DeviceDownTimeDA : BaseDA
    {
        public override int Update(FormM form, DFDictionary entity, ref string message)
        {
            DataTable dt;
            using (var db = Pub.DB)
            {
                var currentUser = Util.GetCurrentUser();
                var category = "";
                var SenderCompany = "";
                var ReceiverCompany = "";
                var sql = @"select * from sm_t_devicedowntime d where 1=1 
                            ";
                if (!string.IsNullOrWhiteSpace(entity["ProductDateFrom"]))
                {
                    sql += " and d.ProductDate>=@ProductDateFrom";
                }
                if (!string.IsNullOrWhiteSpace(entity["ProductDateTo"]))
                {
                    sql += " and d.ProductDate<=@ProductDateTo";
                }

                if (!string.IsNullOrWhiteSpace(entity["LineName"]))
                {
                    sql += " and d.LineName like @LineName";
                }
                //var sql = string.Format("exec sp_Report_Recycle @CompanyName,@PalletType,@CMonth,@Category,@SenderCompany,@ReceiverCompany");

                var p = new
                {
                    ProductDateFrom = entity["ProductDateFrom"],
                    ProductDateTo = entity["ProductDateTo"],
                    LineName = string.Format("%{0}%",entity["LineName"])
                };


                dt = db.ExecuteDataTable(sql, p);
                if (dt.Rows.Count == 0)
                {
                    message = "没有数据".GetRes();
                    return DFPub.EXECUTE_ERROR;
                }
                dt.TableName = "SM_T_DEVICEDOWNTIME";
                var ds = new DataSet();
                ds.Tables.Add(dt);
                this.ReportDataSource = ds;
                this.ReportPath = Path.Combine(new DirectoryInfo(CurrentFolderHelper.GetCurrentFolder()).Parent.FullName, "Reports", "DownTime.frx");

            }

            return DFPub.EXECUTE_SUCCESS;
        }
    }
}