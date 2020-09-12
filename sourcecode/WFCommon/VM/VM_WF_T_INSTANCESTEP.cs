using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_T_INSTANCESTEP : WF_T_INSTANCESTEP
    {
        [PublicCodeConverter("InstanceStepStatus")]
        public new string InstanceStepStatus { get; set; }
        public string ExecutorId { get; set; }
        public string ExecutorName { get; set; }
        public string ChineseName { get; set; }
        public string ExecuteResult { get; set; }
        [PublicCodeConverter("ExecuteStatus")]
        public string ExecuteStatus { get; set; }
        public string ExecuteComment { get; set; }
        public string Memo { get; set; }
        public string AdditionalApprove { get; set; }
    }
}
