using PowerUserMode.Wpf.Common;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q1
{
    public interface IQ1ViewModel
    {
        IEnumerable<ISelectable> AvailableOptions { get; }

        ICommand OptionSelectedCommand { get; }
    }
}
