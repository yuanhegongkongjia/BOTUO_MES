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
	public class WF_M_COMPANY_CLASSMAPPER : ClassMapper<WF_M_COMPANY>
	{
		public WF_M_COMPANY_CLASSMAPPER()
		{
			Map(f => f.CompanyCode).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_COMPANY
	{
		public string CompanyCode { get; set; }
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public string ContactPhone { get; set; }
		public string ContactMail { get; set; }
		public string BankName { get; set; }
		public string BankAccount { get; set; }
		public string Remark { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
		public string Category { get; set; }
	}
}
