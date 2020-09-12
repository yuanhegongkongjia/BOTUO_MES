using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace WFCommon
{
	public class v_sm_t_process_yc_CLASSMAPPER : ClassMapper<v_sm_t_process_yc>
	{
		public v_sm_t_process_yc_CLASSMAPPER()
		{
			AutoMap();
		}
	}
	public class v_sm_t_process_yc
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public string TZC { get; set; }
		public string AQ { get; set; }
		public string JX { get; set; }
		public string ZSX { get; set; }
		public string JT { get; set; }
		public string LSX { get; set; }
		public string RSX { get; set; }
		public string MFLQS { get; set; }
		public string LQC { get; set; }
		public string LS { get; set; }
		public string ZJ { get; set; }
	}
}
