using PowerUserMode.Wpf.Common;
using System.Collections.Generic;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q2
{
    public interface IQ2ViewModel
    {
        IEnumerable<ISelectable> AvailableOptions { get; }
        
        bool IsNotValid { get; }        
    }
}
