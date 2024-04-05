namespace Translations.Library.DataAccess;
public class TranslatorsData : ITranslatorsData
{
    private readonly ISqlServerConnection sqlServerConnection;

    public TranslatorsData(ISqlServerConnection sqlServerConnection)
    {
        this.sqlServerConnection = sqlServerConnection;
    }

    public async Task Insert(TranslatorModel model)
        => await sqlServerConnection.SendData("spTranslators_Insert", new { model.Name, model.BaseUrl, model.ApiKey });

    public async Task<IEnumerable<TranslatorModel>> Get(int? Id = null)
        => await sqlServerConnection.GetData<TranslatorModel>("spTranslators_Get", new { Id });

    public async Task Update(TranslatorModel model)
        => await sqlServerConnection.SendData("spTranslators_Update", model);

    public async Task Delete(int Id)
        => await sqlServerConnection.SendData("spTranslators_Insert", new { Id });
}
