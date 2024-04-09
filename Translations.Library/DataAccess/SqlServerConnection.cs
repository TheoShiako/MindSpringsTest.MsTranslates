namespace Translations.Library.DataAccess;

public class SqlServerConnection : ISqlServerConnection
{
    private readonly IConfiguration config;

    public SqlServerConnection(IConfiguration config)
    {
        this.config = config;
    }

    /// <summary>
    /// Initializes the sql server connection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="Exception"></exception>
    private SqlConnection GetConnection()
    {
        var connectionString = config.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            connection.Close();
            throw new Exception(ex.Message);
        }
    }

    public SqlConnection Connection()
        => GetConnection();

    public async Task<IEnumerable<T>> GetData<T>(string storedProcedure, object parameters)
    {
        using SqlConnection connection = GetConnection();
        return await connection.QueryAsync<T>(storedProcedure, param: parameters);
    }

    public async Task SendData(string storedProcedure, object parameters)
    {
        using SqlConnection connection = GetConnection();
        var result = await connection.ExecuteAsync(storedProcedure, parameters);

    }
}
