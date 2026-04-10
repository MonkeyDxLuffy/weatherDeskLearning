using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;
using WeatherDeskk.Services;

namespace WeatherDeskk.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;
        private readonly ILocalizationService _localizationService;
        private readonly IThemeService _themeService;



    public ILocalizationService Localization => _localizationService;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string cityName = string.Empty;

        [ObservableProperty]
        private string temperature = string.Empty;

        [ObservableProperty]
        private string description = string.Empty;

        [ObservableProperty]
        private string humidity = string.Empty;

        [ObservableProperty]
        private string windSpeed = string.Empty;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        [ObservableProperty]
        private string statusColor = "Red";

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool isEnglishEnabled = false;

        [ObservableProperty]
        private bool isHindiEnabled = true;

        [ObservableProperty]
        private bool isLightThemeEnabled = false;

        [ObservableProperty]
        private bool isDarkThemeEnabled = true;

        public MainViewModel(
            IWeatherService weatherService,
            ILocalizationService localizationService,
            IThemeService themeService)
        {
            _weatherService = weatherService;
            _localizationService = localizationService;
            _themeService = themeService;
        }

        partial void OnSearchTextChanged(string value)
        {
            GetWeatherCommand.NotifyCanExecuteChanged();
        }

        partial void OnIsBusyChanged(bool value)
        {
            GetWeatherCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanGetWeather))]
        private async Task GetWeatherAsync()
        {
            try
            {
                IsBusy = true;
                StatusMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    StatusMessage = Localization["ErrorEmptyCity"];
                    StatusColor = "Red";
                    return;
                }

                var weather = await _weatherService.GetWeatherAsync(SearchText.Trim());

                if (weather == null)
                {
                    StatusMessage = Localization["CityNotFound"];
                    StatusColor = "Red";
                    return;
                }

                CityName = weather.CityName;
                Temperature = $"{weather.Temperature} °C";
                Description = weather.Description;
                Humidity = $"{weather.Humidity}%";
                WindSpeed = $"{weather.WindSpeed} km/h";

                StatusMessage = Localization["Success"];
                StatusColor = "Green";
            }
            catch
            {
                StatusMessage = Localization["ErrorGeneral"];
                StatusColor = "Red";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanGetWeather()
        {
            return !string.IsNullOrWhiteSpace(SearchText) && !IsBusy;
        }

        [RelayCommand]
        private void SwitchToEnglish()
        {
            _localizationService.ChangeLanguage("en");
            IsEnglishEnabled = false;
            IsHindiEnabled = true;
        }

        [RelayCommand]
        private void SwitchToHindi()
        {
            _localizationService.ChangeLanguage("hi");
            IsEnglishEnabled = true;
            IsHindiEnabled = false;
        }

        [RelayCommand]
        private void SetLightTheme()
        {
            _themeService.SetLightTheme();
            IsLightThemeEnabled = false;
            IsDarkThemeEnabled = true;
        }

        [RelayCommand]
        private void SetDarkTheme()
        {
            _themeService.SetDarkTheme();
            IsLightThemeEnabled = true;
            IsDarkThemeEnabled = false;
        }
    }
}