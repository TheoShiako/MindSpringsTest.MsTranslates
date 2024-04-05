namespace Translations.Library.Interfaces;

public interface ISqlServerConnection
{
    Task<IEnumerable<T>> GetData<T>(string sql, object parameters);
    Task SendData(string sql, object parameters);
}