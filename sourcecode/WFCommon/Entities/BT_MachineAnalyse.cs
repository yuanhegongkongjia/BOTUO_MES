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
	public class BT_MachineAnalyse_CLASSMAPPER : ClassMapper<BT_MachineAnalyse>
	{
		public BT_MachineAnalyse_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class BT_MachineAnalyse
	{
		public string PKId { get; set; }
		public string MachineName { get; set; }
		public string MachineNum { get; set; }
		public string MachineOEE { get; set; }
		public string MachineAverageTime { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
