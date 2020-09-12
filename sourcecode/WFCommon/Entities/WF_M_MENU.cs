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
	public class WF_M_MENU_CLASSMAPPER : ClassMapper<WF_M_MENU>
	{
		public WF_M_MENU_CLASSMAPPER()
		{
			Map(f => f.MenuId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_MENU
	{
		public string MenuId { get; set; }
		public string PMenuId { get; set; }
		public int? Expanded { get; set; }
		public string MenuLabel { get; set; }
		public int? MenuOrder { get; set; }
		public string Icon { get; set; }
		public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string ModuleId { get; set; }
	}
}
