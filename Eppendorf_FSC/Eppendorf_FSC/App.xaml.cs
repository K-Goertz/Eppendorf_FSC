using Eppendorf_FSC.Modules.DevicesModule;
using Eppendorf_FSC.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Mvvm;
using System.Reflection;
using System;
using Eppendorf_FSC.Core.Interfaces;
using Eppendorf_FSC.Services.DeviceService;
using AutoMapper;
using Eppendorf_FSC.Core.Models;
using Eppendorf_FSC.Core.Dto;
using Eppendorf_FSC.Mapping;

namespace Eppendorf_FSC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            //Resolve core shell container
            return Container.Resolve<ShellView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Add services here
            containerRegistry.RegisterSingleton<IDevicesRepository, FileDeviceRepositoryService>();
            containerRegistry.RegisterSingleton(typeof(IMapper),CreateMapper);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //Add Modules with ViewModels,Views ...
            moduleCatalog.AddModule<DevicesModule>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //Change default naming scheme to name Views "[Name]View" and ViewModels "[Name]ViewModel"
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}Model, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        private IMapper CreateMapper() 
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<GeneralMapProfile>();
            });
            return new Mapper(config);
        }


    }
}
