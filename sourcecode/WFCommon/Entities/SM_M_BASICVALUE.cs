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
	public class SM_M_BASICVALUE_CLASSMAPPER : ClassMapper<SM_M_BASICVALUE>
	{
		public SM_M_BASICVALUE_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_M_BASICVALUE
	{
		public string PKId { get; set; }
		public string LineId { get; set; }
		public string LineName { get; set; }
		public string Station { get; set; }
		public string SubStation { get; set; }
		public string Category { get; set; }
		public string MaxValue { get; set; }
		public string MinValue { get; set; }
		public string Unit { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
