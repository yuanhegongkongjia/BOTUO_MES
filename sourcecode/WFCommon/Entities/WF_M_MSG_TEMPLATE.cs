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
	public class WF_M_MSG_TEMPLATE_CLASSMAPPER : ClassMapper<WF_M_MSG_TEMPLATE>
	{
		public WF_M_MSG_TEMPLATE_CLASSMAPPER()
		{
			Map(f => f.PK_GUID).Key(KeyType.Assigned);
			AutoMap();
		}
	}
	public class WF_M_MSG_TEMPLATE
	{
		public string PK_GUID { get; set; }
		public string TemplateName { get; set; }
		public string Language { get; set; }
		public string TemplateType { get; set; }
		public string TemplateDesc { get; set; }
		public int? IsActive { get; set; }
		public int? IsSystem { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string Link { get; set; }
		public string CreateUser { get; set; }
		public DateTime? CreateTime { get; set; }
		public string LastModifyUser { get; set; }
		public DateTime? LastModifyTime { get; set; }
	}
}
