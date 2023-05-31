using Stopwatch.Services;
using System;

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
            if (RecordsWindowService.RecordsWindow != null)
            {
                RecordsWindowService.RecordsWindow.Close();
            }
        }

        #endregion Life cycle
    }
}