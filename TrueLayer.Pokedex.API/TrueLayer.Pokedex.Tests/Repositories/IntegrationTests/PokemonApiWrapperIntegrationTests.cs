using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.Repositories.IntegrationTests
{
    public class PokemonApiWrapperIntegrationTests
    {
        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            this.config = ConfigBuilderHelper.InitConfiguration();
        }


        [Test]
        public void GivenValidPokemonName_WhenGetSinglePokemonSpeciesInformationCalled_ThenPokemonReturned()
        {
            //Arrange
            var pokemonWrapperApi = new PokemonApiWrapper(this.config);
            var pokemonName = "pikachu";
            //Act 
            var pokemonDomainModel = pokemonWrapperApi.GetSinglePokemonSpeciesInformation(pokemonName);
            //Assert
            Assert.AreEqual("pikachu", pokemonDomainModel.Name);
        }

        [Test]
        public void GivenInValidPokemonName_WhenGetSinglePokemonSpeciesInformationCalled_ThenExceptionThrown()
        {
            //Arrange
            var pokemonWrapperApi = new PokemonApiWrapper(this.config);
            var pokemonName = "Frank";

            //Act & Assert
            Assert.Throws<WebException>(() => pokemonWrapperApi.GetSinglePokemonSpeciesInformation(pokemonName));

        }
    }
}
