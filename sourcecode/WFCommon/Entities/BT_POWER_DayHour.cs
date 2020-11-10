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
	public class BT_POWER_DayHour_CLASSMAPPER : ClassMapper<BT_POWER_DayHour>
	{
		public BT_POWER_DayHour_CLASSMAPPER()
		{
			Map(f => f.PKID).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class BT_POWER_DayHour
	{
		public long PKID { get; set; }
		public string POSITION { get; set; }
		public string BIAOTOU_VALUE { get; set; }
		public string FENSHI_VALUE { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
		public string REMARK1 { get; set; }
		public string REMARK2 { get; set; }
		public string REMARK3 { get; set; }
		public string REMARK4 { get; set; }
		public string REMARK5 { get; set; }
		public string REMARK6 { get; set; }
	}
}
