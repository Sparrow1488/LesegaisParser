using LesegaisParser.Intefraces;
using Quartz;
using System.Threading.Tasks;

namespace LesegaisParser.Timer.Intefaces
{
    public interface IScheduledParser<T, B> : IJob
        where T : ILesegaisParser<B>
    {
        Task StartAsync(int minutes);
    }
}
