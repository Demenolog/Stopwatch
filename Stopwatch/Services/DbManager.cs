using Stopwatch.Database;
using Stopwatch.Database.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Stopwatch.Services
{
    internal static class DbManager
    {
        private static RecordsDB? s_recordsDb;
        public static ObservableCollection<Records>? RecordsCollection { get; set; }
        
        public static async void CreateDb()
        {
            if (s_recordsDb != null)
            {
                return;
            }

            s_recordsDb = new RecordsDB();

            bool isExist = await s_recordsDb.Database.CanConnectAsync();

            if (!isExist)
            {
                await s_recordsDb.Database.EnsureCreatedAsync();
                MessageBox.Show("New database created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Connected to database", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            RecordsCollection = new ObservableCollection<Records>(RecordsCollection!.ToArray());
        }
    }
}