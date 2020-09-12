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
	public class WF_M_DEPT_CLASSMAPPER : ClassMapper<WF_M_DEPT>
	{
		public WF_M_DEPT_CLASSMAPPER()
		{
			Map(f => f.DeptId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_DEPT
	{
		public string DeptId { get; set; }
		public string PDeptId { get; set; }
		public string DeptName { get; set; }
		public string DeptDisplayText { get; set; }
		public string DeptLabel { get; set; }
		public int? DeptOrder { get; set; }
		public int? Status { get; set; }
		public string Remark { get; set; }
		public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public int? ActiveNumber { get; set; }
		public string ManagerId { get; set; }
		public string ManagerName { get; set; }
	}
}
