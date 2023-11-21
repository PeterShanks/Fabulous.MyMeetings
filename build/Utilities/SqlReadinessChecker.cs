using System;
using System.Data.SqlClient;
using System.Threading;
using Dapper;
using Serilog;

namespace Utilities
{
    public static class SqlReadinessChecker
    {
        public static void WaitForSqlServer(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);

            const int maxTryCounts = 30;
            var tryCounts = 0;
            while (true)
            {
                tryCounts++;
                try
                {
                    connection.QuerySingle<string>("SELECT @@Version");
                    Log.Information("Sql Server started");
                    break;
                }
                catch
                {
                    Log.Information("Sql Server not ready");
                    if (tryCounts > maxTryCounts)
                    {
                        throw new Exception("Sql Server cannot start.");
                    }

                    Thread.Sleep(2000);
                }
            }
        }
    }
}
