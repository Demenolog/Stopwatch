using Stopwatch.Infrastructure.Commands;
using Stopwatch.Models;
using Stopwatch.ViewModels.Base;
using System;
using System.Windows.Input;
using Stopwatch.Services;

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

        #region MainButtonStatus : string - Change button name based on status

        private string _mainButtonStatus = "Start";

        public string MainButtonStatus
        {
            get => _mainButtonStatus;
            set => SetField(ref _mainButtonStatus, value);
        }

        #endregion MainButtonStatus : string - Change button name based on status

        #region IsSplitEnabled : bool - Shows if button is available

        private bool _isSplitEnabled;

        public bool IsSplitEnabled
        {
            get => _isSplitEnabled;
            set => SetField(ref _isSplitEnabled, value);
        }

        #endregion IsSplitEnabled : bool - Show is split button enable

        #region IsResetEnabled : bool - Shows if button reset is available

        private bool _isResetEnabled;

        public bool IsResetEnabled
        {
            get => _isResetEnabled;
            set => SetField(ref _isResetEnabled, value);
        }

        #endregion IsResetEnabled : bool - Shows if button reset is available

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
        
        #region ResetStop command

        public ICommand Reset { get; }

        private bool CanResetExecuted(object p) => true;

        private void OnResetExecute(object p)
        {
            StopwatchLogic.ResetStop();
        }

        #endregion

        #region ShowRecords command

        public ICommand ShowRecords { get; }

        private bool CanShowRecordsExecuted(object p) => true;

        private void OnShowRecordsExecute(object p)
        {
            if (RecordsWindowService.Create())
            {
                DbManager.UpdateDb();
                RecordsWindowService.Show();
            }
        }

        #endregion ShowRecords command

        #region Split command

        public ICommand Split { get; }

        private bool CanSplitExecuted(object p) => true;

        private async void OnSplitExecute(object p)
        {
            await DbManager.Add(_elapsedTime);
            StopwatchLogic.ResetContinue();
        }

        #endregion Split command

        #endregion Commands

        public MainWindowViewModel()
        {
            MainButtonPressed = new LambdaCommand(OnMainButtonPressedExecute, CanMainButtonPressedExecuted);
            
            Reset = new LambdaCommand(OnResetExecute, CanResetExecuted);

            ShowRecords = new LambdaCommand(OnShowRecordsExecute, CanShowRecordsExecuted);

            Split = new LambdaCommand(OnSplitExecute, CanSplitExecuted);
        }
    }
}