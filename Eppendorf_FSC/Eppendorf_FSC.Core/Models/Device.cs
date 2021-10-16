using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Core.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DeviceHealth Device_health { get; set; } // -> Enum
        public DateTime Last_used { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
    }
}
