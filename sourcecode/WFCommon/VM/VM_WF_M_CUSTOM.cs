using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon;
using WFCommon.Utility;


namespace WFCommon.VM
{
    public class VM_WF_M_CUSTOM : WF_M_CUSTOM
    {
        [PublicCodeConverter("FieldType")]
        public new string FieldType { get; set; }

        [PublicCodeConverter("IsAllowNull")]
        public new string IsAllowNull { get; set; }

        [PublicCodeConverter("IsActive")]
        public new string IsActive { get; set; }
        public string StationName { get; set; }
    }
}
