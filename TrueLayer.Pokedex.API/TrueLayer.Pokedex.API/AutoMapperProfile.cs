using AutoMapper;
using TrueLayer.Pokedex.API.ViewModels;
using TrueLayer.Pokedex.Common.Domain.Model;

namespace TrueLayer.Pokedex.API
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Pokemon, PokemonViewModel>();
            CreateMap<PokemonViewModel, Pokemon>();
        }
    }
}
