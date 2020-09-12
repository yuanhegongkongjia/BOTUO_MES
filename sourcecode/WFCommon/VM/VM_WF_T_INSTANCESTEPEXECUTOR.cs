using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_T_INSTANCESTEPEXECUTOR : WF_T_INSTANCESTEPEXECUTOR
    {
        public string DFFormName { get; set; }

        [PublicCodeConverter("ExecuteStatus")]
        public new string ExecuteStatus { get; set; }

        [PublicCodeConverter("InstanceStatus")]
        public string InstanceStatus { get; set; }

        public string Requestor { get; set; }
        public string RequestorName { get; set; }
        public string RequestorProxy { get; set; }
        public string RequestorProxyName { get; set; }
        public string ChineseName { get; set; }
        public string DeptDisplayText { get; set; }
        public DateTime? RequestTime { get; set; }
        public string AFENumber { get; set; }
        public string ProjectName { get; set; }
    }
}
