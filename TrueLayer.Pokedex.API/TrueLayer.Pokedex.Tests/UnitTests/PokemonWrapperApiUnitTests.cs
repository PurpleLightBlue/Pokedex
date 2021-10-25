using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.UnitTests
{
    public class PokemonWrapperApiUnitTests
    {

        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            config = ConfigBuilderHelper.InitConfiguration();
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenEmptyString_WhenYodaRepoGetTranslationCalled_ExceptionThrown(string name)
        {
            //Arrange
            var repo = new PokemonApiWrapper(config);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => repo.GetSinglePokemonSpeciesInformation(name));
        }
    }
}
