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
	public class BT_DayProductAmount_CLASSMAPPER : ClassMapper<BT_DayProductAmount>
	{
		public BT_DayProductAmount_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class BT_DayProductAmount
	{
		public long PKId { get; set; }
		public string ProductAmount { get; set; }
		public string BadAmount { get; set; }
		public string ZuanAmount { get; set; }
		public DateTime? DayTime { get; set; }
		public string YouPercent { get; set; }
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
