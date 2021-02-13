using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dummy_backend.Helper.SQL;
using dummy_backend.Interfaces;
using dummy_backend.Models;

namespace dummy_backend.Repositorys
{
    public class TestRepository : ITestRepository
    {
        private readonly ISqlConnectionProvider _connectionProvider;

        public TestRepository(ISqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<List<Entry>> Get()
        {
            List<Entry> entries;

            using (var con = await _connectionProvider.GetConnection())
            {
                var res = con.Query<Entry>("SELECT * FROM Dummy");

                entries = res.ToList();
            }

            return entries;
        }
        
        public async Task<Entry> Post(EntryDto entry)
        {
            const string query =
                @"INSERT INTO dbo.dummy (Name, Description) VALUES (@Name, @Description);
                  SELECT * FROM dbo.dummy where Id = LAST_INSERT_ID();";

            Entry newEntry;
            
            using (var con = await _connectionProvider.GetConnection())
            {
                newEntry = con.QueryFirst<Entry>(query, new { Name = entry.Name, Description = entry.Description });
            }

            return newEntry;
        }
        
        public async Task<Entry> Patch(Entry entry)
        {
            const string query =
                @"Update dbo.Dummy SET 
                          NAME = IFNULL(@Name, Name),
                          Description = IFNULL(@Description, Description) 
                          where Id = @Id;
                  SELECT * FROM dbo.dummy where Id = @Id";

            Entry newEntry;
            
            using (var con = await _connectionProvider.GetConnection())
            {
                newEntry = con.QueryFirst<Entry>(query, new { Id = entry.Id, Name = entry.Name, Description = entry.Description });
            }

            return newEntry;
        }
        
        public async Task Delete(int id)
        {
            const string query = "Delete From dbo.dummy where Id = @Id;";

            using (var con = await _connectionProvider.GetConnection())
            {
                con.Execute(query, new { Id = id });
            }
        }
    }
}