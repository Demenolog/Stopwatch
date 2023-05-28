﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Stopwatch.Models.Records.Base;

namespace Stopwatch.Services
{
    internal static class DbCreationService
    {
        private static RecordsDb? s_recordsDb;

        public static async void CreateDb()
        {
            if (s_recordsDb != null)
            {
                return;
            }

            s_recordsDb = new RecordsDb();

            bool isExist = await s_recordsDb.Database.CanConnectAsync();

            if (!isExist)
            {
                await s_recordsDb.Database.EnsureCreatedAsync();
                MessageBox.Show("New database created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Database already created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}