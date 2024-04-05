using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Translations.Library.Interfaces;

namespace Translations.Library.DataAccess;
public class SqlServerConnection : ISqlServerConnection
{
    private readonly IConfiguration config;

    public SqlServerConnection(IConfiguration config)
    {
        this.config = config;
    }

    private SqlConnection GetConnection()
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
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
