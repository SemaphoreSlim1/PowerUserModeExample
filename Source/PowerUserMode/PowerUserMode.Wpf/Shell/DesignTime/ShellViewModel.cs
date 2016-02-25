using System.Windows.Input;

namespace PowerUserMode.Wpf.Shell.DesignTime
{
    public class ShellViewModel : IShellViewModel
    {
        public ICommand ApplicationOptionsCommand { get; set; }

        public ICommand PowerUserOptionsCommand { get; set; }

        public ICommand HomeCommand { get; set; }

        public bool PowerSettingsEnabled { get; set; }

        public ShellViewModel()
        {
            PowerSettingsEnabled = true;
        }
    }
}
