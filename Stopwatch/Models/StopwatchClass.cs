using System.Windows.Media;
using Stopwatch.ViewModels;

namespace Stopwatch.Models
{
    internal static class StopwatchLogic
    {
        private static System.Diagnostics.Stopwatch Stopwatch { get; set; } = null!;
        private static ViewModelLocator ViewLocator { get; }

        public static void RunOperation(bool isRunning)
        {
            if (!isRunning && Stopwatch == null)
            {
                Start();
            }
            else if (isRunning && Stopwatch != null)
            {
                Stop();
            }
            else
            {
                Continue();
            }
        }

        public static void Start()
        {
            ViewLocator.MainWindowModel.IsRunning = true;

            Stopwatch = new System.Diagnostics.Stopwatch();

            Stopwatch?.Start();

            CompositionTarget.Rendering +=
                (sender, args) => ViewLocator.MainWindowModel.ElapsedTime = Stopwatch!.Elapsed;
        }

        public static void Stop()
        {
            ViewLocator.MainWindowModel.IsRunning = false;

            Stopwatch.Stop();

            Stopwatch = null!;
        }

        public static void Continue()
        {
            ViewLocator.MainWindowModel.IsRunning = true;

            Stopwatch?.Start();
        }

        public static void Reset()
        {
            Stopwatch?.Reset();

            // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
            CompositionTarget.Rendering -=
                (sender, args) => ViewLocator.MainWindowModel.ElapsedTime = Stopwatch.Elapsed;
        }

        static StopwatchLogic()
        {
            ViewLocator = new ViewModelLocator();
        }
    }
}