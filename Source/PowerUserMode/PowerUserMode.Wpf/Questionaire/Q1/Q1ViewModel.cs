using PowerUserMode.Wpf.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q1
{
    public class Q1ViewModel : BindableBase, IQ1ViewModel
    {
        private ObservableCollection<ISelectable> availableOptions;
        public IEnumerable<ISelectable> AvailableOptions { get { return availableOptions; } }

        private Queue<ISelectable> changeHistory;

        private IPowerConfiguration powerSettings;

        public ICommand OptionSelectedCommand { get; private set; }
        private IEventAggregator eventAggregator;


        public Q1ViewModel(IPowerConfiguration powerSettings, IEventAggregator eventAggregator)
        {
            this.powerSettings = powerSettings;
            this.eventAggregator = eventAggregator;

            availableOptions = new ObservableCollection<ISelectable>()
            {
                new Selectable("Option 1"),
                new Selectable("Option 2"),
                new Selectable("Option 3")
            };

            foreach (var option in AvailableOptions)
            {
                option.PropertyChanged += Option_PropertyChanged;
            }

            OptionSelectedCommand = new DelegateCommand<ISelectable>(OptionSelectedCommand_Execute);

            changeHistory = new Queue<ISelectable>();
        }

        private void Option_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            changeHistory.Enqueue(sender as ISelectable);
        }

        private void OptionSelectedCommand_Execute(ISelectable option)
        {
            var promptResult = MessageBoxResult.Yes;

            var message = string.Format("Are you sure you want to change {0} from {1} to {2}?",
                option.DisplayText,
                !option.IsSelected,
                option.IsSelected);

            if (powerSettings.SuppressValueChangedWarnings == false)
            {
                promptResult = MessageBox.Show(message, string.Empty, MessageBoxButton.YesNo);
            }

            if (promptResult == MessageBoxResult.Yes)
            {
                //do nothing, the happy path has already happened
                changeHistory.Clear();
                eventAggregator.GetEvent<ResponseProvidedEvent>().Publish(new ResponseProvidedInfo());
            }
            else
            {
                //we need to undo what just happened

                while (changeHistory.Count > 0)
                {
                    var item = changeHistory.Dequeue();
                    item.PropertyChanged -= Option_PropertyChanged; //unhook the INPC handler while we change the value
                    item.IsSelected = !item.IsSelected;
                    item.PropertyChanged += Option_PropertyChanged; //restore the INPC handler
                }


            }
        }
    }
}
