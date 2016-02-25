using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUserMode.Wpf.PowerOptions
{
    public interface IPowerSettingsViewModel
    {
        bool AutoNextSubscribed { get; set; }
        bool ShowExtendedOptionsSubscribed { get; set; }
        bool SuppressWarningsSubscribed { get; set; }
    }
}
