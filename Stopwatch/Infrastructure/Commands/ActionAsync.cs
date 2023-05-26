using System.Threading.Tasks;

namespace Stopwatch.Infrastructure.Commands
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}