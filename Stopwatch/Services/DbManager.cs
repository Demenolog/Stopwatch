using System;
using Stopwatch.Database;
using Stopwatch.Database.Base;
using Stopwatch.ViewModels;
using Stopwatch.ViewModels.Auxiliaries;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Stopwatch.Services
{
    internal static class DbManager
    {
        private static RecordsDB? s_recordsDb;
        private static readonly RecordsWindowViewModel? RecordsWindow = new ViewModelLocator().RecordsWindowModel;

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
                MessageBox.Show("Connected to database", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static async Task ClearAll()
        {
            s_recordsDb?.RemoveRange(s_recordsDb.Records!);

            await s_recordsDb?.SaveChangesAsync()!;

            ResetId();

            UpdateDb();
        }

        public static async Task Add(TimeSpan time)
        {
            if (s_recordsDb!.Records!.Any())
            {
                var lastItem = s_recordsDb.Records!
                    .OrderByDescending(item => item.RecordsId)
                    .FirstOrDefault();

                //var lastTime = TimeSpan.Parse(lastItem.Time);
                var lastTotalTime = TimeSpan.ParseExact(lastItem.TotalTime, @"hh\:mm\:ss\:fff", CultureInfo.InvariantCulture);

                await s_recordsDb.Records!.AddAsync(new Records()
                {
                    Time = time.ToString(@"hh\:mm\:ss\:fff"),
                    TotalTime = (time + lastTotalTime).ToString(@"hh\:mm\:ss\:fff")
                });
            }
            else
            {
                await s_recordsDb.Records!.AddAsync(new Records()
                {
                    Time = time.ToString(@"hh\:mm\:ss\:fff"),
                    TotalTime = time.ToString(@"hh\:mm\:ss\:fff")
                });
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
                    ResetId();
                }

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
            s_recordsDb!.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='Records';");
        }
    }
}