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
	public class SM_M_UPH_CLASSMAPPER : ClassMapper<SM_M_UPH>
	{
		public SM_M_UPH_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_M_UPH
	{
		public string PKId { get; set; }
		public string ProductName { get; set; }
		public string Station { get; set; }
		public string SubStation { get; set; }
		public string UPH { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
