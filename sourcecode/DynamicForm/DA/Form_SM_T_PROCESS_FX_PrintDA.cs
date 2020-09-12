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
    public class Form_SM_T_PROCESS_FX_PrintDA : BaseDA
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





            var dtLY = new DataTable();
            dtLY = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from sm_t_process_ly p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtLY.TableName = "SM_T_PROCESS_LY";
            ds.Tables.Add(dtLY);

            var dtLW = new DataTable();
            dtLY = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from SM_T_PROCESS_LW p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtLY.TableName = "SM_T_PROCESS_LW";
            ds.Tables.Add(dtLY);

            var dtLQF = new DataTable();
            dtLY = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from SM_T_PROCESS_LQF p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtLY.TableName = "SM_T_PROCESS_LQF";
            ds.Tables.Add(dtLY);



            var dtAQ = new DataTable();
            dtAQ = base.GetList(entity, ref count, 0, Int32.MaxValue, "select * from SM_T_PROCESS_AQ p where p.InstanceId=@InstanceId", "order by InstanceId", new { InstanceId = entity["InstanceId"] });
            dtAQ.TableName = "SM_T_PROCESS_AQ";
            ds.Tables.Add(dtAQ);



            this.ReportDataSource = ds;
            this.ReportPath = Path.Combine(new DirectoryInfo(CurrentFolderHelper.GetCurrentFolder()).Parent.FullName, "Reports", "Process_FX.frx");
            base.SetAccess(form, entity);
        }
    }
}