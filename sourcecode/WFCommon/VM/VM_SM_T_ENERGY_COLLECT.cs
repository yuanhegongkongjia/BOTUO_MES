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
	public class VM_SM_T_ENERGY_COLLECT : SM_T_ENERGY_COLLECT
	{
        public string CTime { get; set; }
	}
}