using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Core.Models;
using Eppendorf_FSC.Core.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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
        public ICollectionView DevicesCollectionView { get; private set; }


        private DelegateCommand<Device> deleteDeviceCommand;
        public ICommand DeleteDeviceCommand
        {
            get
            {
                if (deleteDeviceCommand == null)
                    deleteDeviceCommand = new DelegateCommand<Device>(DeleteDevice);
                return deleteDeviceCommand;
            }
        }

        private DelegateCommand<Device> addDeviceCommand;
        public ICommand AddDeviceCommand
        {
            get
            {
                if (addDeviceCommand == null)
                    addDeviceCommand = new DelegateCommand<Device>(AddDevice);
                return addDeviceCommand;
            }
        }

        private DelegateCommand<Device> updateDeviceCommand;
        public ICommand UpdateDeviceCommand
        {
            get
            {
                if (updateDeviceCommand == null)
                    updateDeviceCommand = new DelegateCommand<Device>(UpdateDevice);
                return updateDeviceCommand;
            }
        }


        public DevicesViewModel(IRegionManager regionManager, IDevicesRepository devicesRepository) : base(regionManager)
        {
            this.devicesRepository = devicesRepository;
            devices.AddRange(this.devicesRepository.GetDevices());
            DevicesCollectionView = new ListCollectionView(devices);
        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //TODO: do something or remove
        }

        private void DeleteDevice(Device device) 
        {
            Devices.Remove(device);
            devicesRepository.DeleteDevice(device.Id);
        }

        private void AddDevice(Device device)
        {
            //TODO: I pick the max id and add 1 this could be not true for the database (should be clear by convention).

            var lastMaxDevice = devices.Max(dev => dev.Id);
            lastMaxDevice += 1;
            device.Id = lastMaxDevice;
            devicesRepository.CreateDevice(device);

            //The new Device could already be added (datagrid ..) so do not add if device is already in the list
            if(!Devices.Contains(device))
                Devices.Add(device);
        }

        private void UpdateDevice(Device device)
        {
            devicesRepository.UpdateDevice(device);
        }

    }
}
