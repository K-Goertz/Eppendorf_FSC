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
using JsonFlatFileDataStore;
using System.Diagnostics;

namespace Eppendorf_FSC.Services.DeviceService
{
    public class FileDeviceRepositoryService : IDevicesRepository
    {
        private const string datastoreFileName = "Datastore.json";
        private DataStore deviceDataStore;
        private IDocumentCollection<Device> deviceCollection;
        private IMapper mapper;

        public FileDeviceRepositoryService(IMapper mapper)
        {
            this.mapper = mapper;

            //Add datastorefile and seed if opened first time. If datafile already exists - only load
            if (!File.Exists(datastoreFileName))
            {
                deviceDataStore = new DataStore(datastoreFileName);
                deviceCollection = deviceDataStore.GetCollection<Device>();
                AddSeedData();
            } else
            { 
                deviceDataStore = new DataStore(datastoreFileName);
                deviceCollection = deviceDataStore.GetCollection<Device>();
            }

        }

        public void CreateDevice(Device device)
        {
            
            //Gate for id already used
            if (deviceCollection.AsQueryable().Any(storedDevice => storedDevice.Id == device.Id))
            {
                //TODO: Throw;
                return;
            }
            else
            {
                //Seeding is very slow if not done async. Will be okay for now
                deviceCollection.InsertOneAsync(device).ConfigureAwait(false);
            }
        }

        public void DeleteDevice(int id)
        {
            if (deviceCollection.AsQueryable().Any(storedDevice => storedDevice.Id == id))
            {
                deviceCollection.DeleteOne(id);
            }
        }

        public IEnumerable<Device> GetDevices()
        {
            return deviceCollection.AsQueryable().Select(device=>(Device)device.Clone()).ToList();
        }

        public void UpdateDevice(Device device)
        {
            if (deviceCollection.AsQueryable().Any(storedDevice => storedDevice.Id == device.Id))
            {
                deviceCollection.UpdateOne(device.Id, device);
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
            foreach (var deviceMapped in deviceMap)
            {
                CreateDevice(deviceMapped);
            }


        }

    }
}
