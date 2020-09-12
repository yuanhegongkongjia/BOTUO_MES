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
	public class v_sm_t_process_ly_CLASSMAPPER : ClassMapper<v_sm_t_process_ly>
	{
		public v_sm_t_process_ly_CLASSMAPPER()
		{
			AutoMap();
		}
	}
	public class v_sm_t_process_ly
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public string Z1 { get; set; }
		public string Z2 { get; set; }
		public string Z3 { get; set; }
		public string Z4 { get; set; }
		public string Z5 { get; set; }
		public string Z6 { get; set; }
	}
}
