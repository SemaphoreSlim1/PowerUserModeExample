using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Shell
{
    public class ShellViewModel : IShellViewModel
    {
        private DelegateCommand optionsCommand;
        private IRegionManager regionManager;

        public ICommand OptionsCommand
        {
            get { return optionsCommand; }
        }

        public ShellViewModel(IRegionManager regionManager)
        {
            optionsCommand = new DelegateCommand(OptionsCommand_Execute);
            this.regionManager = regionManager;
        }

        private void OptionsCommand_Execute()
        {
           regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Options);

        }

        

    }
}
