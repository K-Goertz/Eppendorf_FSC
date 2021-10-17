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


        public ObservableCollection<Device> Devices { get; } = new ObservableCollection<Device>();
        public ICollectionView DevicesCollectionView { get; private set; }

        
        public ReadOnlyCollection<DeviceHealth> DeviceHealthSelectables { get; } = new ReadOnlyCollection<DeviceHealth>(Enum.GetValues(typeof(DeviceHealth)).Cast<DeviceHealth>().ToList());//Collects possible device health values

        private DelegateCommand<Device> deleteDeviceCommand;
        public ICommand DeleteDeviceCommand
        {
            get
            {
                if (deleteDeviceCommand == null)
                {
                    //TODO: add can excecute ((device)=>device != null)
                    deleteDeviceCommand = new DelegateCommand<Device>(DeleteDevice);
                }
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
            Devices.AddRange(this.devicesRepository.GetDevices());
            DevicesCollectionView = new ListCollectionView(Devices);


        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //TODO: do something or remove
        }

        private void DeleteDevice(Device device) 
        {
            if (device == null)
                return;

            if(Devices.Contains(device))
                Devices.Remove(device);
            devicesRepository.DeleteDevice(device.Id);
        }

        private void AddDevice(Device device)
        {
            if (device == null)
                return;

            //TODO: I pick the max id and add 1 this could be not true for the database (should be clear by convention).
            var lastMaxDevice = Devices.Max(dev => dev.Id);
            lastMaxDevice += 1;
            device.Id = lastMaxDevice;
            devicesRepository.CreateDevice(device);

            //The new Device could already be added (datagrid ..) so do not add if device is already in the list
            if(!Devices.Contains(device))
                Devices.Add(device);
        }

        private void UpdateDevice(Device device)
        {
            if (device == null)
                return;
            devicesRepository.UpdateDevice(device);
        }

    }
}
