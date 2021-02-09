using System.Collections.Generic;
using System.Threading.Tasks;
using dummy_backend.Models;

namespace dummy_backend.Interfaces
{
    public interface ITestRepository
    {
        Task<List<Entry>> Get();
        Task<Entry> Post(EntryDto entry);
        Task<Entry> Patch(Entry entry);
        Task Delete(int id);
    }
}