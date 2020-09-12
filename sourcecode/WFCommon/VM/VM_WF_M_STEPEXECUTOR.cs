using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_M_STEPEXECUTOR : WF_M_STEPEXECUTOR
    {
        [PublicCodeConverter("ExecutorType")]
        public new string ExecutorType { get; set; }

        [PublicCodeConverter("DeptRelated")]
        public new string DeptRelated { get; set; }
    }
}
