using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using TrueLayer.Pokedex.Common.DTO.TranslatorApi;
using TrueLayer.Pokedex.Common.Interfaces;

namespace TrueLayer.PokeDex.DAL.ApiWrappers
{
    public class TranslatorApiWrapper : ITranslatorApiWrapper
    {
        private readonly IConfiguration config;
        public TranslatorApiWrapper(IConfiguration config)
        {
            this.config = config;
        }

        public string GetShakespeareTranslation(string textToBeTranslated)
        {
            return GetTranslation(textToBeTranslated, config.GetSection("TranslatorEndPoints").GetSection("ShakespearTranslator").Value);
        }

        public string GetYodaTranslation(string textToBeTranslated)
        {
            return GetTranslation(textToBeTranslated, config.GetSection("TranslatorEndPoints").GetSection("YodaTranslator").Value);
        }


        private string GetTranslation(string textToBeTranslated, string translatorLocation)
        {
            if (string.IsNullOrWhiteSpace(textToBeTranslated))
            {
                throw new ArgumentException($"'{nameof(textToBeTranslated)}' cannot be null or whitespace.", nameof(textToBeTranslated));
            }

            if (string.IsNullOrWhiteSpace(translatorLocation))
            {
                throw new ArgumentException($"'{nameof(translatorLocation)}' cannot be null or whitespace.", nameof(translatorLocation));
            }

            textToBeTranslated = Regex.Replace(textToBeTranslated, @"\f|\n|\r", " ");
            textToBeTranslated = Uri.EscapeDataString(textToBeTranslated);

            var completeUri = $"{translatorLocation}?text={textToBeTranslated}";

            var request = (HttpWebRequest)WebRequest.Create(completeUri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var translatorResponse = JsonSerializer.Deserialize<TranslatorResponse>(jsonString, options);

                return translatorResponse.Contents.Translated;
            }
        }
    }
}
