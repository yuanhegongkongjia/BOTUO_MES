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
	public class SM_T_PROCESS_QUALITY_CLASSMAPPER : ClassMapper<SM_T_PROCESS_QUALITY>
	{
		public SM_T_PROCESS_QUALITY_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_QUALITY
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public string XJStandard { get; set; }
		public string GG { get; set; }
		public string GGXH { get; set; }
		public string GH { get; set; }
		public decimal? TDStandard { get; set; }
		public decimal? TDPreControlRange { get; set; }
		public decimal? TDQualifiedRange { get; set; }
		public decimal? MaxTD { get; set; }
		public decimal? MinTD { get; set; }
		public decimal? MaxTYD { get; set; }
		public decimal? QDRange { get; set; }
		public decimal? MSStandard { get; set; }
		public decimal? CuStandard { get; set; }
		public decimal? GKgStandard { get; set; }
		public decimal? MSRange { get; set; }
		public decimal? CuRange { get; set; }
		public decimal? GKgRange { get; set; }
		public decimal? MinBCu { get; set; }
		public decimal? ZJStandard { get; set; }
		public decimal? QDPreControlRange { get; set; }
		public decimal? QDQualifiedRange { get; set; }
		public decimal? ZJQualifiedRange { get; set; }
		public decimal? MSPreControlRange { get; set; }
		public decimal? MSQualifiedRange { get; set; }
		public decimal? CuPreControlRange { get; set; }
		public decimal? CuQualifiedRange { get; set; }
		public decimal? GKgPreControlRange { get; set; }
		public decimal? GKgQualifiedRange { get; set; }
		public decimal? BCuPreControlRange { get; set; }
		public decimal? BCuQualifiedRange { get; set; }
		public decimal? BCuStandard { get; set; }
		public string CustomerCode { get; set; }
		public string Spec { get; set; }
		public string QDLevel { get; set; }
		public string AverQD { get; set; }
		public string MaxQD { get; set; }
		public string MinQD { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
		public decimal? QDStandard { get; set; }
	}
}
