using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;

namespace PowerUserMode.Activities
{
    public class RegionNavigationActivity : NativeActivity
    {
        /// <summary>
        /// Gets and sets the name of the region that will house the content
        /// </summary>
        public InArgument<string> RegionName { get; set; }

        /// <summary>
        /// Gets and sets the name of the view as it was registered with the container
        /// </summary>
        public InArgument<string> ViewName { get; set; }

        /// <summary>
        /// Gets and sets the <see cref="RegionManager" /> which will perform the navigation
        /// </summary>
        public InArgument<RegionManager> RegionManager { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            var regionManager = RegionManager.Get(context);
            var regionName = RegionName.Get(context);
            var viewName = ViewName.Get(context);

            Application.Current.Dispatcher.Invoke(() =>
            {
                //navigation must be executed on the ui thread
                Console.WriteLine("{0} is getting view {1}", regionName, viewName);
                regionManager.RequestNavigate(regionName, viewName);
            });
        }
    }
}
