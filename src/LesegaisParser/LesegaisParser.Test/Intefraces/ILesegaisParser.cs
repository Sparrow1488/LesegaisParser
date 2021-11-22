using LesegaisParser.Entities;
using System.Threading.Tasks;

namespace LesegaisParser.Intefraces
{
    public interface ILesegaisParser<T>
    {
        Task<Data<T>> ParseAsync(int count, int page);
        Task<int> GetTotalCountAsync();
    }
}
