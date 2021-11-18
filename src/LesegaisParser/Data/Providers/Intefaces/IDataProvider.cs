using LesegaisParser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LesegaisParser.Data.Providers.Interfaces
{
    public interface IDataProvider<T>
    {
        Task AddAsync(Data<T> entity);
        Task<int> AddRangeAsync(IEnumerable<Data<T>> entities);
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        T Get(int id);
    }
}
