using System;
using System.Windows;
using System.Windows.Media;
using Stopwatch.ViewModels;

namespace Stopwatch.Models
{
    internal static class StopwatchLogic
    {
        private static System.Diagnostics.Stopwatch Stopwatch { get; set; } = null!;
        private static MainWindowViewModel MainWindow { get; }

        public static void RunOperation(bool isRunning)
        {
            try
            {
                switch (isRunning)
                {
                    case false when Stopwatch == null:
                        Start();
                        break;
                    case true when Stopwatch != null:
                        Stop();
                        break;
                    default:
                        Continue();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }

        private static void Start()
        {
            MainWindow.IsRunning = true;

            Stopwatch = new System.Diagnostics.Stopwatch();

            Stopwatch?.Start();

            CompositionTarget.Rendering +=
                (sender, args) => MainWindow.ElapsedTime = Stopwatch!.Elapsed;
        }

        private static void Stop()
        {
            MainWindow.IsRunning = false;

            Stopwatch.Stop();

            Stopwatch = null!;
        }

        private static void Continue()
        {
            MainWindow.IsRunning = true;

            Stopwatch?.Start();
        }
        
        public static void Reset()
        {
            Stopwatch?.Reset();

            // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
            CompositionTarget.Rendering -=
                (sender, args) => MainWindow.ElapsedTime = Stopwatch.Elapsed;
        }

        static StopwatchLogic()
        {
            MainWindow = new ViewModelLocator().MainWindowModel;
        }
    }
}