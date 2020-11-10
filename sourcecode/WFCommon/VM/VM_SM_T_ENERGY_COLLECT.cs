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
		//public readonly object MachineOEE;

		public new DateTime? Remark1 { get; set; }
		public string MachineOEE { get; set; }
		public string MachineParamTyple { get; set; }
		public string MachineParam { get; set; }
		public string MachineName { get; set; }
		public string CTime { get; set; }


	
		public string Collect_Value { get; set; }

		public string ENERGY_TYPE { get; set; }
		public string POSITION { get; set; }

	}
}