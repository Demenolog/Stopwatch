using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stopwatch.Database;
using Stopwatch.Database.Base;
using Stopwatch.Services;
using Stopwatch.ViewModels.Base;

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

        #region Records : ObservableCollection<Records> - get records db

        public ObservableCollection<Records> Records { get; set; }

        #endregion Records : ObservableCollection<Records> - get records db

        public RecordsWindowViewModel()
        {
            using (var db = new RecordsDB())
            {
                Records = new ObservableCollection<Records>(db.Records.ToList());
            }
        }
    }
}
