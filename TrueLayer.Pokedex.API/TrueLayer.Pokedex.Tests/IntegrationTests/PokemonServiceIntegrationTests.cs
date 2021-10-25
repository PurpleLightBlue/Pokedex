using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TrueLayer.Pokedex.Common.Domain.Model;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.IntegrationTests
{
    public class PokemonServiceIntegrationTests
    {

        private IConfiguration config;

        [SetUp]
        public void Setup()
        {
            config = ConfigBuilderHelper.InitConfiguration();
        }


        [Test]
        public void GivenAPokemonWithHabitatOfCave_WhenGetTranslationCalled_ThenYodaTranslationReturned()
        {
            //Arrange
            var translatorApi = new TranslatorApiWrapper(config);
            var mockPokemonDomainService = new PokemonService(translatorApi);
            var pokemon = new Pokemon("onix", "As it grows, the stone portions of its body harden to become similar to a diamond, but colored black.", "cave", false);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert
            Assert.AreEqual("As it grows,To become similar to a diamond,  the stone portions of its body harden,But colored black.", returnedTranslation);
        }

        [Test]
        public void GivenAPokemonWithHabitatOfSeaButIsLegnedary_WhenGetTranslationCalled_ThenYodaTranslationReturned()
        {
            //Arrange
            var translatorApi = new TranslatorApiWrapper(config);
            var mockPokemonDomainService = new PokemonService(translatorApi);
            var pokemon = new Pokemon("onix", "As it grows, the stone portions of its body harden to become similar to a diamond, but colored black.", "sea", true);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert
            Assert.AreEqual("As it grows,To become similar to a diamond,  the stone portions of its body harden,But colored black.", returnedTranslation);
        }

        [Test]
        public void GivenAPokemonWithHabitatOfBeachSnfNotLegendary_WhenGetTranslationCalled_ThenShakespeareTranslationReturned()
        {
            //Arrange
            var translatorApi = new TranslatorApiWrapper(config);
            var mockPokemonDomainService = new PokemonService(translatorApi);
            var pokemon = new Pokemon("onix", "As it grows, the stone portions of its body harden to become similar to a diamond, but colored black.", "beach", false);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert
            Assert.AreEqual("As 't grows,  the stone portions of its corse harden to becometh similar to a diamond,  but colored black.", returnedTranslation);
        }
    }
}
