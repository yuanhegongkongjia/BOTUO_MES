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
	public class DEVICE_TAIZHANG_CLASSMAPPER : ClassMapper<DEVICE_TAIZHANG>
	{
		public DEVICE_TAIZHANG_CLASSMAPPER()
		{
			Map(f => f.DEVICE_ID).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class DEVICE_TAIZHANG
	{
		public string DEVICE_ID { get; set; }
		public string DEVICE_NAME { get; set; }
		public string DEVICE_COMPANY { get; set; }
		public string BUY_TIME { get; set; }
		public string DEVICE_TYPE { get; set; }
		public string FOR_DEPARTMENT { get; set; }
		public byte[] PICTURE { get; set; }
		public DateTime? Create_TIME { get; set; }
		public string CREAT_UESER { get; set; }
		public DateTime? MODIFY_TIME { get; set; }
		public string MODIFY_USER { get; set; }
		public string REMARK1 { get; set; }
		public string REMARK2 { get; set; }
		public string REMARK3 { get; set; }
		public string REMARK4 { get; set; }
		public string REMARK5 { get; set; }
	}
}
