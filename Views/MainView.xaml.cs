using Graphing.ViewModels;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            SfSkinManager.ApplyThemeAsDefaultStyle = true;
            SfSkinManager.ApplicationTheme = ((MainViewModel)DataContext).GetTheme(GraphingApplicationTheme.Dark);
        }
        

    }
    
}
