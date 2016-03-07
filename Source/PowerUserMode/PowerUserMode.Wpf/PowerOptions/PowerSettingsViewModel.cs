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

        public bool SuppressValueChangedWarningsSubscribed
        {
            get { return powerSettingsEditor.IsSubscribed(PowerSetting.SuppressValueChangedWarnings); }
            set
            {
                SetSubscription(PowerSetting.SuppressValueChangedWarnings, value);
                OnPropertyChanged();
            }
        }

        public bool SuppressValidationWarningsSubscribed
        {
            get { return powerSettingsEditor.IsSubscribed(PowerSetting.SuppressValidationWarnings); }
            set
            {
                SetSubscription(PowerSetting.SuppressValidationWarnings, value);
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
