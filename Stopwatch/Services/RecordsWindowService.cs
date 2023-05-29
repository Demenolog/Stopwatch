using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stopwatch.Views;

namespace Stopwatch.Services
{
    internal static class RecordsWindowService
    {
        private static RecordsWindow s_recordsWindow = null;

        public static RecordsWindow RecordsWindow
        {
            get => s_recordsWindow;
            set => s_recordsWindow = value;
        }

        public static void Create()
        {
            if (RecordsWindow != null)
            {
                return;
            }

            RecordsWindow = new RecordsWindow();
            RecordsWindow.Closed += (o, args) => RecordsWindow = null;
        }

        public static bool Show()
        {
            if (RecordsWindow != null)
            {
                RecordsWindow.Show();
                RecordsWindow.Focus();
                return true;
            }

            return false;
        }
    }
}
