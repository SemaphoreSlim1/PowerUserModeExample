using PowerUserMode.Wpf.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q2
{
    public class Q2ViewModel : BindableBase, IQ2ViewModel
    {
        private ObservableCollection<ISelectable> availableOptions;
        public IEnumerable<ISelectable> AvailableOptions { get { return availableOptions; } }

        private bool isNotValid = false;
        public bool IsNotValid
        {
            get { return isNotValid; }
            set { SetProperty(ref isNotValid, value); }
        }

        private IPowerConfiguration powerSettings;
        private IEventAggregator eventAggregator;


        public Q2ViewModel(IPowerConfiguration powerSettings, IEventAggregator eventAggregator)
        {
            this.powerSettings = powerSettings;
            this.eventAggregator = eventAggregator;

            availableOptions = new ObservableCollection<ISelectable>()
            {
                new Selectable<bool>("Valid Option 1",true),
                new Selectable<bool>("Invalid Option 2",false),
                new Selectable<bool>("Valid Option 3", true)
            };

            foreach (var option in AvailableOptions)
            {
                option.PropertyChanged += Option_PropertyChanged;
            }
        }

        private void Option_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var option = sender as ISelectable<bool>;
            if (option == null)
            { return; }

            //remember, at this point, the value has already changed.
            //we just need to make sure that it doesn't need to be changed back

            var message = string.Format("Are you sure you want to change {0} from {1} to {2}?",
                option.DisplayText,
                !option.IsSelected,
                option.IsSelected);

            var promptResult = MessageBoxResult.Yes;

            if (powerSettings.SuppressValueChangedWarnings == false)
            {
                promptResult = MessageBox.Show(message, string.Empty, MessageBoxButton.YesNo);
            }

            if (promptResult == MessageBoxResult.Yes)
            {
                ValidateOption(option);
            }
            else
            {
                //we need to revert the value
                //unhook the INPC handler, do the change, and then wire it back up
                option.PropertyChanged -= Option_PropertyChanged;
                option.IsSelected = !option.IsSelected;
                option.PropertyChanged += Option_PropertyChanged;
            }
        }

        private void ValidateOption(ISelectable<bool> option)
        {
            if (option.Value || (option.Value == false && option.IsSelected == false))
            {
                //we've got a valid value, or we've deselected an invalid value.
                //so we're good to go
                IsNotValid = false;
                eventAggregator.GetEvent<ResponseProvidedEvent>().Publish(new ResponseProvidedInfo());
            }
            else
            {
                var message = string.Format("{0} is not a valid option. Do you want to select it anyway?", option.DisplayText);

                var promptResult = MessageBoxResult.Yes;

                if (powerSettings.SuppressValidationWarnings == false)
                {
                    promptResult = MessageBox.Show(message, string.Empty, MessageBoxButton.YesNo);
                }

                if (promptResult == MessageBoxResult.No)
                {
                    //the user wishes to revert the value
                    //unhook the INPC handler, do the change, and then wire it back up
                    option.PropertyChanged -= Option_PropertyChanged;
                    option.IsSelected = !option.IsSelected;
                    option.PropertyChanged += Option_PropertyChanged;
                }else
                {
                    //the user didn't make a valid choice, but they chose to press on even after warning
                    //so alert the system that a response was provided
                    eventAggregator.GetEvent<ResponseProvidedEvent>().Publish(new ResponseProvidedInfo());
                }

                //now let's toggle the validation state based on whether the item is selected
                IsNotValid = option.IsSelected;
            }
        }
    }
}
