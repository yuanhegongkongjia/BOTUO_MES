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
	public class SM_M_DEFECT_CLASSMAPPER : ClassMapper<SM_M_DEFECT>
	{
		public SM_M_DEFECT_CLASSMAPPER()
		{
            Map(f => f.PKId).Key(KeyType.Assigned);
            AutoMap();
        }
	}
	public class SM_M_DEFECT
	{
		public string PKId { get; set; }
		public string DefectCode { get; set; }
		public string DefectName { get; set; }
		public string DefectDesc { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
