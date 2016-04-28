using PowerUserMode.Wpf.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Activities.Extensions.Tracking;
using PowerUserMode.Workflows;
using Prism.Mvvm;

namespace PowerUserMode.Wpf.Questionaire.Shell
{
    public class QuestionnaireShellViewModel : BindableBase, IQuestionnaireShellViewModel, INavigationAware
    {
        public ICommand AdvanceNextCommand { get; private set; }
        
        public ICommand AdvancePreviousCommand { get; private set; }

        private string stateMachineName;

        /// <summary>
        /// Gets the name of the state machine that is currently executing
        /// </summary>
        public string StateMachineName
        {
            get { return stateMachineName; }
            private set { SetProperty(ref stateMachineName, value); }
        }

        private string currentState;

        /// <summary>
        /// Gets the name of the current state within the executing state machine
        /// </summary>
        public string CurrentState
        {
            get { return currentState; }
            private set { SetProperty(ref currentState, value); }
        }

        private bool workflowRunning;

        /// <summary>
        /// Gets whether the workflow engine is currently running
        /// </summary>
        public bool WorkflowRunning
        {
            get { return workflowRunning;}
            private set { SetProperty(ref workflowRunning, value); }
        }

        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IPowerConfiguration powerSettings;
        private StateMachineStateTracker stateTracker;

        private WorkflowApplication workflowApplication;
        private WorkflowApplication WorkflowApp
        {
            get
            {
                if(workflowApplication != null)
                { return workflowApplication;}

                //setup the workflow
                var activity = new QuestionTemplate();
                stateTracker = new StateMachineStateTracker(activity);
                var workflowAppArgs = new Dictionary<string, object>() { {"RegionManager", regionManager} };
                workflowApplication = new WorkflowApplication(activity, workflowAppArgs)
                {
                    Idle = args =>
                    {
                        UpdateState();
                        UpdateStateMachineName();
                        UpdateCommands();
                        Console.WriteLine("State machine idle");
                    },
                    Completed = args =>
                    {
                        UpdateState();
                        UpdateStateMachineName();
                        UpdateRunning(false);
                        Console.WriteLine("State machine completed");
                    },
                    Aborted = args =>
                    {
                        UpdateState();
                        UpdateStateMachineName();
                        UpdateRunning(false);
                        Console.WriteLine("State machine aborted. Reason: " + args.Reason);
                    },
                    OnUnhandledException = args =>
                    {
                        UpdateState();
                        UpdateStateMachineName();
                        UpdateRunning(false);
                        Console.WriteLine("State machine unhandled exception. " + args.UnhandledException.ToString());
                        return UnhandledExceptionAction.Terminate;
                    }
                };

                workflowApplication.Extensions.Add(stateTracker);

                return workflowApplication;
            }
        }

        public QuestionnaireShellViewModel(IPowerConfiguration powerSettings, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.powerSettings = powerSettings;
            this.regionManager = regionManager;
            this.AdvanceNextCommand = new DelegateCommand(AdvanceNextCommand_Execute, AdvanceNextCommand_CanExecute);
            this.AdvancePreviousCommand = new DelegateCommand(AdvancePreviousCommand_Execute, AdvancePreviousCommand_CanExecute);
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<ResponseProvidedEvent>().Subscribe(ResponseProvided, ThreadOption.PublisherThread,true,filter => powerSettings.AutoNext);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            WorkflowApp.Run();
            UpdateRunning(true);
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
            var cmd = AdvanceNextCommand as DelegateCommand;
            if (cmd.CanExecute())
            { cmd.Execute(); }
        }

        private bool AdvanceNextCommand_CanExecute()
        {
            if (WorkflowRunning == false)
            { return false; }

            var bookmarks = WorkflowApp.GetBookmarks();
            return bookmarks.Any(bm => bm.BookmarkName == "Next");
        }

        private void AdvanceNextCommand_Execute()
        {
            WorkflowApp.ResumeBookmark("Next", null);
        }

        private bool AdvancePreviousCommand_CanExecute()
        {
            if (WorkflowRunning == false)
            { return false; }

            var bookmarks = WorkflowApp.GetBookmarks();
            return bookmarks.Any(bm => bm.BookmarkName == "Previous");
        }

        private void AdvancePreviousCommand_Execute()
        {
            WorkflowApp.ResumeBookmark("Previous", null);
        }

        private void UpdateState()
        {
            if (stateTracker == null)
            {
                CurrentState = "";
                return;
            }

            if (string.IsNullOrWhiteSpace(stateTracker.CurrentState))
            {
                CurrentState = "";
                return;
            }

            CurrentState = stateTracker.CurrentState;
        }

        private void UpdateStateMachineName()
        {
            if (stateTracker == null)
            {
                stateMachineName = "Unknown";
                return;
            }

            if (string.IsNullOrWhiteSpace(stateTracker.CurrentState))
            {
                StateMachineName = "Unknown";
                return;
            }

            StateMachineName = stateTracker.CurrentStateMachine;
        }

        private void UpdateRunning(bool newValue)
        {
            WorkflowRunning = newValue;
            UpdateCommands();
        }

        private void UpdateCommands()
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                (AdvanceNextCommand as DelegateCommand).RaiseCanExecuteChanged();
                (AdvancePreviousCommand as DelegateCommand).RaiseCanExecuteChanged();
            });
        }
    }
}
