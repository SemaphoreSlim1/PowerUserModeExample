using System;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Landing.DesignTime
{
    public class LandingViewModel : ILandingViewModel
    {
        public ICommand BeginQuestionsCommand { get; set; }

        public string DisplayText
        {
            get { return "Hello World"; }
        }
    }
}
