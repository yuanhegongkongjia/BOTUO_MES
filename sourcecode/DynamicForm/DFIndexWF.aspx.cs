﻿using DynamicForm;
using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicForm
{
    public partial class DFIndexWF : WFPageBase
    {
        public DynamicForm.Core.ucForm UcForm1;
        protected void Page_Load(object sender, EventArgs e)
        {
            UcForm1.DFFormName = DFPub.UrlDecode(this.Request[DFPub.DF_FORMNAME]);
            try
            {
                var user = Util.GetCurrentUser();
            }
            catch
            {
                this.Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(this.Request.Url.ToString()));
            }
        }
    }
}