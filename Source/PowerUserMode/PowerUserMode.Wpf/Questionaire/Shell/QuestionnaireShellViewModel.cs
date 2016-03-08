using PowerUserMode.Wpf.Common;
using Prism.Commands;
using Prism.Events;
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
        private readonly IEventAggregator eventAggregator;
        private readonly IPowerConfiguration powerSettings;

        public QuestionnaireShellViewModel(IPowerConfiguration powerSettings, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.powerSettings = powerSettings;
            this.regionManager = regionManager;
            this.AdvanceNextCommand = new DelegateCommand(AdvanceNextCommand_Execute);
            this.AdvancePreviousCommand = new DelegateCommand(AdvancePreviousCommand_Execute);
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<ResponseProvidedEvent>().Subscribe(ResponseProvided, ThreadOption.PublisherThread,true,filter => powerSettings.AutoNext);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            currentQuestionIndex = 0;
            regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {          
        }

        private void ResponseProvided(ResponseProvidedInfo info)
        {
            AdvanceNextCommand_Execute();
        }

        private void AdvanceNextCommand_Execute()
        {
            switch (currentQuestionIndex)
            {
                case 0:
                    currentQuestionIndex++;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question2);
                    break;
                case 1:
                    currentQuestionIndex++;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question3);
                    break;
                case 2:
                    currentQuestionIndex = 0;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
                    break;
                default:
                    regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
                    break;
            }
        }

        private void AdvancePreviousCommand_Execute()
        {
            switch (currentQuestionIndex)
            {
                case 0:
                    currentQuestionIndex = 2;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question3);
                    break;
                case 1:
                    currentQuestionIndex--;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question1);
                    break;
                case 2:
                    currentQuestionIndex--;
                    regionManager.RequestNavigate(KnownRegions.Questions, KnownViews.Question2);
                    break;
                default:
                    regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
                    break;
            }
        }
    }
}
