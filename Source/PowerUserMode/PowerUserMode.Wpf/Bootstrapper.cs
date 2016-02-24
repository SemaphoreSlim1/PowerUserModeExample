using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;
using PowerUserMode.Wpf.Options;
using PowerUserMode.Wpf.Landing;
using Prism.Regions;
using PowerUserMode.Wpf.Shell;

namespace PowerUserMode.Wpf
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IAppSettings, PowerAppSettings>(new ContainerControlledLifetimeManager());
            Container.RegisterType<PowerConfiguration>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPowerConfiguration, PowerConfiguration>();
            Container.RegisterType<IPowerConfigurationEditor, PowerConfiguration>();

            //the shell
            Container.RegisterType<ShellWindow>();
            Container.RegisterType<IShellViewModel, ShellViewModel>();

            //landing view
            Container.RegisterType<LandingView>(KnownViews.Landing);
            Container.RegisterType<ILandingViewModel, LandingViewModel>();


            //options view
            Container.RegisterType<OptionsView>(KnownViews.Options);
            Container.RegisterType<IOptionsViewModel, OptionsViewModel>();
            
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(OptionsView));
            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(LandingView));

            regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
        }
    }
}
