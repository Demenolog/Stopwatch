using Microsoft.Extensions.DependencyInjection;

namespace Stopwatch.ViewModels.Auxiliaries
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModel(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<RecordsWindowViewModel>()
        ;
    }
}
