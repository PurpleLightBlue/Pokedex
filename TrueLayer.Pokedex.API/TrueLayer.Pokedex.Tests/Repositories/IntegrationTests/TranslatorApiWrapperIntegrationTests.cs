using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.Repositories.IntegrationTests
{
    public class TranslatorApiWrapperIntegrationTests
    {
        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            this.config = ConfigBuilderHelper.InitConfiguration();
        }

        [Test]
        public void GivenInputVariablesAreValid_WhenYodaRepoGetTranslationCalled_TranslationJSONReturned()
        {
            //Arrange
            var repo = new TranslatorApiWrapper(this.config);
            //Act
            var translatedJson = repo.GetYodaTranslation("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.");
            //Assert
            Assert.IsNotEmpty(translatedJson);
        }

        [Test]
        public void GivenInputVariablesAreValid_WhenShakespeareRepoGetTranslationCalled_TranslationJSONReturned()
        {
            //Arrange
            var repo = new TranslatorApiWrapper(this.config);
            //Act
            var translatedJson = repo.GetShakespeareTranslation("It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.");
            //Assert
            Assert.IsNotEmpty(translatedJson);
        }
    }
}
