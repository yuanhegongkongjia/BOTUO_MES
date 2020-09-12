using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_M_STEP : WF_M_STEP
    {
        [PublicCodeConverter("IsSendMessage")]
        public new string IsSendMessage { get; set; }

        [PublicCodeConverter("AllowActions")]
        public new string AllowActions { get; set; }
    }
}
