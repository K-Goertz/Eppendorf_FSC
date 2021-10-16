using Prism.Mvvm;

namespace Eppendorf_FSC.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _title = "Eppendorf Full Stack Challenge";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShellViewModel()
        {

        }
    }
}
