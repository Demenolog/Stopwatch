﻿using Stopwatch.ViewModels.Base;
using System;
using System.Windows.Input;
using Stopwatch.Infrastructure.Commands;
using Stopwatch.Models;

namespace Stopwatch.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Title : string - title property

        private string? _title = "Stopwatch";

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

        public ICommand MainButtonPressed { get; }

        private bool CanMainButtonPressedExecuted(object p) => true;

        private void OnMainButtonPressedExecute(object p)
        {
            StopwatchLogic.RunOperation(IsRunning);
        }

        #endregion Start command

        public ICommand Reset { get; }

        private bool CanResetExecuted(object p) => true;

        private void OnResetExecute(object p)
        {
        }

        #endregion Commands

        public MainWindowViewModel()
        {
            MainButtonPressed = new LambdaCommand(OnMainButtonPressedExecute, CanMainButtonPressedExecuted);

            Reset = new LambdaCommand(OnResetExecute, CanResetExecuted);
        }
    }
}