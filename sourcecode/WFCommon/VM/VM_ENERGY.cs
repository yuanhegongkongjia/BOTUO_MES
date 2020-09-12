using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon.VM
{
    public class VM_ENERGY
    {
        public string name { get; set; }
        public decimal[] data { get; set; }

        public object data_vm { get; set; }



    }


    public class VM_ENERGY_EXTEND: VM_ENERGY
    {
        public string SeriesName { get; set; }

        public string[] categories { get; set; }



    }

    public class VM_ENERGY_COLLECT
    {
        public string Line { get; set; }
        public string CollectDate { get; set; }
        public decimal DataValue { get; set; }
        public string Position { get; set; }
    }
}
