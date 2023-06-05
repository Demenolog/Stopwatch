using Microsoft.Extensions.DependencyInjection;

namespace Stopwatch.ViewModels.Auxiliaries
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();

        public RecordsWindowViewModel RecordsWindowModel => App.Services.GetRequiredService<RecordsWindowViewModel>();
    }
}
