using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using TrueLayer.Pokedex.API.ViewModels;
using TrueLayer.Pokedex.Common.Domain.Services;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.Pokedex.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        readonly IPokemonApiWrapper pokeApiRepository;
        readonly IPokemonService pokemonService;
        readonly ILogger<PokemonController> logger;
        private readonly IMapper mapper;

        public PokemonController(IPokemonApiWrapper pokeApiRepository, IPokemonService pokemonService, ILogger<PokemonController> logger, IMapper mapper)
        {
            this.pokeApiRepository = pokeApiRepository;
            this.pokemonService = pokemonService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet("{name}")]
        public ActionResult Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var message = $"Pokemon name must present and not whitespace. Name submitted: {name}";
                logger.LogError(message);
                return BadRequest(message);
            }

            try
            {
                var pokemon = pokeApiRepository.GetSinglePokemonSpeciesInformation(name);
                PokemonViewModel pokemonViewModel = mapper.Map<PokemonViewModel>(pokemon);
                return Ok(pokemonViewModel);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response == null)
                    {
                        var message = $"HttpWebResponse from WebException in PokemonController.Get is null trying to get Pokemon: {name}";
                        logger.LogError(ex, message);
                        return StatusCode(500);
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        var message = $"Pokemon {name} as for in PokemonController.Get cannot be found.";
                        logger.LogInformation(ex, message);
                        return NotFound($"The asked for Pokemon {name} cannot be found");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var message = $"A bad request was submitted trying to get Pokemon: {name}";
                        logger.LogError(ex, message);
                        return BadRequest(message);
                    }
                    else
                    {
                        var message = $"WebException with {response.StatusCode} encountered in  PokemonController.Get for requested: {name}";
                        logger.LogError(ex, message);
                        return StatusCode(500);
                    }
                }
                else
                {
                    var message = $"WebException with encountered in PokemonController.Get for requested: {name}";
                    logger.LogError(ex, message);
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                var message = $"An error ocurred trying to get Pokemon: {name}";
                logger.LogError(ex, message);
                return StatusCode(500);
            }
        }


        [HttpGet("translated/{name}")]
        public ActionResult GetTranslated(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var message = $"Pokemon name must present and not whitespace. Name submitted: {name}";
                logger.LogError(message);
                return BadRequest(message);
            }

            try
            {
                var pokemon = pokeApiRepository.GetSinglePokemonSpeciesInformation(name);
                var translatedDescription = pokemonService.GetTranslationOfDescription(pokemon);
                PokemonViewModel pokemonViewModel = mapper.Map<PokemonViewModel>(pokemon);
                pokemonViewModel.Description = translatedDescription;
                return Ok(pokemonViewModel);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response == null)
                    {
                        var message = $"HttpWebResponse from WebException in PokemonController.GetTranslated is null trying to get a fun translated record for Pokemon: {name}";
                        logger.LogError(ex, message);
                        return StatusCode(500);
                    }
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        var message = $"Pokemon {name} as for in PokemonController.GetTranslated cannot be found.";
                        logger.LogInformation(ex, message);
                        return NotFound($"The asked for Pokemon {name} cannot be found");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var message = $"A bad request was submitted trying to get a fun translated record for Pokemon: {name}";
                        logger.LogError(ex, message);
                        return BadRequest(message);
                    }
                    else
                    {
                        var message = $"WebException with {response.StatusCode} encountered in  PokemonController.GetTranslated for requested: {name}";
                        logger.LogError(ex, message);
                        return StatusCode(500);
                    }
                }
                else
                {
                    var message = $"WebException with encountered in PokemonController.GetTranslated for requested: {name}";
                    logger.LogError(ex, message);
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                var message = $"An error ocurred trying to get a fun translated record for Pokemon {name}";
                logger.LogError(ex, message);
                return StatusCode(500);
            }
        }
    }
}
