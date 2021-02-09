using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dummy_backend.Helper.SQL;
using dummy_backend.Interfaces;

namespace dummy_backend.Repositorys
{
    public class TestRepository : ITestRepository
    {
        private readonly ISqlConnectionProvider _connectionProvider;

        public TestRepository(ISqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task test()
        {

            using (var con = await _connectionProvider.GetConnection())
            {
                var res = con.Query("SELECT * FROM TestEntry");

                var list = res.ToList();
            }
        }
    }
}