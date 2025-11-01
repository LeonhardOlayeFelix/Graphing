using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Graphing.Views;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Graphing.ViewModels
{
    public class MainViewModel
    {
        private ICommand _lightThemeCommand;
        private ICommand _darkThemeCommand;

        public ICommand LightThemeCommand => _lightThemeCommand ??= new RelayCommand(OnLightTheme);
        public ICommand DarkThemeCommand => _darkThemeCommand ??= new RelayCommand(OnDarkTheme);

        public MainViewModel()
        {
            WeakReferenceMessenger.Default.Send(new ChangeThemeMessage(GraphingApplicationTheme.Dark));
        }
        

        
        public enum GraphingApplicationTheme
        {
            Default,
            Light,
            Dark
        }
        private void OnLightTheme()
        {
            WeakReferenceMessenger.Default.Send(new ChangeThemeMessage(GraphingApplicationTheme.Light));
        }
        private void OnDarkTheme()
        {
            WeakReferenceMessenger.Default.Send(new ChangeThemeMessage(GraphingApplicationTheme.Dark));
        }
    }

}
