using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WeatherDeskk.Services;
using WeatherDeskk.ViewModels;

namespace WeatherDeskk
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                Services = serviceCollection.BuildServiceProvider();

                var mainWindow = Services.GetRequiredService<MainWindow>();
                mainWindow.DataContext = Services.GetRequiredService<MainViewModel>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Startup error: {ex.Message}\n\n{ex}",
                    "Application Startup Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IThemeService, ThemeService>();

            services.AddSingleton<MainWindow>();
        }
    }
}