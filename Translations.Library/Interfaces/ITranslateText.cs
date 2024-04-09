namespace Translations.Library.Interfaces;

public interface ITranslateText
{
    /// <summary>
    /// Takes a translation model containing the text to be translated and
    /// the translator to be used, gets the translation and the returns
    /// translation model with updated properties i.e. the translated text if
    /// translation was succesful or the error message if unsuccesful
    /// </summary>
    /// <param name="translation"></param>
    /// <returns></returns>
    Task<TranslationModel> Translate(TranslationModel translation);
}