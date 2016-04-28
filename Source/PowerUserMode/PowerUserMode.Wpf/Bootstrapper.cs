using Prism.Unity;
using Microsoft.Practices.Unity;
using System.Windows;
using PowerUserMode.Core;
using PowerUserMode.Wpf.Options;
using PowerUserMode.Wpf.Landing;
using Prism.Regions;
using PowerUserMode.Wpf.Shell;
using PowerUserMode.Wpf.PowerOptions;
using PowerUserMode.Wpf.Questionaire.Q1;
using PowerUserMode.Wpf.Questionaire.Shell;
using PowerUserMode.Wpf.Questionaire.Q2;
using PowerUserMode.Wpf.Questionaire.Q3;

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
            
            //application options view
            Container.RegisterType<OptionsView>(KnownViews.Options);
            Container.RegisterType<IOptionsViewModel, OptionsViewModel>();

            //power settings view
            Container.RegisterType<PowerSettingsView>(KnownViews.PowerSettings);
            Container.RegisterType<IPowerSettingsViewModel, PowerSettingsViewModel>();


            //register the types for the questionnaire
            Container.RegisterType<QuestionnaireShellView>(KnownViews.QuestionnaireShell);
            Container.RegisterType<IQuestionnaireShellViewModel, QuestionnaireShellViewModel>();

            Container.RegisterType<Q1View>(KnownViews.Question1);
            Container.RegisterType<IQ1ViewModel, Q1ViewModel>();

            Container.RegisterType<Q2View>(KnownViews.Question2);
            Container.RegisterType<IQ2ViewModel, Q2ViewModel>();

            Container.RegisterType<Q3View>(KnownViews.Question3);
            Container.RegisterType<IQ3ViewModel, Q3ViewModel>();
                  
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(OptionsView));
            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(LandingView));
            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(PowerSettingsView));
            regionManager.RegisterViewWithRegion(KnownRegions.MainWindow, typeof(QuestionnaireShellView));

            regionManager.RegisterViewWithRegion(KnownRegions.Questions, typeof(Q1View));
            regionManager.RegisterViewWithRegion(KnownRegions.Questions, typeof(Q2View));
            regionManager.RegisterViewWithRegion(KnownRegions.Questions, typeof(Q3View));

            regionManager.RequestNavigate(KnownRegions.MainWindow, KnownViews.Landing);
        }
    }
}
