using System.Threading.Tasks;
using MySqlConnector;

namespace dummy_backend.Helper.SQL
{
    public interface ISqlConnectionProvider
    {
        Task<MySqlConnection> GetConnection();
    }
}