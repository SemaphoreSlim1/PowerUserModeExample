using Prism.Commands;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Landing
{
    public class LandingViewModel : ILandingViewModel
    {
        public string DisplayText { get; private set; }

        public ICommand BeginQuestionsCommand { get; private set; }

        private readonly IRegionManager regionManager;

        public LandingViewModel(IRegionManager regionManager)
        {
            this.DisplayText = "Hello Power User Mode" + Environment.NewLine + "This is the landing page";
            this.BeginQuestionsCommand = new DelegateCommand(BeginQuestionsCommand_Execute);
            this.regionManager = regionManager;
        }

        private void BeginQuestionsCommand_Execute()
        {
            regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.QuestionnaireShell);
        }
    }
}
