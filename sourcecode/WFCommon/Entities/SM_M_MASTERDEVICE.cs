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
	public class SM_M_MASTERDEVICE_CLASSMAPPER : ClassMapper<SM_M_MASTERDEVICE>
	{
		public SM_M_MASTERDEVICE_CLASSMAPPER()
		{
			Map(f => f.MasterDeviceId).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class SM_M_MASTERDEVICE
	{
		public string MasterDeviceId { get; set; }
		public string MasterDeviceName { get; set; }
		public string Address { get; set; }
		public string LineId { get; set; }
		public string LineName { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
