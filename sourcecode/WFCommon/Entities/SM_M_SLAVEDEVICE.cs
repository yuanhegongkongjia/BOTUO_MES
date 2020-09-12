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
	public class SM_M_SLAVEDEVICE_CLASSMAPPER : ClassMapper<SM_M_SLAVEDEVICE>
	{
		public SM_M_SLAVEDEVICE_CLASSMAPPER()
		{
            Map(f => f.SlaveDeviceId).Key(KeyType.Assigned);
            AutoMap();
        }
	}
	public class SM_M_SLAVEDEVICE
	{
		public string SlaveDeviceId { get; set; }
		public string SlaveDeviceAddress { get; set; }
		public string SlaveDeviceName { get; set; }
		public string MasterDeviceId { get; set; }
		public string MasterDeviceName { get; set; }
		public string MasterDeviceAddress { get; set; }
		public string Remark1 { get; set; }
		public string Remark2 { get; set; }
		public string Remark3 { get; set; }
		public DateTime? CreateTime { get; set; }
		public string CreateUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifyUser { get; set; }
	}
}
