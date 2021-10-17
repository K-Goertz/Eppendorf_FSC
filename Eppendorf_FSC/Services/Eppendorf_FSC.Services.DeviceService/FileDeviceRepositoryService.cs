using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Eppendorf_FSC.Services.DeviceService
{
    public class FileDeviceRepositoryService : IDevicesRepository
    {
        //TODO: change with real file
        private List<Device> inMemoryDeviceList = new List<Device>();

        public FileDeviceRepositoryService()
        {
            //TODO: change this to only once and than save to other file
            //Seed on creation to test
            AddSeedData();
        }

        public void CreateDevice(Device device)
        {
            //throw new NotImplementedException();
        }

        public void DeleteDevice(int id)
        {
            //throw new NotImplementedException();
        }

        public IEnumerable<Device> GetDevices()
        {
            //throw new NotImplementedException();
            return inMemoryDeviceList;
        }

        public void UpdateDevice(Device device)
        {
            //throw new NotImplementedException();
        }


        private void AddSeedData()
        {
            var seedAsText = File.ReadAllText(@"Seed/data.json");
            var seedData = JsonSerializer.Deserialize<Core.Dto.FileSeedDevice[]>(seedAsText);
        }

    }
}
