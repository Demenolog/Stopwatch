using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
