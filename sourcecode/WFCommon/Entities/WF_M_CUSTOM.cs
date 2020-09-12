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
	public class WF_M_CUSTOM_CLASSMAPPER : ClassMapper<WF_M_CUSTOM>
	{
		public WF_M_CUSTOM_CLASSMAPPER()
		{
			Map(f => f.CustomId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_CUSTOM
	{
		public string CustomId { get; set; }
		public string FieldName { get; set; }
		public string FieldDisplayText { get; set; }
		public int? FieldType { get; set; }
		public string FieldAdditional { get; set; }
		public int? IsAllowNull { get; set; }
		public string DefaultValue { get; set; }
		public int? IsActive { get; set; }
		public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string Station { get; set; }
	}
}
