using LesegaisParser.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LesegaisParser.Data.Providers.Interfaces
{
    public interface IDataProvider<T>
    {
        Task AddAsync(Data<T> entity);
        Task AddRangeAsync(IEnumerable<Data<T>> entities);
        T Get(int id);
    }
}
