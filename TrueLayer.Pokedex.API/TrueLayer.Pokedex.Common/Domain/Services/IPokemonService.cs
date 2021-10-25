using TrueLayer.Pokedex.Common.Domain.Model;

namespace TrueLayer.Pokedex.Common.Domain.Services
{
    public interface IPokemonService
    {
        string GetTranslationOfDescription(Pokemon pokemon);
    }
}