using DynamicForm.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCore;

namespace WFCommon.VM
{
    public class VM_WF_M_MODEL : WF_M_MODEL, ISelected
    {
        public bool selected { get; set; }
    }
}
