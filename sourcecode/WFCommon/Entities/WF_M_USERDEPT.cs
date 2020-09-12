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
	public class WF_M_USERDEPT_CLASSMAPPER : ClassMapper<WF_M_USERDEPT>
	{
		public WF_M_USERDEPT_CLASSMAPPER()
		{
            Map(f => f.PK_GUID).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_USERDEPT
	{
        public string PK_GUID { get; set; }
		public string UserId { get; set; }
		public string DeptId { get; set; }
		
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
