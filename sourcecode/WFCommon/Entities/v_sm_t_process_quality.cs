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
	public class v_sm_t_process_quality_CLASSMAPPER : ClassMapper<v_sm_t_process_quality>
	{
		public v_sm_t_process_quality_CLASSMAPPER()
		{
			AutoMap();
		}
	}
	public class v_sm_t_process_quality
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public string XJ { get; set; }
		public string TYD { get; set; }
		public string QD { get; set; }
		public string MS { get; set; }
		public string KZ { get; set; }
		public string Cu { get; set; }
		public string BT { get; set; }
		public string CustomerCode { get; set; }
		public string Spec { get; set; }
		public string QDLevel { get; set; }
		public decimal? MaxQD { get; set; }
		public decimal? AverQD { get; set; }
		public decimal? MinQD { get; set; }
	}
}
