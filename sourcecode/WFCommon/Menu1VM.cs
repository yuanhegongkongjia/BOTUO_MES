using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon
{

    public class Menu1VM
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool collapsed { get; set; }
        public List<Menu2VM> items { get; set; }
        public string ModuleId { get; set; }
    }
}
