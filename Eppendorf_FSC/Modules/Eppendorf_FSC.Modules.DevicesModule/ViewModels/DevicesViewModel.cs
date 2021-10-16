using Eppendorf_FSC.Core.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eppendorf_FSC.Modules.DevicesModule.ViewModels
{
    public class DevicesViewModel : RegionViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { 
                SetProperty(ref message, value); 
            }
        }

        public DevicesViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Message = "Devices view";
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //TODO: do something or remove
        }

    }
}
