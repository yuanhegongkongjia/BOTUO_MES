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
	public class SM_T_PROCESS_CLASSMAPPER : ClassMapper<SM_T_PROCESS>
	{
		public SM_T_PROCESS_CLASSMAPPER()
		{
			Map(f => f.InstanceId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS
	{
		public string InstanceId { get; set; }
		public string Line { get; set; }
		public string ProduceDate { get; set; }
		public string ProcessManager { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
		public string ZDB { get; set; }
		public decimal? JXDLStandard { get; set; }
		public decimal? TotalLYStandard { get; set; }
		public decimal? JXDLMax { get; set; }
		public decimal? JXDLMin { get; set; }
		public decimal? TotalLYMax { get; set; }
		public decimal? TotalLYMin { get; set; }
		public string SXGY { get; set; }
		public decimal? PXSpeedStandard { get; set; }
		public decimal? PXSpeedMax { get; set; }
		public decimal? PXSpeedMin { get; set; }
		public decimal? SXZLStandard { get; set; }
		public decimal? SXZLMax { get; set; }
		public decimal? SXZLMin { get; set; }
		public string FXHD { get; set; }

        public string IsSXSaved { get; set; }
        public string IsFXSaved { get; set; }
        public string ProcessStatus { get; set; }

        public string ProcessName { get; set; }
    }
}
