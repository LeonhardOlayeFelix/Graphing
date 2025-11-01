using Graphing.Views;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            
        }
        public Theme GetTheme(GraphingApplicationTheme theme)
        {
            switch (theme)
            {
                case GraphingApplicationTheme.Default:
                    return new Theme("Windows11Dark");
                case GraphingApplicationTheme.Light:
                    return new Theme("Windows11Light");
                case GraphingApplicationTheme.Dark:
                    return new Theme("Windows11Dark");
                default:
                    return new Theme("Windows11Light");
            }
        }
        public enum GraphingApplicationTheme
        {
            Default,
            Light,
            Dark
        }
    }

}
