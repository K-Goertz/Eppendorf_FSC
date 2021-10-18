using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Core.Models
{
    /// <summary>
    /// The current health state of the device 
    /// </summary>
    public enum DeviceHealth
    {
        None = 0,
        good = 1,
        ok = 2,
        mediocre = 3,
        bad = 4,
        broken = 5,
    }
}
