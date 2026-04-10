using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using WeatherDeskk.Resources;

namespace WeatherDeskk.Services
{
    public class LocalizationService : INotifyPropertyChanged, ILocalizationService
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationService()
        {
            _resourceManager = Strings.ResourceManager;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string this[string key]
        {
            get
            {
                return _resourceManager.GetString(
                    key,
                    Thread.CurrentThread.CurrentUICulture
                ) ?? key;
            }
        }

        public void ChangeLanguage(string cultureCode)
        {
            Thread.CurrentThread.CurrentUICulture =
                new CultureInfo(cultureCode);

            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(null)
            );
        }
    }
}