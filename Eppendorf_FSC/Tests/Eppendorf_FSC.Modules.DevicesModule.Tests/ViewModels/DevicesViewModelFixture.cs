using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Modules.DevicesModule.ViewModels;
using System.Collections.Generic;
using Moq;
using Prism.Regions;
using Xunit;
using System.Linq;
using Eppendorf_FSC.Core.Models;
using System;

namespace Eppendorf_FSC.Modules.DevicesModule.Tests.ViewModels
{
    public class DevicesViewModelFixture
    {

        
        const string MessageServiceDefaultMessage = "Some Value";
        Mock<IRegionManager> regionManagerMock;
        Mock<IDevicesRepository> deviceRepositoryMock;

        private List<Device> testDevices;

        public DevicesViewModelFixture()
        {
            regionManagerMock = new Mock<IRegionManager>();
            

            testDevices = new List<Device>();
            testDevices.Add(new Device() { Id = 0, Type = "pipette", DeviceHealth = DeviceHealth.ok, LastUsed = DateTime.Now.Date,Price=0.22, Location= "Vohipaho", Color="#4a2b5b" });
            testDevices.Add(new Device() { Id = 1, Type = "shaker", DeviceHealth = DeviceHealth.broken, LastUsed = DateTime.Now.Date, Price = 1.22, Location = "Berzosilla", Color = "#044446" });
            
            deviceRepositoryMock = new Mock<IDevicesRepository>();
            deviceRepositoryMock.Setup<IEnumerable<Device>>(x => x.GetDevices()).Returns(testDevices);
            deviceRepositoryMock.Setup(x => x.CreateDevice(It.IsAny<Device>())).Callback<Device>((device)=> testDevices.Add(device));
            deviceRepositoryMock.Setup(x => x.DeleteDevice(It.IsAny<int>())).Callback<int>((id) => 
            {
                var cur = testDevices.FirstOrDefault(device => device.Id == id);
                testDevices.Remove(cur);
            });
            deviceRepositoryMock.Setup(x => x.UpdateDevice(It.IsAny<Device>())).Callback<Device>((device) =>
            {
                var index = testDevices.FindIndex(div => div.Id == device.Id);
                testDevices[index] = device;
            });

        }

        [Fact]
        public void GetDeviceDataOnStartup()
        {
            var vm = new DevicesViewModel(regionManagerMock.Object,deviceRepositoryMock.Object);
            deviceRepositoryMock.Verify(x => x.GetDevices(), Times.Once);
            Assert.Equal(testDevices.Count(), vm.Devices.Count());
        }

        [Fact]
        public void CreateDeviceData()
        {
            var testIdChangedStartValue = -1;
            var testCreate = new Device() { Id = testIdChangedStartValue, Type = "shaker", DeviceHealth = DeviceHealth.good, LastUsed = DateTime.Now.Date, Price = 1.22, Location = "Berzosilla", Color = "#044446" };

            var vm = new DevicesViewModel(regionManagerMock.Object, deviceRepositoryMock.Object);
            vm.AddDeviceCommand.Execute(testCreate);
            
            deviceRepositoryMock.Verify(x => x.CreateDevice(It.IsAny<Device>()), Times.Once);
            Assert.Equal(testDevices.Count(), vm.Devices.Count());
            Assert.NotEqual(testIdChangedStartValue, testCreate.Id);
        }

        [Fact]
        public void UpdateDeviceData()
        {

            var checkUpdatDevice = (Device)testDevices.First().Clone();
            var currentPrice = checkUpdatDevice.Price;

            checkUpdatDevice.Price = currentPrice + 3.33;
            var vm = new DevicesViewModel(regionManagerMock.Object, deviceRepositoryMock.Object);
            vm.UpdateDeviceCommand.Execute(checkUpdatDevice);


            deviceRepositoryMock.Verify(x => x.UpdateDevice(It.IsAny<Device>()), Times.Once);
            var foundInRepo = testDevices.First(div => div.Id == checkUpdatDevice.Id);
            Assert.Equal(foundInRepo.Price, checkUpdatDevice.Price);
        }

        [Fact]
        public void DeleteDeviceData()
        {

            var testDevice = testDevices.First();

            var vm = new DevicesViewModel(regionManagerMock.Object, deviceRepositoryMock.Object);
            vm.DeleteDeviceCommand.Execute(testDevice);

            deviceRepositoryMock.Verify(x => x.DeleteDevice(It.IsAny<int>()), Times.Once);
            Assert.Equal(testDevices.Count(), vm.Devices.Count());
            Assert.DoesNotContain(testDevices, (device) => device.Id == testDevice.Id);
        }


    }
}
