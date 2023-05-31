using System.Windows.Interop;
using System;
using Stopwatch.Services;

namespace Stopwatch
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        #region Life cycle

        //protected override void OnSourceInitialized(EventArgs e)
        //{
          
        //}

        protected override void OnClosed(EventArgs e)
        {
            RecordsWindowService.RecordsWindow.Close();
        }

        #endregion
    }
}