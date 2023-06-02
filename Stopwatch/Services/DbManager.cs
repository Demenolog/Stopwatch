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

namespace Stopwatch.Services
{
    internal static class DbManager
    {
        private static RecordsDB? s_recordsDb;
        private static readonly RecordsWindowViewModel? RecordsWindow = new ViewModelLocator().RecordsWindowModel;
        private static readonly MainWindowViewModel? MainWindow = new ViewModelLocator().MainWindowModel;

        public static async Task CreateDb()
        {
            if (s_recordsDb != null)
            {
                return;
            }

            s_recordsDb = new RecordsDB();

            if (!await s_recordsDb.Database.CanConnectAsync())
            {
                await s_recordsDb.Database.EnsureCreatedAsync();
                MessageBox.Show("New database created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Connected to an existing database", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MainWindow!.IsSplitEnabled = true;

            if (s_recordsDb!.Records!.Any())
            {
                RecordsWindow.IsDbAny = true;
            }
        }

        public static async Task ClearAll()
        {
            s_recordsDb?.RemoveRange(s_recordsDb.Records!);

            await s_recordsDb?.SaveChangesAsync()!;

            ResetId();

            UpdateDb();

            RecordsWindow.IsDbAny = false;
        }

        public static async Task Add(TimeSpan time)
        {
            if (s_recordsDb!.Records!.Any())
            {
                var lastItem = s_recordsDb.Records!
                    .OrderByDescending(item => item.RecordsId)
                    .FirstOrDefault();

                var lastTotalTime = TimeSpan.ParseExact(lastItem.TotalTime, @"hh\:mm\:ss\:fff", CultureInfo.InvariantCulture);

                await s_recordsDb.Records!.AddAsync(new Records()
                {
                    Time = time.ToStringTime(),
                    TotalTime = (time + lastTotalTime).ToStringTime()
                });
            }
            else
            {
                await s_recordsDb.Records!.AddAsync(new Records()
                {
                    Time = time.ToStringTime(),
                    TotalTime = time.ToStringTime()
                });

                RecordsWindow.IsDbAny = true;
            }

            await s_recordsDb.SaveChangesAsync();

            UpdateDb();
        }

        public static async Task DeleteLast()
        {
            var lastItem = s_recordsDb!.Records!.OrderByDescending(e => e.RecordsId).FirstOrDefault();

            if (lastItem != null)
            {
                s_recordsDb.Records!.RemoveRange(lastItem);
                await s_recordsDb.SaveChangesAsync();

                if (!s_recordsDb.Records.Any())
                {
                    RecordsWindow.IsDbAny = false;
                }

                ResetId();

                UpdateDb();
            }
        }

        public static void CloseConnection()
        {
            if (s_recordsDb != null)
            {
                s_recordsDb.Dispose();
            }
        }

        public static void UpdateDb()
        {
            RecordsWindow!.Records = new ObservableCollection<Records>(s_recordsDb!.Records!.ToList());
        }

        private static void ResetId()
        {
            var lastItem = s_recordsDb!.Records!.OrderByDescending(e => e.RecordsId).FirstOrDefault();
            var lastId = 0;

            if (lastItem != null)
            {
                lastId = lastItem.RecordsId;
            }

            s_recordsDb!.Database.ExecuteSqlRaw($"UPDATE SQLITE_SEQUENCE SET SEQ={lastId} WHERE NAME='Records';");
        }
    }
}