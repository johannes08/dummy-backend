using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace dummy_backend.Helper.SQL
{
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        public readonly SqlCredentials _SqlCredentials;

        public SqlConnectionProvider(IOptions<SqlCredentials> sqlCredentials)
        {
            _SqlCredentials = sqlCredentials.Value;
        }

        public async Task<MySqlConnection> GetConnection()
        {
            if (string.IsNullOrEmpty(_SqlCredentials.ConnectionString))
            {
                throw new ArgumentException($"'[{_SqlCredentials.ConnectionString}]' ConnectionString was not found");
            }
            var connection = new MySqlConnection(_SqlCredentials.ConnectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}