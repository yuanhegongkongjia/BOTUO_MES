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
	public class SM_T_PROCESS_LY_CLASSMAPPER : ClassMapper<SM_T_PROCESS_LY>
	{
		public SM_T_PROCESS_LY_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_LY
    {
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public decimal? Z1Standard { get; set; }
		public decimal? Z1Max { get; set; }
		public decimal? Z1Min { get; set; }
		public decimal? Z2Standard { get; set; }
		public decimal? Z2Max { get; set; }
		public decimal? Z2Min { get; set; }
		public decimal? Z3Standard { get; set; }
		public decimal? Z3Max { get; set; }
		public decimal? Z3Min { get; set; }
		public decimal? Z4Standard { get; set; }
		public decimal? Z4Max { get; set; }
		public decimal? Z4Min { get; set; }
		public decimal? Z5Standard { get; set; }
		public decimal? Z5Max { get; set; }
		public decimal? Z5Min { get; set; }
		public decimal? Z6Standard { get; set; }
		public decimal? Z6Max { get; set; }
		public decimal? Z6Min { get; set; }
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
