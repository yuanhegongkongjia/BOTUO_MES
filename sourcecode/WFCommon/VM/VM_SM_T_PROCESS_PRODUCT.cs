using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace WFCommon.VM
{
	public class VM_SM_T_PROCESS_PRODUCT : SM_T_PROCESS_PRODUCT
	{
        public string Line { get; set; }
        public string ProcessName { get; set; }
        public string CustomerCode { get; set; }
        public string ProductSpec { get; set; }
    }
}