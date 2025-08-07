using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable

{
    private IDbConnection? _connection;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IDbConnection GetOpenConnection()
    {
        if (_connection == null)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
        else if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection;
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