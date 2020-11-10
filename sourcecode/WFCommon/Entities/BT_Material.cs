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
	public class BT_Material_CLASSMAPPER : ClassMapper<BT_Material>
	{
		public BT_Material_CLASSMAPPER()
		{
			AutoMap();
		}
	}
	public class BT_Material
	{
		public string PKID { get; set; }
		public string ENERGY_TYPE { get; set; }
		public string Collect_Value { get; set; }
		public DateTime? COLLECTIME { get; set; }
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
