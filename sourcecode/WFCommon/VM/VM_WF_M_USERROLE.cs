using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFCommon;

namespace WFCommon.VM
{
    public class VM_WF_M_USERROLE : WF_M_USERROLE
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string ChineseName { get; set; }
        public string EmployeeId { get; set; }
        public string DeptName { get; set; }
        public string DeptDisplayText { get; set; }
    }
}
