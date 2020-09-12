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
	public class SM_T_PROCESS_PRODUCT_CLASSMAPPER : ClassMapper<SM_T_PROCESS_PRODUCT>
	{
		public SM_T_PROCESS_PRODUCT_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_PRODUCT
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public string LinePosition { get; set; }
		public string GangHao { get; set; }
		public decimal? XianJing { get; set; }
		public string Spec { get; set; }
		public decimal? Speed { get; set; }
		public decimal? DV { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
        
        public int? GenShu { get; set; }

    }
}
