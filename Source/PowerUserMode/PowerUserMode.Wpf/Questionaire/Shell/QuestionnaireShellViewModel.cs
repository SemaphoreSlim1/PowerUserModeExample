using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Shell
{
    public class QuestionnaireShellViewModel : IQuestionnaireShellViewModel, INavigationAware
    {
        public ICommand AdvanceNextCommand { get; private set; }
        
        public ICommand AdvancePreviousCommand { get; private set; }

        private int currentQuestionIndex = 0;

        private readonly IRegionManager regionManager;

        public QuestionnaireShellViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {          
        }
    }
}
