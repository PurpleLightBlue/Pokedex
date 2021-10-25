namespace TrueLayer.Pokedex.Common.Interfaces
{
    public interface IPokemonApiWrapper
    {
        Domain.Model.Pokemon GetSinglePokemonSpeciesInformation(string pokemonName);
    }
}