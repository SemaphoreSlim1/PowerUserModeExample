using PowerUserMode.Wpf.Common;
using System.Collections.Generic;
using System.Windows.Input;

namespace PowerUserMode.Wpf.Questionaire.Q2.DesignTime
{
    public class Q2ViewModel : IQ2ViewModel
    {
        public IEnumerable<ISelectable> AvailableOptions { get; set; }

        public bool IsNotValid { get { return false; } }

        public Q2ViewModel()
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
