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
	public class SM_M_CELL_CLASSMAPPER : ClassMapper<SM_M_CELL>
	{
		public SM_M_CELL_CLASSMAPPER()
		{
			Map(f => f.CellId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_M_CELL
	{
		public string CellId { get; set; }
		public string CellName { get; set; }
		public string CellDesc { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
