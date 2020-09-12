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
	public class SM_T_PROCESS_COLLECT_CLASSMAPPER : ClassMapper<SM_T_PROCESS_COLLECT>
	{
		public SM_T_PROCESS_COLLECT_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_COLLECT
	{
		public string PKId { get; set; }
		public string PLCAddress { get; set; }
		public string Line { get; set; }
		public string Position { get; set; }
		public string ParamName { get; set; }
		public string CollectValue { get; set; }
		public DateTime? CollectTime { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string ParamType { get; set; }
	}
}
