using System.Windows.Input;

namespace PowerUserMode.Wpf.Landing
{
    public interface ILandingViewModel
    {
        string DisplayText { get; }

        ICommand BeginQuestionsCommand { get; }
    }
}
