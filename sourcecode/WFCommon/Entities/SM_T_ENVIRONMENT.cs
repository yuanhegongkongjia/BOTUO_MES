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

    public class SM_T_ENVIRONMENT_CLASSMAPPER : ClassMapper<SM_T_ENVIRONMENT>
    {
        public SM_T_ENVIRONMENT_CLASSMAPPER()
        {
            Map(f => f.PKId).Key(KeyType.Assigned);
            AutoMap();
        }
    }
    public class SM_T_ENVIRONMENT
    {
        public int PKId { get; set; }
        public string ParamType { get; set; }
        public string ParamValue { get; set; }
        public string Line { get; set; }
        public DateTime CollectTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string ParamName { get; set; }
    }
}
