using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Core.Models
{
    /// <summary>
    /// Device domain model
    /// </summary>
    public class Device : ICloneable
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DeviceHealth DeviceHealth { get; set; } // -> Enum
        public DateTime LastUsed { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }

        public object Clone()
        {
            return new Device() { Id=Id,Location = Location,Type=Type,DeviceHealth=DeviceHealth,LastUsed=LastUsed,Price=Price,Color=Color};
        }
    }
}
