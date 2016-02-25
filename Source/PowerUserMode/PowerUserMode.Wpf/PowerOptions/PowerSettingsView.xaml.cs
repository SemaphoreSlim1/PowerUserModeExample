namespace PowerUserMode.Wpf.PowerOptions
{
    /// <summary>
    /// Interaction logic for PowerSettingsView.xaml
    /// </summary>
    public partial class PowerSettingsView
    {
        public PowerSettingsView(IPowerSettingsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
