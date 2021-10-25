using TrueLayer.Pokedex.Common.DTO.PokemonApi;

namespace TrueLayer.Pokedex.Common.Interfaces
{
    public interface IPokemonApiWrapper
    {
        TrueLayer.Pokedex.Common.Domain.Model.Pokemon GetSinglePokemonSpeciesInformation(string pokemonName);
    }
}