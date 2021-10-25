using System.Collections.Generic;

namespace TrueLayer.Pokedex.Common.DTO.PokemonApi
{
    public class PokemonSpeciesInformationRoot
    {
        public int Base_happiness { get; set; }
        public int Capture_rate { get; set; }
        public Color Color { get; set; }
        public List<EggGroup> Egg_groups { get; set; }
        public EvolutionChain Evolution_chain { get; set; }
        public EvolvesFromSpecies Evolves_from_species { get; set; }
        public List<FlavorTextEntry> Flavor_text_entries { get; set; }
        public List<object> Form_descriptions { get; set; }
        public bool Forms_switchable { get; set; }
        public int Gender_rate { get; set; }
        public List<Genera> Genera { get; set; }
        public Generation Generation { get; set; }
        public GrowthRate Growth_rate { get; set; }
        public Habitat Habitat { get; set; }
        public bool Has_gender_differences { get; set; }
        public int Hatch_counter { get; set; }
        public int Id { get; set; }
        public bool Is_baby { get; set; }
        public bool Is_legendary { get; set; }
        public bool Is_mythical { get; set; }
        public string Name { get; set; }
        public List<Name> Names { get; set; }
        public int Order { get; set; }
        public List<PalParkEncounter> Pal_park_encounters { get; set; }
        public List<PokedexNumber> Pokedex_numbers { get; set; }
        public Shape Shape { get; set; }
        public List<Variety> Varieties { get; set; }
    }
}
