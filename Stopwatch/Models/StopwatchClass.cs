using Stopwatch.ViewModels;
using Stopwatch.ViewModels.Auxiliaries;
using System;
using System.Windows;
using System.Windows.Media;
using static Stopwatch.Infrastructure.Constans.ButtonStatus;

namespace Stopwatch.Models
{
    internal static class StopwatchLogic
    {
        private static MainWindowViewModel MainWindow { get; }
        private static System.Diagnostics.Stopwatch Stopwatch { get; set; }

        static StopwatchLogic()
        {
            MainWindow = new ViewModelLocator().MainWindowModel;
            MainWindow.IsResetEnabled = true;
            CompositionTarget.Rendering += UpdateStopwatch;
        }

        public static void ResetStop()
        {
            Reset();
            ChangeButtonStatus(Status.Reseted);
        }

        public static void ResetContinue()
        {
            Reset();
            if (MainWindow.IsRunning)
            {
                Stopwatch?.Start();
            }
        }

        public static void RunOperation(bool isRunning)
        {
            try
            {
                switch (isRunning)
                {
                    case false when Stopwatch == null:
                        Start();
                        ChangeButtonStatus(Status.Started);
                        break;

                    case true when Stopwatch != null:
                        Stop();
                        ChangeButtonStatus(Status.Stopped);
                        break;

                    default:
                        Continue();
                        ChangeButtonStatus(Status.Continued);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
        }

        private static void Reset()
        {
            Stopwatch?.Reset();
        }

        private static void Continue()
        {
            Stopwatch?.Start();
        }

        private static void Start()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
        }

        private static void Stop()
        {
            Stopwatch.Stop();
        }

        private static void ChangeButtonStatus(Enum status)
        {
            switch (status)
            {
                case Status.Started:
                    MainWindow.MainButtonStatus = "Stop";
                    MainWindow.IsRunning = true;
                    break;

                case Status.Stopped:
                    MainWindow.MainButtonStatus = "Continue";
                    MainWindow.IsRunning = false;
                    break;

                case Status.Reseted:
                    MainWindow.MainButtonStatus = "Start";
                    MainWindow.IsRunning = false;
                    break;

                default:
                    MainWindow.MainButtonStatus = "Stop";
                    MainWindow.IsRunning = true;
                    break;
            }
        }

        private static void UpdateStopwatch(object? sender, EventArgs args)
        {
            if (Stopwatch != null)
            {
                MainWindow.ElapsedTime = Stopwatch.Elapsed;
            }
        }
    }
}