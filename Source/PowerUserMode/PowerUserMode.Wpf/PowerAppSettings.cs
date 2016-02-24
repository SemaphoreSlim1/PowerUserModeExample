using System;
using System.Configuration;
using System.Linq;

namespace PowerUserMode.Wpf
{
    /// <summary>
    /// Provides read/write access to the app.config file specific to the needs of power settings
    /// </summary>
    public class PowerAppSettings : IAppSettings
    {
        public bool GetIsEnabled(PowerSetting setting)
        {
            var key = GenerateEnabledKey(setting);
            return ReadValue(key);
        }

        public bool GetIsSubscribed(PowerSetting setting)
        {
            var key = GenerateSubscriptionKey(setting);
            return ReadValue(key);
        }

        public void SetIsEnabled(PowerSetting setting, bool value)
        {
            var key = GenerateEnabledKey(setting);
            WriteValue(key, value);
        }

        public void SetIsSubscribed(PowerSetting setting, bool value)
        {
            var key = GenerateSubscriptionKey(setting);
            WriteValue(key, value);
        }

        /// <summary>
        /// Generate the settings key used for determining if a <see cref="PowerSetting"/> is enabled.
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>The configuration key for the power setting</returns>
        private string GenerateEnabledKey(PowerSetting setting)
        {
           return "PowerUser." + setting.ToString() + ".IsEnabled";
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
            var value = false;

            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                var settingValue = ConfigurationManager.AppSettings[key];

                if (Boolean.TryParse(settingValue, out value) == false)
                {
                    //a value was present for this key, but it did not convert to a boolean
                    value = false;
                }
            }

            return value;
        }

        private void WriteValue(string key, bool value)
        {
            ConfigurationManager.AppSettings.Remove(key);
            ConfigurationManager.AppSettings.Add(key, value.ToString());
        }
    }
}
