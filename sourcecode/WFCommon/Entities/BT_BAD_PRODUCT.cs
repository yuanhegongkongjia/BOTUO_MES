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
	public class BT_BAD_PRODUCT_CLASSMAPPER : ClassMapper<BT_BAD_PRODUCT>
	{
		public BT_BAD_PRODUCT_CLASSMAPPER()
		{
			Map(f => f.PKID).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class BT_BAD_PRODUCT
	{
		public long PKID { get; set; }
		public string BAD_NUMBER { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public string Remark6 { get; set; }
	}
}
