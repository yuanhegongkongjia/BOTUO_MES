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
	public class SM_M_PLC_CLASSMAPPER : ClassMapper<SM_M_PLC>
	{
		public SM_M_PLC_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_M_PLC
	{
		public string PKId { get; set; }
		public string PLCAddress { get; set; }
		public string Line { get; set; }
		public string Station { get; set; }
		public string ParamName { get; set; }
		public string CollectFreq { get; set; }
		public string Unit { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
        public string ParamType { get; set; }

    }
}
