using Eppendorf_FSC.Modules.DevicesModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Eppendorf_FSC.Core;

namespace Eppendorf_FSC.Modules.DevicesModule
{
    public class DevicesModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public DevicesModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.DevicesRegion, nameof(Modules.DevicesModule.Views.DevicesView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DevicesView>();
        }
    }
}