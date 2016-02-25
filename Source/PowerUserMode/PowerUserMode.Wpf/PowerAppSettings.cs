using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PowerUserMode.Wpf
{
    /// <summary>
    /// Provides read/write access to the app.config file specific to the needs of power settings
    /// </summary>
    public class PowerAppSettings : IAppSettings
    {
        private readonly IDictionary<string, bool> settings;

        public PowerAppSettings()
        {
            //read in from the file
            settings = new Dictionary<string, bool>();
            foreach(var key in ConfigurationManager.AppSettings.AllKeys)
            {
                var boolValue = false;
                var rawSettingValue = ConfigurationManager.AppSettings[key];
                if(bool.TryParse(rawSettingValue, out boolValue))
                {
                    settings[key] = boolValue;
                }                
            }
        }

        public bool GetIsEnabled()
        {
            var key = "PowerUser.IsEnabled";
            return ReadValue(key);
        }

        public bool GetIsSubscribed(PowerSetting setting)
        {
            var key = GenerateSubscriptionKey(setting);
            return ReadValue(key);
        }

        public void SetIsEnabled(bool value)
        {
            var key = "PowerUser.IsEnabled";
            WriteValue(key, value);
        }

        public void SetIsSubscribed(PowerSetting setting, bool value)
        {
            var key = GenerateSubscriptionKey(setting);
            WriteValue(key, value);
        }        

        /// <summary>
        /// Generates the settings key used for determining if a <see cref="PowerSetting"/> is subscribed
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>The configuration key for the power setting</returns>
        private string GenerateSubscriptionKey(PowerSetting setting)
        {
            return "PowerUser." + setting.ToString() + ".IsSubscribed";
        }

        private bool ReadValue(string key)
        {
            return settings[key];
        }

        private void WriteValue(string key, bool value)
        {
            settings[key] = value;
        }
    }
}
