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
	public class WF_M_AUTH_DATA_CLASSMAPPER : ClassMapper<WF_M_AUTH_DATA>
	{
		public WF_M_AUTH_DATA_CLASSMAPPER()
		{
			Map(f => f.AuthId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_AUTH_DATA
	{
		public string AuthId { get; set; }
		public string RoleId { get; set; }
		public string DeptId { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
