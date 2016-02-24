using PowerUserMode.Wpf.Common;
using System.Collections.Generic;

namespace PowerUserMode.Wpf.Options
{
    public interface IOptionsViewModel
    {
        IEnumerable<ISelectable> Options { get; }
    }
}
