using CommunityToolkit.Mvvm.Messaging;
using Graphing.ViewModels;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Graphing.ViewModels.MainViewModel;

namespace Graphing.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Syncfusion.Windows.Tools.Controls.RibbonWindow
    {
        public MainView()
        {
            InitializeComponent();
            SfSkinManager.ApplyThemeAsDefaultStyle = true;
            SfSkinManager.ApplicationTheme = GetTheme(GraphingApplicationTheme.Dark);
            WeakReferenceMessenger.Default.Register<ChangeThemeMessage>(this, (r, m) => SfSkinManager.SetTheme(this, GetTheme(m.Theme)));
        }
         public Theme GetTheme(GraphingApplicationTheme theme)
        {
            switch (theme)
            {
                case GraphingApplicationTheme.Default:
                    return new Theme("Windows11Dark");
                case GraphingApplicationTheme.Light:
                    return new Theme("Office2019Colorful");
                case GraphingApplicationTheme.Dark:
                    return new Theme("Office2019Black");
                default:
                    return new Theme("Windows11Light");
            }
        }
    }
   
    public class ChangeThemeMessage
    {
        public GraphingApplicationTheme Theme { get; set; }

        public ChangeThemeMessage(GraphingApplicationTheme theme)
        {
            Theme = theme;
        }
    }

}
