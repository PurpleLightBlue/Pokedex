using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TrueLayer.Pokedex.API;
using TrueLayer.Pokedex.API.Controllers;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.Repositories.IntegrationTests
{
    public class PokemonControllerIntegrationTests
    {

        private IConfiguration config;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.config = ConfigBuilderHelper.InitConfiguration();
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            this.mapper = new Mapper(configuration);

        }

        [Test]
        public void GivenInputVariablesAreValid_WhenTranslationCalled_ObjectWithTranslationReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(this.config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(this.config);
            var logger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiWrapper,pokemonDomainService, logger.Object, mapper);

            //Act
            var pokemonDomainModel = pokemonController.GetTranslated("onix");

            //Assert
            Assert.IsNotEmpty(pokemonDomainModel.ToString());
        }


        [Test]
        public void GivenNameInputInvalud_WhenTranslationCalled_NotFoundReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(this.config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(this.config);
            var logger = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiWrapper, pokemonDomainService, logger.Object, mapper);

            //Act 
            var pokemonDomainModel = pokemonController.GetTranslated("frank");

            //Assert
            var statusCodeResult = (IStatusCodeActionResult)pokemonDomainModel;
            Assert.AreEqual(404, statusCodeResult.StatusCode);
        }

        [Test]
        public void GivenNameInputInvalud_WhenGetCalled_NotFoundReturned()
        {
            //Arrange
            var translatorApiWrapper = new TranslatorApiWrapper(this.config);
            var pokemonDomainService = new PokemonService(translatorApiWrapper);
            var pokemonApiWrapper = new PokemonApiWrapper(this.config);
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
