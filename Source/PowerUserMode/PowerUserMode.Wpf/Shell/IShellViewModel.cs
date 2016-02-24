using System.Windows.Input;

namespace PowerUserMode.Wpf.Shell
{
    public interface IShellViewModel
    {
        ICommand OptionsCommand { get; }
    }
}
