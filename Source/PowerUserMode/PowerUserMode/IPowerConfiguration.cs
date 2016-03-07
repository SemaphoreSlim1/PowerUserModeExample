using System.ComponentModel;

namespace PowerUserMode
{
    public interface IPowerConfiguration : INotifyPropertyChanged
    {        
        /// <summary>
        /// Gets whether or not the user can see an expanded list of options
        /// </summary>
        bool ShowExpandedOptions { get; }
        
        /// <summary>
        /// Gets whether or not the application will automatically advance to the next screen when appropriate
        /// </summary>
        bool AutoNext { get; }

        /// <summary>
        /// Gets whether or not to suppress the warning dialog when a value is changed
        /// </summary>
        bool SuppressValueChangedWarnings { get; }

        /// <summary>
        /// Gets whether validation warning dialog boxes will be suppressed
        /// </summary>
        /// <remarks>
        /// This option will not suppress validation, rather, it only suppresses the warning message
        /// </remarks>
        bool SuppressValidationWarnings { get; }
    }
}
