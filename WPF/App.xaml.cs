using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        public App()
        {
            var services = new ServiceCollection();

            services.AddScoped<IFileWatcherDal, FileWatcherDal>();
            services.AddScoped<IFileWatcherService, FileWatcherManager>();
            services.AddScoped<IDataImporterService, DataImporterManager>();
            services.AddScoped<IDataImporterDal, DataImporterDal>();

            services.AddSingleton<DispatcherTimer>();

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

           

            MainWindow mainWindow = new MainWindow(_serviceProvider.GetRequiredService<IFileWatcherService>(), _serviceProvider.GetRequiredService<IDataImporterService>());
            mainWindow.Show();
        }
    }
}
