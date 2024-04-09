namespace Translations.Library.Interfaces;

public interface ITranslationData
{
    /// <summary>
    /// Retrieve all saved translations from database
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<TranslationModel>> Get();

    /// <summary>
    /// Save translation model to the database
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Insert(TranslationModel model);
}