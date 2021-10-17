using Eppendorf_FSC.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eppendorf_FSC.Core.Interfaces
{
    public interface IDevicesRepository
    {
        

        public void CreateDevice(Device device);
        public IEnumerable<Device> GetDevices();
        public void UpdateDevice(Device device);
        public void DeleteDevice(int id);
    }
}
