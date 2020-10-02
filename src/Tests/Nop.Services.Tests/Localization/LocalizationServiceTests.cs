using System.Diagnostics;
using System.IO;
using System.Linq;
using FluentAssertions;
using Nop.Core.Domain.Localization;
using Nop.Services.Localization;
using NUnit.Framework;

namespace Nop.Services.Tests.Localization
{
    [TestFixture]
    public class LocalizationServiceTests : ServiceTest
    {
        private ILocalizationService _localizationService;
        private string _xmlData;

        [OneTimeSetUp]
        public void SetUp()
        {
            _localizationService = GetService<ILocalizationService>();
            _xmlData =  "<?xml version=\"1.0\" encoding=\"utf - 16\"?>" +
                        "<Language Name=\"English\">" +
                        "<LocaleResource Name = \"Temp.Plugins.Temp\">" +
                        "<Value>Temp</Value>" +
                        "</LocaleResource>" +
                        "</Language>";
        }

        [Test]
        public void ShouldHaveResourcesWhenLanguageMatches()
        {
            var language = new Language { Name = "English" };
            var resources = _localizationService.LoadLocaleResourcesFromStream(new StringReader(_xmlData), language.Name);

            resources.Should().HaveCount(1);
        }

        [Test]
        public void ShouldNotHaveResourcesWhenLanguageDoesNotMatch()
        {
            var language = new Language { Name = "Russian" };
            var resources = _localizationService.LoadLocaleResourcesFromStream(new StringReader(_xmlData), language.Name);

            resources.Should().HaveCount(0);
        }
    }
}
