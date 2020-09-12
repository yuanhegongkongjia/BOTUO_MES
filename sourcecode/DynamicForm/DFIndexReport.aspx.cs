using DynamicForm;
using DynamicForm.Core;
using FastReport.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicForm
{
    public partial class DFIndexReport : WFPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UcForm1.DFFormName = DFPub.UrlDecode(this.Request[DFPub.DF_FORMNAME]);
        }

        protected void WebReport1_StartReport(object sender, EventArgs e)
        {
            if (UcForm1.DA != null
                && !string.IsNullOrWhiteSpace(UcForm1.DA.ReportPath)
                && UcForm1.DA.ReportDataSource != null
                && UcForm1.DA.ReportDataSource.Tables.Count > 0)
            {
                var report = (sender as WebReport).Report;
                report.Load(UcForm1.DA.ReportPath);
                report.RegisterData(UcForm1.DA.ReportDataSource);
            }
        }
    }
}