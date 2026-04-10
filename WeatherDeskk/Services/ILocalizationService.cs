using System.ComponentModel;

namespace WeatherDeskk.Services
{
    public interface ILocalizationService : INotifyPropertyChanged
    {
        string this[string key] { get; }

        void ChangeLanguage(string cultureCode);
    }
}