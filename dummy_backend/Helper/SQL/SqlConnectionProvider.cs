using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace dummy_backend.Helper.SQL
{
    public class SqlConnectionProvider : ISqlConnectionProvider
    {
        public readonly SqlCredentials _SqlCredentials;
        public readonly ILogger _logger;

        public SqlConnectionProvider(IOptions<SqlCredentials> sqlCredentials, ILoggerFactory loggerFactory)
        {
            _SqlCredentials = sqlCredentials.Value;
            _logger = loggerFactory.CreateLogger<ISqlConnectionProvider>();
        }

        public async Task<MySqlConnection> GetConnection()
        {
            if (string.IsNullOrEmpty(_SqlCredentials.ConnectionString))
            {
                throw new ArgumentException($"'[{_SqlCredentials.ConnectionString}]' ConnectionString was not found");
            }
            
            _logger.LogInformation($"Try to get mariadb connection: {_SqlCredentials.ConnectionString}");
            
            var connection = new MySqlConnection("Server=localhost;Database=dbo;Uid=dotnet;Pwd=dotnet;");
            await connection.OpenAsync();
            
            _logger.LogInformation($"Connection Opened successfully: {_SqlCredentials.ConnectionString}");

            return connection;
        }
    }
}