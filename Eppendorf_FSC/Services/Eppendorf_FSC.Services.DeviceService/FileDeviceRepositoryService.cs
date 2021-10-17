using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Core.Models;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AutoMapper;
using Eppendorf_FSC.Core.Dto;

namespace Eppendorf_FSC.Services.DeviceService
{
    public class FileDeviceRepositoryService : IDevicesRepository
    {
        //TODO: change with real file
        private List<Device> inMemoryDeviceList = new List<Device>();
        private IMapper mapper;

        public FileDeviceRepositoryService(IMapper mapper)
        {
            //TODO: change this to only once and than save to other file
            //Seed on creation to test
            this.mapper = mapper;
            AddSeedData();
        }

        public void CreateDevice(Device device)
        {
            //Gate for id already used
            if (inMemoryDeviceList.Any(device => device.Id == device.Id))
            {
                //TODO: Throw;
                return;
            }
            else
            {
                inMemoryDeviceList.Add(device);
            }
        }

        public void DeleteDevice(int id)
        {
            var foundDevice = inMemoryDeviceList.FirstOrDefault(device => device.Id == id);
            if (foundDevice != default)
            {
                inMemoryDeviceList.Remove(foundDevice);
            }
        }

        public IEnumerable<Device> GetDevices()
        {
            //Protect in memory repo 
            //TODO: change this for real repository
            return inMemoryDeviceList.Select(device=>(Device)device.Clone()).ToList();
        }

        public void UpdateDevice(Device device)
        {
            var foundDevice = inMemoryDeviceList.FirstOrDefault(device => device.Id == device.Id);
            if (foundDevice != default)
            {
                foundDevice.Id = device.Id;
                foundDevice.Location = device.Location;
                foundDevice.Type = device.Type;
                foundDevice.LastUsed = device.LastUsed;
                foundDevice.Price = device.Price;
                foundDevice.Color = device.Color;
            }
            else
            {
                //TODO: throw /show error
                return;
            }
        }


        private void AddSeedData()
        {
            var seedAsText = File.ReadAllText(@"Seed/data.json");
            var seedData = JsonSerializer.Deserialize<Core.Dto.FileSeedDevice[]>(seedAsText);
            var deviceMap = mapper.Map<FileSeedDevice[],Device[]>(seedData);
            inMemoryDeviceList.AddRange(deviceMap);
        }

    }
}
