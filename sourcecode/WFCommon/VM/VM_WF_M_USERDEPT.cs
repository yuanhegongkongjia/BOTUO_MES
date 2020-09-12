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
	public class VM_WF_M_USERDEPT : WF_M_USERDEPT
	{
        public bool selected { get; set; }
        public string DeptName { get; set; }
        public string Extend01 { get; set; }
    }
}