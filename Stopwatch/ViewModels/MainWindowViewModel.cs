using Stopwatch.Infrastructure.Commands;
using Stopwatch.Models;
using Stopwatch.ViewModels.Base;
using System;
using System.Windows.Input;

namespace Stopwatch.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Title : string - title property

        private string? _title = "StopwatchClass";

        public string? Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        #endregion Title : string - title property

        #region ElapsedTime : TimeSpan - variable which keep counts time

        private TimeSpan _elapsedTime;

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetField(ref _elapsedTime, value);
        }

        #endregion ElapsedTime : TimeSpan - variable which keep counts time

        #region IsRunning : bool - Tracks the status of an operation

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set => SetField(ref _isRunning, value);
        }

        #endregion IsRunning : bool - Tracks the status of an operation

        #endregion Properties

        #region Commands

        #region Start command

        public ICommand Start { get; }

        private bool CanStartExecuted(object p) => true;

        private void OnStartExecute(object p)
        {
            StopwatchClass.Start();
        }

        #endregion Start command

        #region Stop command

        public ICommand Stop { get; }

        private bool CanStopExecuted(object p) => true;

        private void OnStopExecute(object p)
        {
            StopwatchClass.Stop();
        }

        #endregion

        public ICommand Reset { get; }

        private bool CanResetExecuted(object p) => true;

        private void OnResetExecute(object p)
        {
            StopwatchClass.Reset();
        }


        #endregion Commands

        public MainWindowViewModel()
        {
            Start = new LambdaCommand(OnStartExecute, CanStartExecuted);

            Stop = new LambdaCommand(OnStopExecute, CanStopExecuted);

            Reset = new LambdaCommand(OnResetExecute, CanResetExecuted);
        }
    }
}