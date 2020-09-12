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
	public class SM_T_DAYENERGY_CLASSMAPPER : ClassMapper<SM_T_DAYENERGY>
	{
		public SM_T_DAYENERGY_CLASSMAPPER()
		{
			Map(f => f.Id).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class SM_T_DAYENERGY
	{
		public int Id { get; set; }
		public string Category { get; set; }
		public string Line { get; set; }
		public decimal? TotalValue { get; set; }
		public string CollectDate { get; set; }
		public string CollectYear { get; set; }
		public string CollectMonth { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
	}
}
