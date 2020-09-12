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
using System.IO;
using System.Data;

namespace DynamicForm.DA
{
    public class Form_SM_T_PROCESS_PrintDA:BaseDA
    {
        public override void SetAccess(FormM form, DFDictionary entity)
        {
            var ds = new DataSet();
            var InstanceId = entity["InstanceId"];

            var count = 0;

            var dtHeader = new DataTable();
            dtHeader = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from sm_t_process  where InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });


            dtHeader.TableName = "SM_T_PROCESS";
            ds.Tables.Add(dtHeader);


            var dtProduct = new DataTable();
            dtProduct = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from sm_t_process_product p where p.InstanceId=@InstanceId", "order by LinePosition", new { InstanceId = entity["InstanceId"] });
            dtProduct.TableName = "SM_T_PROCESS_PRODUCT";
            ds.Tables.Add(dtProduct);


            var dtCY = new DataTable();
            dtCY = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from sm_t_process_cy p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtCY.TableName = "SM_T_PROCESS_CY";
            ds.Tables.Add(dtCY);

            var dtYC = new DataTable();
            dtYC = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from sm_t_process_yc p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtYC.TableName = "SM_T_PROCESS_YC";
            ds.Tables.Add(dtYC);

            //var dtLY = new DataTable();
            //dtLY = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from v_sm_t_process_ly p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            //dtLY.TableName = "v_sm_t_process_ly";
            //ds.Tables.Add(dtLY);

            var dtQuality = new DataTable();
            dtQuality = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from v_sm_t_process_quality p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtQuality.TableName = "v_sm_t_process_quality";
            ds.Tables.Add(dtQuality);

            //var dtAQ = new DataTable();
            //dtAQ = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from SM_T_PROCESS_AQ p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            //dtAQ.TableName = "SM_T_PROCESS_AQ";
            //ds.Tables.Add(dtAQ);



            this.ReportDataSource = ds;
            this.ReportPath = Path.Combine(new DirectoryInfo(CurrentFolderHelper.GetCurrentFolder()).Parent.FullName, "Reports", "Process.frx");
            base.SetAccess(form, entity);
        }
    }
}