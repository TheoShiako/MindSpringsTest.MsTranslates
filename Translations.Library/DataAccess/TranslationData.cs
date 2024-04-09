namespace Translations.Library.DataAccess;

public class TranslationData : ITranslationData
{
    private readonly ISqlServerConnection sqlServerConnection;

    public TranslationData(ISqlServerConnection sqlServerConnection)
    {
        this.sqlServerConnection = sqlServerConnection;
    }

    //Save translation to sql server
    public async Task Insert(TranslationModel model)
       => await sqlServerConnection.SendData("spTranslations_Insert", new { model.SearchText, model.TranslatedText, model.Status, model.Error, TranslatorId = model.Translator.Id });

    //Get the stored translations from sql server database
    public async Task<IEnumerable<TranslationModel>> Get()
    {
        //Dapper complex object mapping used to map data retrieved from query.
        using var conn = sqlServerConnection.Connection();
        var translations = await conn.QueryAsync<TranslationModel, TranslatorModel, TranslationModel>("spTranslations_Get", (translation, translator) =>
        {
            translation.Translator = translator;
            return translation;
        });

        return translations;
    }
}
