using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_T_INSTANCE : WF_T_INSTANCE, DynamicForm.Core.ISelectedPK
    {

        [PublicCodeConverter("InstanceStatus")]
        public new string InstanceStatus { get; set; }

        public string DFFormName { get; set; }
        public string CurrentExecutorName { get; set; }

        public string CompanyName { get; set; }

        public bool selected { get; set; }

        public string PK_GUID { get; set; }
        public string ChineseName { get; set; }
        public string DeptDisplayText { get; set; }
        public string EmployeeId { get; set; }

        public string AFENumber { get; set; }

        public string ProjectName { get; set; }
    }
}
