using System;

namespace TrueLayer.Pokedex.Common.Domain.Model
{
    public class Pokemon
    {
        public Pokemon(string name, string description, string habitiat, bool isLegendary)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"'{nameof(description)}' cannot be null or empty.", nameof(description));
            }

            if (string.IsNullOrWhiteSpace(habitiat))
            {
                throw new ArgumentException($"'{nameof(habitiat)}' cannot be null or empty.", nameof(habitiat));
            }

            Name = name;
            Description = description;
            Habitat = habitiat;
            IsLegendary = isLegendary;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"'{nameof(description)}' cannot be updated to be null or empty.", nameof(description));
            }

            this.Description = description;
        }

        public void UpdateHabitat(string habitat)
        {
            if (string.IsNullOrWhiteSpace(habitat))
            {
                throw new ArgumentException($"'{nameof(habitat)}' cannot be updated to be null or empty.", nameof(habitat));
            }

            this.Habitat = habitat;
        }

        public void UpdateLegendaryStatus(bool isLegendary)
        {
            this.IsLegendary = isLegendary;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Habitat { get; private set; }
        public bool IsLegendary { get; private set; }


    }
}
