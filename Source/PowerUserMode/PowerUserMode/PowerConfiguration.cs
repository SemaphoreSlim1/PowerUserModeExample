using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PowerUserMode
{
    /// <summary>
    /// A collection of settings for power user mode
    /// </summary>
    public class PowerConfiguration : IPowerConfiguration, IPowerConfigurationEditor
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IAppSettings appSettings;

        public PowerConfiguration(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        private bool isEnabled;
        
        /// <summary>
        /// Gets and sets whether or not the collection of subscribed <see cref="PowerSetting"/>s are enabled
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if(isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged();

                    var allSettings = Enum.GetValues(typeof(PowerSetting)).Cast<PowerSetting>();
                    foreach(var setting in allSettings)
                    {
                        SetPropertyEnabled(setting,value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets whether or not the user can see an expanded list of options
        /// </summary>
        public bool ShowExpandedOptions
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.ShowExtendedOptions);
            }           
        }

        /// <summary>
        /// Gets whether or not the application will automatically advance to the next screen when appropriate
        /// </summary>
        public bool AutoNext
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.AutoNext);
            }            
        }

        /// <summary>
        /// Gets whether warning dialog boxes will be suppressed
        /// </summary>
        /// <remarks>
        /// This option will not suppress validation, rather, it only suppresses the warning message
        /// </remarks>
        public bool SuppressWarnings
        {
            get
            {
                return GetPropertyEnabled(PowerSetting.SuppressWarnings);
            }           
        }

        /// <summary>
        /// Determines if the user is subscribed to a particular <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>Whether or not the user is subscribed to the <see cref="PowerSetting"/></returns>
        public bool IsSubscribed(PowerSetting setting)
        {
            return appSettings.GetIsSubscribed(setting);
        }

        /// <summary>
        /// Subscribes to a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-in to</param>
        public void Subscribe(PowerSetting setting)
        {
            appSettings.SetIsSubscribed(setting, true);
            SetPropertyEnabled(setting, IsEnabled);
        }

        /// <summary>
        /// Unsubscribes from a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-out of</param>
        public void Unsubscribe(PowerSetting setting)
        {
            //turn the power setting off, THEN unsubscribe
            SetPropertyEnabled(setting, false);
            appSettings.SetIsSubscribed(setting, false);            
        }

        /// <summary>
        /// Gets a <see cref="PowerSetting"/>'s enabled state, provided that it is currently being subscribed to
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>Whether the <see cref="PowerSetting"/> is currently enabled</returns>
        private bool GetPropertyEnabled(PowerSetting setting)
        {
            if (appSettings.GetIsSubscribed(setting))
            {
                return appSettings.GetIsEnabled(setting);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets a <see cref="PowerSetting"/>'s enabled state to the new value, provided that is currently being subscribed to
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <param name="value">The new value</param>
        private void SetPropertyEnabled(PowerSetting setting, bool value)
        {
            if (appSettings.GetIsSubscribed(setting))
            {
                //only set the value and raise INPC if the incoming value is different
                var oldEnabled = appSettings.GetIsEnabled(setting);
                if (oldEnabled != value)
                {
                    appSettings.SetIsEnabled(setting, value);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event, if there are subscribers
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
