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
	public class WF_M_ROLE_MODEL_CLASSMAPPER : ClassMapper<WF_M_ROLE_MODEL>
	{
		public WF_M_ROLE_MODEL_CLASSMAPPER()
		{
			Map(f => f.ModelId).Key(KeyType.Assigned);
			Map(f => f.RoleId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_ROLE_MODEL
	{
		public string ModelId { get; set; }
		public string RoleId { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
