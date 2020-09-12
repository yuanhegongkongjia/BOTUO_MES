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
	public class WF_M_AUTH_MODULE_CLASSMAPPER : ClassMapper<WF_M_AUTH_MODULE>
	{
		public WF_M_AUTH_MODULE_CLASSMAPPER()
		{
			Map(f => f.AuthId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_AUTH_MODULE
	{
		public string AuthId { get; set; }
		public string RoleId { get; set; }
		public string ModuleId { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
