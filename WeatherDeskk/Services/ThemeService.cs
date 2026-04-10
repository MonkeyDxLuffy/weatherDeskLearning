using System;
using System.Windows;

namespace WeatherDeskk.Services
{
    public class ThemeService : IThemeService
    {
        public void SetLightTheme()
        {
            ApplyTheme("pack://application:,,,/Themes/LightTheme.xaml");
        }

        public void SetDarkTheme()
        {
            ApplyTheme("pack://application:,,,/Themes/DarkTheme.xaml");
        }

        private void ApplyTheme(string themePath)
        {
            var dictionary = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Absolute)
            };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
    }
}