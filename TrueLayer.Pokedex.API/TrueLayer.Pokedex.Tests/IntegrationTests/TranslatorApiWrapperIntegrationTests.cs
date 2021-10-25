using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.IntegrationTests
{
    public class TranslatorApiWrapperIntegrationTests
    {
        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            config = ConfigBuilderHelper.InitConfiguration();
        }

        [Test]
        public void GivenInputVariablesAreValid_WhenGetYodaTranslationCalled_TranslationJSONReturned()
        {
            //Arrange
            var repo = new TranslatorApiWrapper(config);
            //Act
            var translatedJson = repo.GetYodaTranslation("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.");
            //Assert
            Assert.IsNotEmpty(translatedJson);
        }

        [Test]
        public void GivenInputVariablesAreValid_WhenGetShakespeareTranslationCalled_TranslationJSONReturned()
        {
            //Arrange
            var repo = new TranslatorApiWrapper(config);
            //Act
            var translatedJson = repo.GetShakespeareTranslation("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.");
            //Assert
            Assert.IsNotEmpty(translatedJson);
        }
    }
}
