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
            base.OnStartup(e);

            //Splash screen
            SplashScreenView splashScreenView = new Views.SplashScreenView();
            splashScreenView.Show();

            //MainView mainView = new Views.MainView();
            //mainView.Show();

            //splashScreenView.Close();

        }
    }

}
