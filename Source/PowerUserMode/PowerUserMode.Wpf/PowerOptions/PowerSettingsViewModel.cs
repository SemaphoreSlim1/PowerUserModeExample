using Prism.Mvvm;

namespace PowerUserMode.Wpf.PowerOptions
{
    public class PowerSettingsViewModel : BindableBase, IPowerSettingsViewModel
    {        
        private IPowerConfigurationEditor powerSettingsEditor;

        public bool AutoNextSubscribed
        {
            get { return powerSettingsEditor.IsSubscribed(PowerSetting.AutoNext); }
            set
            {
                SetSubscription(PowerSetting.AutoNext, value);                
                OnPropertyChanged();
            }
        }

        public bool ShowExtendedOptionsSubscribed
        {
            get { return powerSettingsEditor.IsSubscribed(PowerSetting.ShowExtendedOptions); }
            set
            {
                SetSubscription(PowerSetting.ShowExtendedOptions, value);
                OnPropertyChanged();
            }
        }

        public bool SuppressWarningsSubscribed
        {
            get { return powerSettingsEditor.IsSubscribed(PowerSetting.SuppressWarnings); }
            set
            {
                SetSubscription(PowerSetting.SuppressWarnings, value);
                OnPropertyChanged();
            }
        }

        private void SetSubscription(PowerSetting setting, bool value)
        {
            if(value)
            {
                powerSettingsEditor.Subscribe(setting);
            }else
            {
                powerSettingsEditor.Unsubscribe(setting);
            }
        }

        public PowerSettingsViewModel(IPowerConfigurationEditor powerSettingsEditor)
        {
            this.powerSettingsEditor = powerSettingsEditor;
        }
    }
}
