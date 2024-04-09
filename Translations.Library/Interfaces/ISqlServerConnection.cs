namespace Translations.Library.Interfaces;

public interface ISqlServerConnection
{
    /// <summary>
    /// Returns an open SqlServer connection
    /// </summary>
    /// <returns></returns>
    SqlConnection Connection();

    /// <summary>
    /// Retrieves data from SqlServer of Type T by executing the stored procedure supplied
    /// with the parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storedProcedure"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetData<T>(string sql, object parameters);


    /// <summary>
    /// Saves data to SqlServer with no output by executing the stored procedure supplied.
    /// Used for Insert, Update and Delete queries.
    /// </summary>
    /// <param name="storedProcedure"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task SendData(string sql, object parameters);
}