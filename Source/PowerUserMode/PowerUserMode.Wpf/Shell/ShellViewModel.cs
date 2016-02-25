using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Shell
{
    public class ShellViewModel : BindableBase, IShellViewModel
    {
        private DelegateCommand applicationOptionsCommand;
        private DelegateCommand powerUserOptionsCommand;
        private DelegateCommand homeCommand;

        private IPowerConfigurationEditor powerSettingsEditor;

        private IRegionManager regionManager;

        public ICommand ApplicationOptionsCommand
        {
            get { return applicationOptionsCommand; }
        }

        public ICommand PowerUserOptionsCommand
        {
            get { return powerUserOptionsCommand; }
        }

        public ICommand HomeCommand
        {
            get { return homeCommand; }
        }
        
        public bool PowerSettingsEnabled
        {
            get { return powerSettingsEditor.IsEnabled; }
            set
            {
                powerSettingsEditor.IsEnabled = value;
                OnPropertyChanged();
            }
        }

        public ShellViewModel(IRegionManager regionManager, IPowerConfigurationEditor powerSettingsEditor)
        {
            applicationOptionsCommand = new DelegateCommand(ApplicationOptionsCommand_Execute);
            powerUserOptionsCommand = new DelegateCommand(PowerUserOptionsCommand_Execute);
            homeCommand = new DelegateCommand(HomeCommand_Execute);

            this.regionManager = regionManager;
            this.powerSettingsEditor = powerSettingsEditor;
        }

        private void ApplicationOptionsCommand_Execute()
        {
           regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Options);
        }

        private void PowerUserOptionsCommand_Execute()
        {
            regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.PowerSettings);
        }
        
        private void HomeCommand_Execute()
        {
            regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
        }            
    }
}
