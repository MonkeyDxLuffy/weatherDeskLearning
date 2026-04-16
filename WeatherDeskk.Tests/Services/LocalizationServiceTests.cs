using System.ComponentModel;
using System.Threading;
using WeatherDeskk.Services;
using Xunit;

namespace WeatherDeskk.Tests.Services
{
    public class LocalizationServiceTests
    {
        [Fact]
        public void ChangeLanguage_ShouldUpdateCurrentUICulture_ToEnglish()
        {
            var service = new LocalizationService();

            service.ChangeLanguage("en");

            Assert.Equal("en", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }

        [Fact]
        public void ChangeLanguage_ShouldUpdateCurrentUICulture_ToHindi()
        {
            var service = new LocalizationService();

            service.ChangeLanguage("hi");

            Assert.Equal("hi", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName);
        }

        [Fact]
        public void ChangeLanguage_ShouldRaisePropertyChangedEvent()
        {
            var service = new LocalizationService();
            PropertyChangedEventArgs? receivedArgs = null;

            service.PropertyChanged += (_, e) => receivedArgs = e;

            service.ChangeLanguage("en");

            Assert.NotNull(receivedArgs);
            Assert.Null(receivedArgs!.PropertyName);
        }

        [Fact]
        public void Indexer_ShouldReturnKey_WhenResourceDoesNotExist()
        {
            var service = new LocalizationService();

            var value = service["SomeMissingResourceKey"];

            Assert.Equal("SomeMissingResourceKey", value);
        }

        [Fact]
        public void Indexer_ShouldReturnValue_ForExistingResourceKey()
        {
            var service = new LocalizationService();

            var value = service["Success"];

            Assert.False(string.IsNullOrWhiteSpace(value));
        }
    }
}