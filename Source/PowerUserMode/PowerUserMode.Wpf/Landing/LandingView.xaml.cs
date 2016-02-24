using System;
using Prism.Regions;

namespace PowerUserMode.Wpf.Landing
{
    /// <summary>
    /// Interaction logic for LandingView.xaml
    /// </summary>
    public partial class LandingView
    {
        public LandingView(ILandingViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        
    }
}
