using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using TrueLayer.Pokedex.Common.DTO.PokemonApi;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.PokeDex.DAL.ApiWrappers
{
    public class PokemonApiWrapper : IPokemonApiWrapper
    {
        private readonly IConfiguration config;

        public PokemonApiWrapper(IConfiguration config)
        {
            this.config = config;
        }

        public TrueLayer.Pokedex.Common.Domain.Model.Pokemon GetSinglePokemonSpeciesInformation(string pokemonName)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                throw new ArgumentException($"'{nameof(pokemonName)}' cannot be null or whitespace.", nameof(pokemonName));
            }

            var escapedString = Uri.EscapeDataString(pokemonName);

            var completeUri = $"{config.GetSection("PokemonEndPoints").GetSection("PokemonSpecies").Value}/{escapedString}";

            var request = (HttpWebRequest)WebRequest.Create(completeUri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var pokemonSpeciesInformationRoot = JsonSerializer.Deserialize<PokemonSpeciesInformationRoot>(jsonString, options);
                var englishDescription = pokemonSpeciesInformationRoot.Flavor_text_entries.FirstOrDefault(x => x.Language.Name == "en").Flavor_text;
                englishDescription = Regex.Replace(englishDescription, @"\f|\n|\r", " ");

                //now to create our own domain model from this information
                var pokemonDomainModel = new TrueLayer.Pokedex.Common.Domain.Model.Pokemon(pokemonSpeciesInformationRoot.Name, englishDescription, pokemonSpeciesInformationRoot.Habitat.Name, pokemonSpeciesInformationRoot.Is_legendary);
                return pokemonDomainModel;
            }
        }

    }
}
