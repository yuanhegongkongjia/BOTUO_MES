using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon.VM
{
    public class VM_WF_M_ROLE : WF_M_ROLE, ISelected
    {
        public bool selected { get; set; }
    }
}
