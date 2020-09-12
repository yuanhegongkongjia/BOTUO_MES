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
	public class SM_T_PROCESS_YC_CLASSMAPPER : ClassMapper<SM_T_PROCESS_YC>
	{
		public SM_T_PROCESS_YC_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_YC
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public decimal? TZCStandard { get; set; }
		public decimal? AQStandard { get; set; }
		public decimal? JXStandard { get; set; }
		public decimal? ZSXStandard { get; set; }
		public decimal? JTStandard { get; set; }
		public decimal? LSXStandard { get; set; }
		public decimal? RSXStandard { get; set; }
		public decimal? MFLQSStandard { get; set; }
		public decimal? LQCStandard { get; set; }
		public decimal? LSStandard { get; set; }
		public decimal? ZJStandard { get; set; }
		public decimal? TZCMax { get; set; }
		public decimal? AQMax { get; set; }
		public decimal? JXMax { get; set; }
		public decimal? ZSXMax { get; set; }
		public decimal? JTMax { get; set; }
		public decimal? LSXMax { get; set; }
		public decimal? RSXMax { get; set; }
		public decimal? MFLQSMax { get; set; }
		public decimal? LQCMax { get; set; }
		public decimal? LSMax { get; set; }
		public decimal? ZJMax { get; set; }
		public decimal? TZCMin { get; set; }
		public decimal? AQMin { get; set; }
		public decimal? JXMin { get; set; }
		public decimal? ZSXMin { get; set; }
		public decimal? JTMin { get; set; }
		public decimal? LSXMin { get; set; }
		public decimal? RSXMin { get; set; }
		public decimal? MFLQSMin { get; set; }
		public decimal? LQCMin { get; set; }
		public decimal? LSMin { get; set; }
		public decimal? ZJMin { get; set; }
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
