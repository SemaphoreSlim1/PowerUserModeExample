using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerUserMode.Wpf.Common;

namespace PowerUserMode.Wpf.Options.DesignTime
{
    public class OptionsViewModel : IOptionsViewModel
    {
        public IEnumerable<ISelectable> Options { get; set; }

        public OptionsViewModel()
        {
            Options = new ISelectable[]
            {
                new Selectable("Option 1"),
                new Selectable("Option 2"),
                new Selectable("Option 3")
            };
        }
    }
}
