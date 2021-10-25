using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TrueLayer.Pokedex.API;
using TrueLayer.Pokedex.API.Controllers;
using TrueLayer.Pokedex.API.ViewModels;
using TrueLayer.Pokedex.Common.Domain.Model;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.Pokedex.Tests.UnitTests
{
    public class PokemonControllerUnitTests
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
        }

        [Test]
        public void GivenNameIsEmptyString_WhenGetCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var pokemonDomainModel = pokemonController.Get("");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsNull_WhenGetCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var pokemonDomainModel = pokemonController.Get(null);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsWhiteSpace_WhenGetCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act 
            var pokemonDomainModel = pokemonController.Get("");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsEmptyString_WhenGetTranslatedCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act 
            var pokemonDomainModel = pokemonController.Get("");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsNull_WhenGetTranslatedCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var pokemonDomainModel = pokemonController.GetTranslated(null);

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsWhiteSpace_WhenGetTranslationCalled_BadRequestReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var pokemonDomainModel = pokemonController.GetTranslated("");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameIsValid_WhenGetTranslationCalled_ThenTranslatedPokemonWithTextReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            mockPokemonDomainService.Setup(x => x.GetTranslationOfDescription(It.IsAny<Pokemon>())).Returns("This is translated text");
            var domainPokemon = new Pokemon("test pokemon", "Initial description", "cave", true);
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            mockPokemonApiWrapper.Setup(x => x.GetSinglePokemonSpeciesInformation(It.IsAny<string>())).Returns(domainPokemon);
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var returnedActionResult = pokemonController.GetTranslated("test pokemon");

            //Assert
            var okResult = returnedActionResult as OkObjectResult;
            var pokemonDomainModel = okResult.Value as PokemonViewModel;
            Assert.AreEqual("This is translated text", pokemonDomainModel.Description);
        }


        [Test]
        public void GivenNameIsValid_WhenGetCalled_ThenPokemonReturned()
        {
            //Arrange
            var mockPokemonDomainService = new Mock<IPokemonService>();
            var domainPokemon = new Pokemon("test pokemon", "Initial description", "cave", true);
            var mockPokemonApiWrapper = new Mock<IPokemonApiWrapper>();
            mockPokemonApiWrapper.Setup(x => x.GetSinglePokemonSpeciesInformation(It.IsAny<string>())).Returns(domainPokemon);
            var mockLogger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(mockPokemonApiWrapper.Object, mockPokemonDomainService.Object, mockLogger.Object, mapper);

            //Act
            var returnedActionResult = pokemonController.Get("test pokemon");

            //Assert
            var okResult = returnedActionResult as OkObjectResult;
            var pokemonDomainModel = okResult.Value as PokemonViewModel;
            Assert.AreEqual("test pokemon", pokemonDomainModel.Name);
        }
    }
}
