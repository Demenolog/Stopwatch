using Stopwatch.Database;
using Stopwatch.Database.Base;
using Stopwatch.ViewModels;
using Stopwatch.ViewModels.Auxiliaries;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Stopwatch.Services
{
    internal static class DbManager
    {
        private static RecordsDB? s_recordsDb;
        private static RecordsWindowViewModel? s_recordsWindow = new ViewModelLocator().RecordsWindowModel;

        public static async Task CreateDb()
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
        }

        public static async Task ClearAll()
        {
            s_recordsDb?.RemoveRange(s_recordsDb.Records!);

            s_recordsDb?.SaveChangesAsync();

            UpdateDb();
        }

        public static void UpdateDb()
        {
            s_recordsWindow!.Records = new ObservableCollection<Records>(s_recordsDb!.Records!.ToList());
        }
    }
}