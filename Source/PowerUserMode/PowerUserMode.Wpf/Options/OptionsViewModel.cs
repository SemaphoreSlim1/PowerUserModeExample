using PowerUserMode.Wpf.Common;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUserMode.Wpf.Options
{
    public class OptionsViewModel : IOptionsViewModel
    {
        private ObservableCollection<ISelectable> options;
        public IEnumerable<ISelectable> Options { get { return options; } }

        private IPowerConfiguration powerConfig;

        public OptionsViewModel(IPowerConfiguration powerConfig)
        {
            this.options = new ObservableCollection<ISelectable>();
            this.powerConfig = powerConfig;
            this.powerConfig.PropertyChanged += PowerConfig_PropertyChanged;

            RepopulateOptions();
        }

        private void PowerConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //something in power user mode switched
            //only repopulate if its something we care about
            if(e.PropertyName == "IsEnabled" || e.PropertyName == PowerSetting.ShowExtendedOptions.ToString())
            {
                RepopulateOptions();
            }
            
        }

        private void RepopulateOptions()
        {
            options.Clear();

            if(powerConfig.ShowExpandedOptions)
            {
                options.Add(new Selectable("Expanded option 1"));
                options.Add(new Selectable("Expanded option 2"));
                options.Add(new Selectable("Expanded option 3"));
                options.Add(new Selectable("Expanded option 4"));
            }
            else
            {
                options.Add(new Selectable("Simple option 1"));
            }
        }

       
    }
}
