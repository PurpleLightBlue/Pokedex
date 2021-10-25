using Microsoft.Extensions.Configuration;

namespace TrueLayer.Pokedex.Tests.Utilities
{
    static class ConfigBuilderHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}
