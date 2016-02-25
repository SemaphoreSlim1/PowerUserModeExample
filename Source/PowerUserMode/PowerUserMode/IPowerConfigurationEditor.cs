using System.ComponentModel;

namespace PowerUserMode
{
    public interface IPowerConfigurationEditor : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets and sets whether or not the collection of subscribed <see cref="PowerSetting"/>s are enabled
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Determines if the user is subscribed to a particular <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting</param>
        /// <returns>Whether or not the user is subscribed to the <see cref="PowerSetting"/></returns>
        bool IsSubscribed(PowerSetting setting);

        /// <summary>
        /// Subscribes to a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-in to</param>
        void Subscribe(PowerSetting setting);

        /// <summary>
        /// Unsubscribes from a <see cref="PowerSetting"/>
        /// </summary>
        /// <param name="setting">The power setting to opt-out of</param>
        void Unsubscribe(PowerSetting setting);
    }
}
