using Stopwatch.Database;
using Stopwatch.Infrastructure.Commands;
using Stopwatch.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Stopwatch.Services;

namespace Stopwatch.ViewModels
{
    internal class RecordsWindowViewModel : ViewModel
    {
        #region Title : string - title for records window

        private string _title = "Records";

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        #endregion Title : string - title for records window

        #region Records : ObservableCollection<Records> - get records collection

        private ObservableCollection<Records> _records;

        public ObservableCollection<Records> Records
        {
            get => _records;
            set => SetField(ref _records, value);
        }

        #endregion Records : ObservableCollection<Records> - get records collection

        #region DbClearAll command

        public ICommand DbClearAll { get; }

        private bool CanDbClearAllExecuted(object p) => true;

        private async void OnDbClearAllExecute(object p)
        {
            await DbManager.ClearAll();
        }

        #endregion DbClearAll command

        public RecordsWindowViewModel()
        {
            DbClearAll = new LambdaCommand(OnDbClearAllExecute, CanDbClearAllExecuted);
        }
    }
}