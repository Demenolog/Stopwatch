using Stopwatch.Database;
using Stopwatch.Infrastructure.Commands;
using Stopwatch.Services;
using Stopwatch.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Stopwatch.ViewModels
{
    internal class RecordsWindowViewModel : ViewModel
    {
        #region Properties

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

        #region IsDbAny : bool - Show if db is empty

        private bool _isDbAny;

        public bool IsDbAny
        {
            get => _isDbAny;
            set => SetField(ref _isDbAny, value);
        }

        #endregion IsDbAny : bool - Show if db is empty

        #endregion Properties

        #region Commands

        #region DbClearAll command

        public ICommand DbClearAll { get; }

        private bool CanDbClearAllExecuted(object p) => true;

        private async void OnDbClearAllExecute(object p)
        {
            await DbManager.ClearAll();
        }

        #endregion DbClearAll command

        #region DbDeleteLast command

        public ICommand DbDeleteLast { get; }

        private bool CanDbDeleteLastExecuted(object p) => true;

        private async void OnDbDeleteLastExecute(object p)
        {
            await DbManager.DeleteLast();
        }

        #endregion DbDeleteLast command

        #endregion Commands

        public RecordsWindowViewModel()
        {
            DbClearAll = new LambdaCommand(OnDbClearAllExecute, CanDbClearAllExecuted);

            DbDeleteLast = new LambdaCommand(OnDbDeleteLastExecute, CanDbDeleteLastExecuted);
        }
    }
}