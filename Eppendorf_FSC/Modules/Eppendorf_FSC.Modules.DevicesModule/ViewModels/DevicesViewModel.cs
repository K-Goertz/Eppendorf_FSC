using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Core.Models;
using Eppendorf_FSC.Core.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eppendorf_FSC.Modules.DevicesModule.ViewModels
{
    public class DevicesViewModel : RegionViewModelBase
    {
        private IDevicesRepository devicesRepository;

        private ObservableCollection<Device> devices = new ObservableCollection<Device>();
        public ObservableCollection<Device> Devices
        {
            get { return devices; }
        }

        public DevicesViewModel(IRegionManager regionManager, IDevicesRepository devicesRepository) : base(regionManager)
        {
            this.devicesRepository = devicesRepository;
            devices.AddRange(this.devicesRepository.GetDevices());
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //TODO: do something or remove
        }

    }
}
