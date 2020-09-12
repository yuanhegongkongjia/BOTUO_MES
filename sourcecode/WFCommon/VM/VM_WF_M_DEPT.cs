using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFCommon;
using WFCommon.Utility;

namespace WFCommon.VM
{
    public class VM_WF_M_DEPT : WF_M_DEPT, ISelected
    {
        public bool selected { get; set; }
    }
}