using System;

namespace PowerUserMode.Wpf.Landing
{
    public class LandingViewModel : ILandingViewModel
    {
        public string DisplayText { get; private set; }

        public LandingViewModel()
        {
            this.DisplayText = "Hello Power User Mode" + Environment.NewLine + "This is the landing page"; 
        }
    }
}
