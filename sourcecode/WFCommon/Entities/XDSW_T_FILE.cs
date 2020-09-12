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
	public class XDSW_T_FILE_CLASSMAPPER : ClassMapper<XDSW_T_FILE>
	{
		public XDSW_T_FILE_CLASSMAPPER()
		{
			Map(f => f.FileId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class XDSW_T_FILE
	{
		public string FileId { get; set; }
		public string FileName { get; set; }
		public string FileDesc { get; set; }
		public string Extend01 { get; set; }
		public string Extend02 { get; set; }
		public string Extend03 { get; set; }
		public string Extend04 { get; set; }
		public string Extend05 { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public byte[] FileData { get; set; }
	}
}
