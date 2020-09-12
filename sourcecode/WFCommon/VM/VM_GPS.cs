using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFCommon.VM
{
    //{ "status":0,"result":{ "location":{ "lng":120.76312450192897,"lat":31.37437028428678},"precise":1,"confidence":80,"level":"道路"} }
    public class VM_GPS
    {
        public string status { get; set; }
        public GPSResult result { get; set; }
    }

    public class GPSResult
    {
        public GPSLocation location { get; set; }
        public string precise { get; set; }
        public string confidence { get; set; }
        public string level { get; set; }
    }

    public class GPSLocation
    {
        public decimal lng { get; set; }
        public decimal lat { get; set; }
    }
}
