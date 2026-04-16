using System;
using System.Threading.Tasks;
using NSubstitute;
using WeatherDeskk.Models;
using WeatherDeskk.Services;
using WeatherDeskk.ViewModels;
using Xunit;

namespace WeatherDeskk.Tests.ViewModels
{
    public class MainViewModelTests
    {
        private readonly IWeatherService _weatherService;
        private readonly ILocalizationService _localizationService;
        private readonly IThemeService _themeService;
        private readonly MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _weatherService = Substitute.For<IWeatherService>();
            _localizationService = Substitute.For<ILocalizationService>();
            _themeService = Substitute.For<IThemeService>();

            _viewModel = new MainViewModel(
                _weatherService,
                _localizationService,
                _themeService);
        }

        [Fact]
        public void GetWeatherCommand_ShouldBeDisabled_WhenSearchTextIsEmpty()
        {
            _viewModel.SearchText = string.Empty;
            _viewModel.IsBusy = false;

            var canExecute = _viewModel.GetWeatherCommand.CanExecute(null);

            Assert.False(canExecute);
        }

        [Fact]
        public void GetWeatherCommand_ShouldBeEnabled_WhenSearchTextHasValue_AndNotBusy()
        {
            _viewModel.SearchText = "Delhi";
            _viewModel.IsBusy = false;

            var canExecute = _viewModel.GetWeatherCommand.CanExecute(null);

            Assert.True(canExecute);
        }

        [Fact]
        public void GetWeatherCommand_ShouldBeDisabled_WhenViewModelIsBusy()
        {
            _viewModel.SearchText = "Delhi";
            _viewModel.IsBusy = true;

            var canExecute = _viewModel.GetWeatherCommand.CanExecute(null);

            Assert.False(canExecute);
        }

        [Fact]
        public void SwitchToEnglishCommand_ShouldUpdateLanguageState()
        {
            _viewModel.SwitchToEnglishCommand.Execute(null);

            Assert.False(_viewModel.IsEnglishEnabled);
            Assert.True(_viewModel.IsHindiEnabled);

            _localizationService.Received(1).ChangeLanguage("en");
        }

        [Fact]
        public void SwitchToHindiCommand_ShouldUpdateLanguageState()
        {
            _viewModel.SwitchToHindiCommand.Execute(null);

            Assert.True(_viewModel.IsEnglishEnabled);
            Assert.False(_viewModel.IsHindiEnabled);

            _localizationService.Received(1).ChangeLanguage("hi");
        }

        [Fact]
        public void SetLightThemeCommand_ShouldUpdateThemeState()
        {
            _viewModel.SetLightThemeCommand.Execute(null);

            Assert.False(_viewModel.IsLightThemeEnabled);
            Assert.True(_viewModel.IsDarkThemeEnabled);

            _themeService.Received(1).SetLightTheme();
        }

        [Fact]
        public void SetDarkThemeCommand_ShouldUpdateThemeState()
        {
            _viewModel.SetDarkThemeCommand.Execute(null);

            Assert.True(_viewModel.IsLightThemeEnabled);
            Assert.False(_viewModel.IsDarkThemeEnabled);

            _themeService.Received(1).SetDarkTheme();
        }

        [Fact]
        public async Task GetWeatherCommand_ShouldUpdateWeatherDetails_WhenServiceReturnsValidData()
        {
            var weather = new WeatherInfo
            {
                CityName = "Delhi",
                Temperature = 30,
                Description = "Clear sky",
                Humidity = 60,
                WindSpeed = 12.5
            };

            _weatherService.GetWeatherAsync("Delhi").Returns(weather);
            _localizationService["Success"].Returns("Weather loaded successfully");

            _viewModel.SearchText = "Delhi";

            await _viewModel.GetWeatherCommand.ExecuteAsync(null);

            Assert.Equal("Delhi", _viewModel.CityName);
            Assert.Equal("30 °C", _viewModel.Temperature);
            Assert.Equal("Clear sky", _viewModel.Description);
            Assert.Equal("60%", _viewModel.Humidity);
            Assert.Equal("12.5 km/h", _viewModel.WindSpeed);
            Assert.Equal("Weather loaded successfully", _viewModel.StatusMessage);
            Assert.Equal("Green", _viewModel.StatusColor);
            Assert.False(_viewModel.IsBusy);
        }

        [Fact]
        public async Task GetWeatherCommand_ShouldShowCityNotFound_WhenServiceReturnsNull()
        {
            _weatherService.GetWeatherAsync("UnknownCity").Returns((WeatherInfo?)null);
            _localizationService["CityNotFound"].Returns("City not found");

            _viewModel.SearchText = "UnknownCity";

            await _viewModel.GetWeatherCommand.ExecuteAsync(null);

            Assert.Equal("City not found", _viewModel.StatusMessage);
            Assert.Equal("Red", _viewModel.StatusColor);
            Assert.False(_viewModel.IsBusy);
        }

      

        [Fact]
        public async Task GetWeatherCommand_ShouldShowGeneralError_WhenServiceThrowsException()
        {
            _weatherService
                .GetWeatherAsync("Delhi")
                .Returns<Task<WeatherInfo?>>(x => throw new Exception("API failed"));

            _localizationService["ErrorGeneral"].Returns("Something went wrong");

            _viewModel.SearchText = "Delhi";

            await _viewModel.GetWeatherCommand.ExecuteAsync(null);

            Assert.Equal("Something went wrong", _viewModel.StatusMessage);
            Assert.Equal("Red", _viewModel.StatusColor);
            Assert.False(_viewModel.IsBusy);
        }
    }
}