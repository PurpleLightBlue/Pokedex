﻿using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using TrueLayer.Pokedex.Tests.Utilities;
using TrueLayer.PokeDex.DAL.ApiWrappers;

namespace TrueLayer.Pokedex.Tests.Repositories.UnitTests
{
    public class TranslatorWrapperApiUnitTests
    {
        private IConfiguration config;
        [SetUp]
        public void Setup()
        {
            this.config = ConfigBuilderHelper.InitConfiguration();
        }


        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenDescriptionIsInvalid_WhenGetYodaTranslationCalled_ExceptionThrown(string description)
        {
            var repo = new TranslatorApiWrapper(this.config);

            Assert.Throws<ArgumentException>(() => repo.GetYodaTranslation(description));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenDescriptionIsInvalid_WhenGetShakespeareTranslationCalled_ExceptionThrown(string description)
        {
            //Arrange
            var repo = new TranslatorApiWrapper(this.config);

            //Act & Assert
            Assert.Throws<ArgumentException>(() => repo.GetShakespeareTranslation(description));
        }
    }
}
