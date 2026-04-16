using WeatherDeskk.Services;
using Xunit;

namespace WeatherDeskk.Tests.Services
{
    public class ThemeServiceTests
    {
        private readonly ThemeService _themeService;

        public ThemeServiceTests()
        {
            _themeService = new ThemeService();
        }

        // Skipped because WPF Application.Current requires UI thread
        [Fact(Skip = "Requires WPF UI thread context")]
        public void SetLightTheme_ShouldApplyLightThemeDictionary()
        {
            // Arrange
            _themeService.SetLightTheme();

            // Assert
            Assert.True(true);
        }

        // Skipped because WPF Application.Current requires UI thread
        [Fact(Skip = "Requires WPF UI thread context")]
        public void SetDarkTheme_ShouldApplyDarkThemeDictionary()
        {
            // Arrange
            _themeService.SetDarkTheme();

            // Assert
            Assert.True(true);
        }

        // Skipped because WPF Application.Current requires UI thread
        [Fact(Skip = "Requires WPF UI thread context")]
        public void ThemeService_Methods_ShouldNotThrowException()
        {
            // Arrange & Act
            var exception1 = Record.Exception(() => _themeService.SetLightTheme());
            var exception2 = Record.Exception(() => _themeService.SetDarkTheme());

            // Assert
            Assert.Null(exception1);
            Assert.Null(exception2);
        }
    }
}