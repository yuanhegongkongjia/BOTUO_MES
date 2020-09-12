using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using WFCommon.Utility;

namespace WFCommon.VM
{
    public class VM_WF_T_PROXY : WF_T_PROXY
    {
        public string ProxyUserName { get; set; }

        public string ModelName { get; set; }

        public string UserName { get; set; }

        [PublicCodeConverter("IsActive")]
        public new string IsActive { get; set; }
    }
}