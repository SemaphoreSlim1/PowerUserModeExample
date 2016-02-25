namespace PowerUserMode.Wpf.PowerOptions.DesignTime
{
    public class PowerSettingsViewModel : IPowerSettingsViewModel
    {
        public bool AutoNextSubscribed { get; set; }

        public bool ShowExtendedOptionsSubscribed { get; set; }

        public bool SuppressWarningsSubscribed { get; set; }
    }
}
