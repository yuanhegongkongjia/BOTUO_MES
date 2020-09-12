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
	public class SM_T_POWER_CLASSMAPPER : ClassMapper<SM_T_POWER>
	{
		public SM_T_POWER_CLASSMAPPER()
		{
			Map(f => f.Id).Key(KeyType.Identity);
			AutoMap();
		}
	}
	public class SM_T_POWER
	{
		public int Id { get; set; }
		public string Line { get; set; }
		public string Position { get; set; }
		public string Period { get; set; }
		public string PeriodName { get; set; }
		public decimal? CollectValue { get; set; }
		public string CollectDate { get; set; }
		public string CollectMonth { get; set; }
		public string CollectYear { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
	}
}
