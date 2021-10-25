using NUnit.Framework;
using System;
using TrueLayer.Pokedex.Common.Domain.Model;

namespace TrueLayer.Pokedex.Tests.UnitTests
{
    public class PokemonDomainUnitTests
    {
        [Test]
        public void GivenValidInputs_WhenDomainModelCreated_ThenOjectReturned()
        {
            //Arrange & Act
            var pokemon = new Pokemon("pikatu", "The best one", "fields", true);
            //Assert
            Assert.AreEqual("pikatu", pokemon.Name);
            Assert.AreEqual("The best one", pokemon.Description);
            Assert.AreEqual("fields", pokemon.Habitat);
            Assert.AreEqual(true, pokemon.IsLegendary);
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenNameIsInvalid_WhenDomainModelCreated_ThenExceptionThrown(string nameValue)
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Pokemon(nameValue, "The best one", "fields", true));
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenDescriptionIsInvalid_WhenDomainModelCreated_ThenExceptionThrown(string descriptionValue)
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Pokemon("Pikatu", descriptionValue, "fields", true));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenHabitatIsInvalid_WhenDomainModelCreated_ThenExceptionThrown(string habitatValue)
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Pokemon("Pikatu", "The best one", habitatValue, true));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenHabitaUpdatetIsInvalid_WhenUpdateHabitatCalled_ThenExceptionThrown(string habitatValue)
        {
            //Arrange
            var pokemon = new Pokemon("Pikatu", "The best one", "Fields", true);
            //Act & Assert
            Assert.Throws<ArgumentException>(() => pokemon.UpdateHabitat(habitatValue));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenDescriptionUpdatetIsInvalid_WhenUpdateDescriptionCalled_ThenExceptionThrown(string descriptionValue)
        {
            //Arrange
            var pokemon = new Pokemon("Pikatu", "The best one", "Fields", true);
            //Act & Assert
            Assert.Throws<ArgumentException>(() => pokemon.UpdateDescription(descriptionValue));
        }

        [Test]
        public void GivenHabitatUpdatetIsValid_WheUpdateHabitatCalled_ThenUpdateIsReturned()
        {
            //Arrange
            var pokemon = new Pokemon("Pikatu", "The best one", "Fields", true);
            //Act
            pokemon.UpdateHabitat("Inner city");
            //Assert
            Assert.AreEqual("Inner city", pokemon.Habitat);
        }

        [Test]
        public void GivenDescriptionUpdatetIsValid_WheUpdateDescriptionCalled_ThenUpdateIsReturned()
        {
            //Arrange
            var pokemon = new Pokemon("Pikatu", "The best one", "Fields", true);
            //Act
            pokemon.UpdateDescription("Yellow and cool");
            //Assert
            Assert.AreEqual("Yellow and cool", pokemon.Description);
        }

        [Test]
        public void GivenIsLegendaryUpdateIsValid_WheUpdateLegendaryStatusCalled_ThenUpdateIsReturned()
        {
            //Arrange
            var pokemon = new Pokemon("Pikatu", "The best one", "Fields", true);
            //Act
            pokemon.UpdateLegendaryStatus(false);
            //Assert
            Assert.AreEqual(false, pokemon.IsLegendary);
        }
    }
}
