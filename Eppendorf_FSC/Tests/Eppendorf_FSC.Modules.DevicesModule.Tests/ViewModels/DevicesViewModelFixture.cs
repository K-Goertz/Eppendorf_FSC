using Eppendorf_FSC.Modules.DevicesModule.ViewModels;
using Moq;
using Prism.Regions;
using Xunit;

namespace Eppendorf_FSC.Modules.DevicesModule.Tests.ViewModels
{
    public class DevicesViewModelFixture
    {
        Mock<IRegionManager> _regionManagerMock;
        const string MessageServiceDefaultMessage = "Some Value";

        public DevicesViewModelFixture()
        {
            //TODO: remove line
            //messageService.Setup(x => x.GetMessage()).Returns(MessageServiceDefaultMessage);
            _regionManagerMock = new Mock<IRegionManager>();
        }

        //TODO: remove 
        //[Fact]
        //public void MessagePropertyValueUpdated()
        //{
        //    var vm = new DevicesViewModel(_regionManagerMock.Object);

        //    //_messageServiceMock.Verify(x => x.GetMessage(), Times.Once);

        //    Assert.Equal(MessageServiceDefaultMessage, vm.Message);
        //}

        [Fact]
        public void MessageINotifyPropertyChangedCalled()
        {
            var vm = new DevicesViewModel(_regionManagerMock.Object);
            Assert.PropertyChanged(vm, nameof(vm.Message), () => vm.Message = "Changed");
        }
    }
}
