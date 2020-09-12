using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon.VM
{
    public class VM_XIAPAN
    {
        public decimal Weight { get; set; }
        public string CTH { get; set; }

        public string XPRQ { get; set; }

    }

    public class IndexVM
    {
        public bool hasError { get; set; }
        public string error { get; set; }
        //
        // 摘要:
        //     其他数据
        public string data { get; set; }

        public string data1 { get; set; }
        public string data2 { get; set; }
    }
}
