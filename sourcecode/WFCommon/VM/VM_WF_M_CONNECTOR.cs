using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_M_CONNECTOR : WF_M_CONNECTOR
    {
        [PublicCodeConverter("ScriptType")]
        public new string ScriptType { get; set; }

        public string FromStepName { get; set; }
        public string ToStepName { get; set; }
    }
}
