using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TrueLayer.Pokedex.API;
using TrueLayer.Pokedex.API.Controllers;
using TrueLayer.Pokedex.API.ViewModels;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.IntegrationTests
{
    public class PokemonControllerIntegrationTests
    {

        private IConfiguration config;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            config = ConfigBuilderHelper.InitConfiguration();
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

        }

        [Test]
        public void GivenInputVariablesAreValid_WhenTranslationCalled_ObjectWithTranslationReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(config);
            var logger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiWrapper, pokemonDomainService, logger.Object, mapper);

            //Act
            var actionResult = pokemonController.GetTranslated("onix");

            //Assert
            var okResult = actionResult as OkObjectResult;
            var viewModel = okResult.Value as PokemonViewModel;
            Assert.AreEqual("onix", viewModel.Name);
        }


        [Test]
        public void GivenNameInputInvalid_WhenGetTranslationCalled_NotFoundReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(config);
            var logger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiWrapper, pokemonDomainService, logger.Object, mapper);

            //Act 
            var pokemonDomainModel = pokemonController.GetTranslated("frank");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameInputInvalid_WhenGetCalled_NotFoundReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(config);
            var logger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiWrapper, pokemonDomainService, logger.Object, mapper);

            //Act 
            var pokemonDomainModel = pokemonController.Get("frank");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

    }
}
