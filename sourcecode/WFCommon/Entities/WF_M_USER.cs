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
	public class WF_M_USER_CLASSMAPPER : ClassMapper<WF_M_USER>
	{
		public WF_M_USER_CLASSMAPPER()
		{
			Map(f => f.UserId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_USER
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int? Status { get; set; }
		public DateTime? LastLoginTime { get; set; }
		public string Remark { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string DeptId { get; set; }
		public string Email { get; set; }
		public string ChineseName { get; set; }
		public string EmployeeId { get; set; }
	
        public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string Extend06 { get; set; }
		public string Extend07 { get; set; }
		public string Extend08 { get; set; }
		public string Extend09 { get; set; }
		public string Extend10 { get; set; }
        
        public string Duty { get; set; }

        public string MobilePhone { get; set; }
        public string Tel { get; set; }



       
	}
}
