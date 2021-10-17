using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Core.Models;
using System.Text.Json;
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
        }

        public void DeleteDevice(int id)
        {
        }

        public IEnumerable<Device> GetDevices()
        {
            return inMemoryDeviceList;
        }

        public void UpdateDevice(Device device)
        {
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
