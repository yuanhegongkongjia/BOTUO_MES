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
	public class WF_M_METADATA_CLASSMAPPER : ClassMapper<WF_M_METADATA>
	{
		public WF_M_METADATA_CLASSMAPPER()
		{
			Map(f => f.TableName).Key(KeyType.Assigned);
			Map(f => f.ColumnName).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_METADATA
	{
		public string TableName { get; set; }
		public string ColumnName { get; set; }
		public string TableText { get; set; }
		public string ColumnText { get; set; }
		public int? IsPrimaryKey { get; set; }
		public int? ColumnOrder { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
