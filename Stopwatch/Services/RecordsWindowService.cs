﻿using System.Threading.Tasks;
using Stopwatch.Views;

namespace Stopwatch.Services
{
    internal static class RecordsWindowService
    {
        private static RecordsWindow? s_recordsWindow = null;

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

            RecordsWindow = new RecordsWindow();
            RecordsWindow.Closed += (o, args) => RecordsWindow = null;

            return true;
        }

        public static void Show()
        {
            if (RecordsWindow != null)
            {
                RecordsWindow.Show();
                RecordsWindow.Focus();
            }
        }
    }
}