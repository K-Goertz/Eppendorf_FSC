using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Core.Dto
{
    public class FileSeedDevice
    {
        public int id { get; set; }
        public string location { get; set; }
        public string type { get; set; }
        public string device_health { get; set; } // -> Enum
        public string last_used { get; set; }
        public string price { get; set; }
        public string color { get; set; }


    }
}
