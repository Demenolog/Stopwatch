using System.Windows.Media;
using Stopwatch.ViewModels;

namespace Stopwatch.Models
{
    internal static class StopwatchClass
    {
        private static System.Diagnostics.Stopwatch Stopwatch { get; set; }
        private static ViewModelLocator ViewLocator { get; set; }

        public static void Start()
        {
            ViewLocator.MainWindowModel.IsRunning = true;

            Stopwatch?.Start();

            CompositionTarget.Rendering +=
                (sender, args) => ViewLocator.MainWindowModel.ElapsedTime = Stopwatch.Elapsed;
        }

        public static void Stop()
        {
            ViewLocator.MainWindowModel.IsRunning = false;

            Stopwatch.Stop();

            CompositionTarget.Rendering -=
                (sender, args) => ViewLocator.MainWindowModel.ElapsedTime = Stopwatch.Elapsed;
        }

        public static void Reset()
        {
            Stopwatch?.Reset();
        }

        static StopwatchClass()
        {
            ViewLocator = new ViewModelLocator();
            Stopwatch = new System.Diagnostics.Stopwatch();
        }
    }
}