using System.Windows.Input;

namespace PowerUserMode.Wpf.Shell
{
    public interface IShellViewModel
    {
        ICommand ApplicationOptionsCommand { get; }

        ICommand PowerUserOptionsCommand { get; }

        ICommand HomeCommand { get; }

        bool PowerSettingsEnabled { get; }
    }
}
