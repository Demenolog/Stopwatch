using Stopwatch.ViewModels.Base;

namespace Stopwatch.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Title : string - title property

        private string? _title = "Stopwatch";

        public string? Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        #endregion Title : string - title property
    }
}