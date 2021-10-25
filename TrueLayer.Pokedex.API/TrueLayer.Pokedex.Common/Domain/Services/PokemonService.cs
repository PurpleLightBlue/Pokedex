using System;
using TrueLayer.Pokedex.Common.Domain.Model;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.Pokedex.Common.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        readonly ITranslatorApiWrapper translatorApiWrapper;
        public PokemonService(ITranslatorApiWrapper translatorApiWrapper)
        {
            this.translatorApiWrapper = translatorApiWrapper ?? throw new System.ArgumentNullException(nameof(translatorApiWrapper));
        }
        public string GetTranslationOfDescription(Pokemon pokemon)
        {
            if (pokemon is null)
            {
                throw new ArgumentNullException(nameof(pokemon));
            }

            if (pokemon.Habitat == "cave" || pokemon.IsLegendary)
            {
                return translatorApiWrapper.GetYodaTranslation(pokemon.Description);
            }
            else
            {
                return translatorApiWrapper.GetShakespeareTranslation(pokemon.Description);
            }
        }

    }
}
