using Graphing.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Graphing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF1cX2hIfEx3WmFZfVtgdl9HZFZTRWY/P1ZhSXxWd0RjWH5ac3JURmReUEN9XEM=");
            //base.OnStartup(e);

            //SplashScreenView splashScreenView = new Views.SplashScreenView();
            //splashScreenView.Show();

            MainView mainView = new Views.MainView();
            mainView.Show();

            //splashScreenView.Close();

        }
    }

}
