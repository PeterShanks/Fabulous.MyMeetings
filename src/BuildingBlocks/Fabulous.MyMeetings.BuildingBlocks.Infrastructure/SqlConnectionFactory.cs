using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using System.Data;
using System.Data.SqlClient;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable

{
    private IDbConnection? _connection;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <exception cref="InvalidOperationException">Connection already created.</exception>
    public IDbConnection GetOpenConnection()
    {
        if (_connection is not null && _connection.State != ConnectionState.Open)
            throw new InvalidOperationException("Connection already created.");

        if (_connection is not null)
            return _connection;

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        return _connection;
    }

    public IDbConnection CreateNewConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }

    public string GetConnectionString()
    {
        return connectionString;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _connection?.Dispose();
    }

    ~SqlConnectionFactory()
    {
        Dispose(false);
    }
}