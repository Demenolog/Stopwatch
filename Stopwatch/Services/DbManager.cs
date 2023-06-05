using Microsoft.EntityFrameworkCore;
using Stopwatch.Database;
using Stopwatch.Database.Base;
using Stopwatch.ViewModels;
using Stopwatch.ViewModels.Auxiliaries;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
#pragma warning disable CS8604
#pragma warning disable CS8602

namespace Stopwatch.Services
{
    internal static class DbManager
    {
        private static readonly MainWindowViewModel MainWindow = new ViewModelLocator().MainWindowModel;
        private static readonly RecordsWindowViewModel RecordsWindow = new ViewModelLocator().RecordsWindowModel;
        private static RecordsDB? s_recordsDb;

        public static async Task Add(TimeSpan time)
        {
            var lastItem = s_recordsDb
                .Records
                .OrderByDescending(item => item.RecordsId)
                .FirstOrDefault();

            var lastTotalTime = lastItem != null ? TimeSpan.ParseExact(lastItem.TotalTime, @"hh\:mm\:ss\:fff", CultureInfo.InvariantCulture) : TimeSpan.Zero;

            await s_recordsDb.Records.AddAsync(new Records()
            {
                Time = time.ToStringTime(),
                TotalTime = (time + lastTotalTime).ToStringTime()
            });

            await s_recordsDb.SaveChangesAsync();

            RecordsWindow.IsDbAny = true;

            UpdateDb();
        }

        public static async Task ClearAll()
        {
            s_recordsDb.Records.RemoveRange(s_recordsDb.Records);

            await s_recordsDb.SaveChangesAsync();

            ResetId();

            UpdateDb();

            RecordsWindow.IsDbAny = false;
        }

        public static void CloseConnection()
        {
            s_recordsDb?.Dispose();
        }

        public static async Task CreateDb()
        {
            if (s_recordsDb != null)
            {
                return;
            }

            s_recordsDb = new RecordsDB();

            bool isConnected = await s_recordsDb.Database.CanConnectAsync();

            if (!isConnected)
            {
                await s_recordsDb.Database.EnsureCreatedAsync();
                MessageBox.Show("New database created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Connected to an existing database", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MainWindow.IsSplitEnabled = true;

            RecordsWindow.IsDbAny = s_recordsDb.Records.Any();
        }

        public static async Task DeleteLast()
        {
            var lastItem = s_recordsDb.Records.OrderByDescending(e => e.RecordsId).FirstOrDefault();

            if (lastItem != null)
            {
                s_recordsDb.Records.Remove(lastItem);
                await s_recordsDb.SaveChangesAsync();

                RecordsWindow.IsDbAny = s_recordsDb.Records.Any();

                ResetId();

                UpdateDb();
            }
        }

        public static void UpdateDb()
        {
            RecordsWindow.Records = new ObservableCollection<Records>(s_recordsDb.Records.ToList());
        }

        private static void ResetId()
        {
            var lastItem = s_recordsDb.Records.OrderByDescending(e => e.RecordsId).FirstOrDefault();
            var lastId = lastItem?.RecordsId ?? 0;

            s_recordsDb.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET SEQ={lastId} WHERE NAME='Records';");
        }
    }
}