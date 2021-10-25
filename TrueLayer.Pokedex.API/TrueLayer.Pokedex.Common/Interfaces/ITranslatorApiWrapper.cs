namespace TrueLayer.Pokedex.Common.Interfaces
{
    public interface ITranslatorApiWrapper
    {
        string GetYodaTranslation(string textToBeTranslated);
        string GetShakespeareTranslation(string textToBeTranslated);
    }
}