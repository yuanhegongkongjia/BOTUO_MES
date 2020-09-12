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
	public class SM_T_PRODUCTQTY_CLASSMAPPER : ClassMapper<SM_T_PRODUCTQTY>
	{
		public SM_T_PRODUCTQTY_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class SM_T_PRODUCTQTY
	{
		public int PKId { get; set; }
		public string ProductYear { get; set; }
		public string ProductMonth { get; set; }
		public string ProductQuarter { get; set; }
		public string OrderNo { get; set; }
		public string ProductName { get; set; }
		public int? Quantity { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
