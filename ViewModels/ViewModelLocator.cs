using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Graphing.ViewModels
{
    public class ViewModelLocator
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider => _serviceProvider;

        public MainViewModel MainViewModel => _serviceProvider.GetRequiredService<MainViewModel>();

        public ViewModelLocator()
        {
            Initialise();
        }

        public void Initialise()
        {
            if (_serviceProvider != null) return;

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainViewModel>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
