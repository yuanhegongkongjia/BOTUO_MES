using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon
{
    public class Menu2VM
    {
        public string id { get; set; }
        public string text { get; set; }
        public string href { get; set; }
        public List<Menu2VM> childs { get; set; }
        public string ModuleId { get; set; }

    }
}
