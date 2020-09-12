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
	public class WF_M_MODULE_CLASSMAPPER : ClassMapper<WF_M_MODULE>
	{
		public WF_M_MODULE_CLASSMAPPER()
		{
			Map(f => f.ModuleId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_MODULE
	{
		public string ModuleId { get; set; }
		public string PModuleId { get; set; }
		public string ModuleName { get; set; }
		public string ModuleDisplayText { get; set; }
		public string ModuleDesc { get; set; }
		public string ModuleLink { get; set; }
		public int? ModuleOrder { get; set; }
		public string ModuleLabel { get; set; }
		public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
