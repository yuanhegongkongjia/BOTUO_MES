using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon;
using WFCommon.Utility;

namespace WFCommon.VM
{
    public class VM_WF_M_USER : WF_M_USER, ISelected
    {
        public bool selected { get; set; }

        [PublicCodeConverter("STATUS", "Status")]
        public string Status_Text { get; set; }
        public string Roles { get; set; }
        public string DeptName { get; set; }

        public string Company_Text { get; set; }
        public int IsLC { get; set; }
    }
}