using Stopwatch.Views;
using System;
using System.Windows.Media.Imaging;

namespace Stopwatch.Services
{
    internal static class RecordsWindowService
    {
        private static RecordsWindow? s_recordsWindow;

        public static RecordsWindow? RecordsWindow
        {
            get => s_recordsWindow;
            set => s_recordsWindow = value;
        }

        public static bool Create()
        {
            if (RecordsWindow != null)
            {
                return false;
            }

            _ = DbManager.CreateDb();

            RecordsWindow = new RecordsWindow
            {
                Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/RecordsIcon.ico"))
            };

            RecordsWindow.Closed += (o, args) => RecordsWindow = null;

            return true;
        }

        public static void Show()
        {
            RecordsWindow?.Show();
            RecordsWindow?.Focus();
        }
    }
}