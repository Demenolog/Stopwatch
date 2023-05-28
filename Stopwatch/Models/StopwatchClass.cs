using System;
using System.Data;
using System.Windows;
using System.Windows.Media;
using Stopwatch.ViewModels;
using static Stopwatch.Infrastructure.Constans.ButtonStatus;

namespace Stopwatch.Models
{
    internal static class StopwatchLogic
    {
        private static MainWindowViewModel MainWindow { get; }
        private static System.Diagnostics.Stopwatch Stopwatch { get; set; } = null!;

        static StopwatchLogic()
        {
            MainWindow = new ViewModelLocator().MainWindowModel;
        }

        public static void Reset()
        {
            Stopwatch?.Reset();

            ChangeButtonStatus(Status.Reseted);

            //CompositionTarget.Rendering -= UpdateStopwatch;
        }

        public static void RunOperation(bool isRunning)
        {
            try
            {
                switch (isRunning)
                {
                    case false when Stopwatch == null:
                        ChangeButtonStatus(Status.Started);
                        Start();
                        break;
                    case true when Stopwatch != null:
                        ChangeButtonStatus(Status.Stopped);
                        Stop();
                        break;
                    default:
                        ChangeButtonStatus(Status.Continued);
                        Continue();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
            }
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

        private static void Continue()
        {
            Stopwatch?.Start();
        }

        private static void Start()
        {
            Stopwatch = new System.Diagnostics.Stopwatch();

            Stopwatch?.Start();
            
            CompositionTarget.Rendering += UpdateStopwatch;
        }

        private static void Stop()
        {
            Stopwatch.Stop();

            //CompositionTarget.Rendering -= UpdateStopwatch;
        }

        private static void UpdateStopwatch(object? sender, EventArgs args)
        {
            MainWindow.ElapsedTime = Stopwatch!.Elapsed;
        }
    }
}