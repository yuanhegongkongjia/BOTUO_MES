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
	public class BT_MachineManage_CLASSMAPPER : ClassMapper<BT_MachineManage>
	{
		public BT_MachineManage_CLASSMAPPER()
		{
			Map(f => f.KPId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class BT_MachineManage
	{
		public string KPId { get; set; }
		public string MachineName { get; set; }
		public string MachineNum { get; set; }
		public string MachineStatus { get; set; }
		public string MachineParam1 { get; set; }
		public string MachineParam2 { get; set; }
		public string MachineParam3 { get; set; }
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
