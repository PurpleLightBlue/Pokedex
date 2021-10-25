using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.Repositories.UnitTests
{
    public class PokemonWrapperApiUnitTests
    {

        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            this.config = ConfigBuilderHelper.InitConfiguration();
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenEmptyString_WhenYodaRepoGetTranslationCalled_ExceptionThrown(string name)
        {
            //Arrange
            var repo = new PokemonApiWrapper(this.config);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => repo.GetSinglePokemonSpeciesInformation(name));
        }
    }
}
