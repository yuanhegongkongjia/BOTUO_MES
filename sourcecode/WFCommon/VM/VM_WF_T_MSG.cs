using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon.Utility;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_T_MSG : WF_T_MSG
    {
        [PublicCodeConverter("YESNO")]
        public new string IsRead { get; set; }
        [PublicCodeConverter("YESNO")]
        public new string IsSendEmail { get; set; }
        [PublicCodeConverter("YESNO")]
        public new string IsSendFeiQ { get; set; }
    }
}