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
	public class SM_T_PROCESS_AQ_CLASSMAPPER : ClassMapper<SM_T_PROCESS_AQ>
	{
		public SM_T_PROCESS_AQ_CLASSMAPPER()
		{
			Map(f => f.PKId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_T_PROCESS_AQ
	{
		public string PKId { get; set; }
		public string InstanceId { get; set; }
		public decimal? AQ1 { get; set; }
		public decimal? AQ2 { get; set; }
		public decimal? AQ3 { get; set; }
		public decimal? AQ4 { get; set; }
		public decimal? AQ5 { get; set; }
		public decimal? AQ6 { get; set; }
		public decimal? AQ7 { get; set; }
		public decimal? AQ8 { get; set; }

		public string AWT1 { get; set; }
		public string BWT1 { get; set; }
		public decimal? AKK { get; set; }
		public decimal? BKK { get; set; }
		public decimal? AWT2 { get; set; }
		public decimal? BWT2 { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public string Remark4 { get; set; }
		public string Remark5 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }

        public string WT2AQ1 { get; set; }
        public string WT2AQ2 { get; set; }
        public string WT2AQ3 { get; set; }
        public string WT2AQ4 { get; set; }
        public string WT2AQ5 { get; set; }

        public string WT2AQ6 { get; set; }
        public string WT2AQ7 { get; set; }
        public string WT2AQ8 { get; set; }
    }
}
