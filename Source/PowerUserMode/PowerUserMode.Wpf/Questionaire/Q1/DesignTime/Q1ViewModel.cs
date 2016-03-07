using System;
using System.Collections.Generic;
using PowerUserMode.Wpf.Common;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q1.DesignTime
{
    public class Q1ViewModel : IQ1ViewModel
    {
        public IEnumerable<ISelectable> AvailableOptions { get; set; }

        public ICommand OptionSelectedCommand { get; private set; }
        
        public Q1ViewModel()
        {
            this.AvailableOptions = new List<ISelectable>()
            {
                new Selectable("Option 1"),
                new Selectable("Option 2"),
                new Selectable("Option 3")
            };
        }
    }
}
