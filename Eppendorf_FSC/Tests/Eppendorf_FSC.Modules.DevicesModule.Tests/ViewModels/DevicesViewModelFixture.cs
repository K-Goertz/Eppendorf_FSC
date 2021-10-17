using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Modules.DevicesModule.ViewModels;
using System.Collections.Generic;
using Moq;
using Prism.Regions;
using Xunit;
using System.Linq;
using Eppendorf_FSC.Core.Models;

namespace Eppendorf_FSC.Modules.DevicesModule.Tests.ViewModels
{
    public class DevicesViewModelFixture
    {

        private List<Device> testDevices = new List<Device>();
        const string MessageServiceDefaultMessage = "Some Value";
        Mock<IRegionManager> regionManagerMock;
        Mock<IDevicesRepository> deviceRepositoryMock; 

        public DevicesViewModelFixture()
        {
            regionManagerMock = new Mock<IRegionManager>();
            deviceRepositoryMock = new Mock<IDevicesRepository>();
            deviceRepositoryMock.Setup<IEnumerable<Device>>(x => x.GetDevices()).Returns(testDevices);
        }

        [Fact]
        public void GetDeviceDataOnStartup()
        {
            var vm = new DevicesViewModel(regionManagerMock.Object,deviceRepositoryMock.Object);
            deviceRepositoryMock.Verify(x => x.GetDevices(), Times.Once);
            Assert.Equal(testDevices.Count(), vm.Devices.Count());
        }


    }
}
