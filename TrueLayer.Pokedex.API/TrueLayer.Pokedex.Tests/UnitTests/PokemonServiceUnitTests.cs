using Moq;
using NUnit.Framework;
using System;
using TrueLayer.Pokedex.Common.Domain.Model;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.Pokedex.Tests.Repositories.UnitTests
{
    public class PokemonServiceUnitTests
    {
        [Test]
        public void GivenAPokemonWithHabitatOfCave_WhenGetTranslationCalled_ThenYodaTranslationReturned()
        {
            //Arrange
            var mockTranslaionApiWrapper = new Mock<ITranslatorApiWrapper>();
            mockTranslaionApiWrapper.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>())).Returns("To be or not to be.");
            mockTranslaionApiWrapper.Setup(x => x.GetYodaTranslation(It.IsAny<string>())).Returns("Strong with the force, is this one.");
            var mockPokemonDomainService = new PokemonService(mockTranslaionApiWrapper.Object);
            var pokemon = new Pokemon("onix", "As it grows, the\nstone portions of\nits body harden\fto become similar\nto a diamond, but\ncolored black.", "cave", false);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert

            Assert.AreEqual("Strong with the force, is this one.", returnedTranslation);
        }

        [Test]
        public void GivenAPokemonWithHabitatOfBeachButIsLegendary_WhenGetTranslationCalled_ThenYodaTranslationReturned()
        {
            //Arrange
            var mockTranslaionApiWrapper = new Mock<ITranslatorApiWrapper>();
            mockTranslaionApiWrapper.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>())).Returns("To be or not to be.");
            mockTranslaionApiWrapper.Setup(x => x.GetYodaTranslation(It.IsAny<string>())).Returns("Strong with the force, is this one.");
            var mockPokemonDomainService = new PokemonService(mockTranslaionApiWrapper.Object);
            var pokemon = new Pokemon("Madeup", "As it grows, the\nstone portions of\nits body harden\fto become similar\nto a diamond, but\ncolored black.", "beach", true);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert
            Assert.AreEqual("Strong with the force, is this one.", returnedTranslation);
        }


        [Test]
        public void GivenAPokemonWithHabitatOfBeachAndIsNotLegendary_WhenGetTranslationCalled_ThenShakespeareTranslationReturned()
        {
            //Arrange
            var mockTranslaionApiWrapper = new Mock<ITranslatorApiWrapper>();
            mockTranslaionApiWrapper.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>())).Returns("To be or not to be.");
            mockTranslaionApiWrapper.Setup(x => x.GetYodaTranslation(It.IsAny<string>())).Returns("Strong with the force, is this one.");
            var mockPokemonDomainService = new PokemonService(mockTranslaionApiWrapper.Object);
            var pokemon = new Pokemon("Madeup", "As it grows, the\nstone portions of\nits body harden\fto become similar\nto a diamond, but\ncolored black.", "beach", false);

            //Act
            var returnedTranslation = mockPokemonDomainService.GetTranslationOfDescription(pokemon);

            //Assert
            Assert.AreEqual("To be or not to be.", returnedTranslation);
        }


        [Test]
        public void GivenNullApiWrapper_WhenServiceCreated_ThenExceptionThrown()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PokemonService(null));
        }

        [Test]
        public void GivenANullPokemon_WhenGetTranslationCalled_ThenExceptionThrown()
        {
            //Arrange
            var mockTranslaionApiWrapper = new Mock<ITranslatorApiWrapper>();
            mockTranslaionApiWrapper.Setup(x => x.GetShakespeareTranslation(It.IsAny<string>())).Returns("To be or not to be.");
            mockTranslaionApiWrapper.Setup(x => x.GetYodaTranslation(It.IsAny<string>())).Returns("Strong with the force, is this one.");
            var mockPokemonDomainService = new PokemonService(mockTranslaionApiWrapper.Object);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => mockPokemonDomainService.GetTranslationOfDescription(null));
        }
    }
}
