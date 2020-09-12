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
	public class SM_T_PROCESS_CY_CLASSMAPPER : ClassMapper<SM_T_PROCESS_CY>
	{
		public SM_T_PROCESS_CY_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_CY
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
	
		public decimal? ADCStandard { get; set; }
		public decimal? ADCMax { get; set; }
		public decimal? ADCMin { get; set; }
		public decimal? ATJCStandard { get; set; }
		public decimal? ATJCMax { get; set; }
		public decimal? ATJCMin { get; set; }
		public decimal? AIznStandard { get; set; }
		public decimal? AIznMax { get; set; }
		public decimal? AIznMin { get; set; }
		public decimal? AMF1Standard { get; set; }
		public decimal? AMF1Max { get; set; }
		public decimal? AMF1Min { get; set; }
		public decimal? AMF2Standard { get; set; }
		public decimal? AMF2Max { get; set; }
		public decimal? AMF2Min { get; set; }
		public decimal? BDCStandard { get; set; }
		public decimal? BDCMax { get; set; }
		public decimal? BDCMin { get; set; }
		public decimal? BTJCStandard { get; set; }
		public decimal? BTJCMax { get; set; }
		public decimal? BTJCMin { get; set; }
		public decimal? BIznStandard { get; set; }
		public decimal? BIznMax { get; set; }
		public decimal? BIznMin { get; set; }
		public decimal? BMF1Standard { get; set; }
		public decimal? BMF1Max { get; set; }
		public decimal? BMF1Min { get; set; }
		public decimal? BMF2Standard { get; set; }
		public decimal? BMF2Max { get; set; }
		public decimal? BMF2Min { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
