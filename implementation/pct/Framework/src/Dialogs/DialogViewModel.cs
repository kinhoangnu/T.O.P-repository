/*
*  Copyright (c) 2016 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/

using System;
using System.Windows;
using System.Windows.Threading;

namespace com.vanderlande.wpf
{
    public class DialogViewModel : WindowViewModel
    {
        private readonly TimeSpan? _duration;
        private DispatcherTimer _timer;

        private readonly Action<bool> _onAccepted;
        private readonly Action<bool> _onDeclined;
        private bool? _onAcceptResult;
        private bool? _onDeclineResult;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { ChangeProperty(ref _message, value); }
        }

        public string AcceptButtonText { get; private set; }
        public string DeclineButtonText { get; private set; }

        public static TimeSpan DefaultDuration
        {
            get { return TimeSpan.FromSeconds(3); }
        }

        public RelayCommand AcceptCommand { get; private set; }
        public RelayCommand DeclineCommand { get; private set; }


        /// <summary>
        /// Displays a dialog with a message and a single button
        /// </summary>
        /// <param name="message">The message to display in the dialog</param>
        /// <param name="okButtonText">The text to display on the OK button</param>
        public DialogViewModel(string message, string okButtonText)
            : this(message,
                   okButtonText,
                   null)
        { }

        /// <summary>
        /// Displays a dialog with a message and a single button
        /// </summary>
        /// <param name="message">The message to display in the dialog</param>
        /// <param name="okButtonText">The text to display on the OK button</param>
        /// <param name="onOk">Code that is executed when the user clicks the OK button</param>
        public DialogViewModel(string message, string okButtonText, Func<object, bool> onOk)
            : this(message,
                   okButtonText,
                   onOk,
                   (x => { }))
        { }

        /// <summary>
        /// Displays a dialog with a message and a single button
        /// </summary>
        /// <param name="message">The message to display in the dialog</param>
        /// <param name="okButtonText">The text to display on the OK button</param>
        /// <param name="onOk">Code that is executed when the user clicks the OK/Yes button</param>
        /// <param name="onOked">Code that is executed based on the result of the "onOk" function</param>
        public DialogViewModel(string message, string okButtonText, Func<object, bool> onOk, Action<bool> onOked)
            : this(message,
                   okButtonText,
                   onOk,
                   (x => true),
                   onOked,
                   null,
                   null,
                   null,
                   null)
        { }

        /// <summary>
        /// Displays a dialog with a message and two buttons: a button to accept and a button to decline
        /// </summary>
        /// <param name="message">The message to display in the dialog</param>
        /// <param name="acceptButtonText">The text to display on the OK/Yes button</param>
        /// <param name="onAccept">Code that is executed when the user clicks the OK/Yes button</param>
        /// <param name="canAccept">Code to evaluate the OK/Yes button should be enabled</param>
        /// <param name="onAccepted">Code that is executed based on the result of the "onAccept" function</param>
        /// <param name="declineButtonText">The text to display on the Decline/No button</param>
        public DialogViewModel(string message,
                              string acceptButtonText,
                              Func<object, bool> onAccept, 
                              Predicate<object> canAccept, 
                              Action<bool> onAccepted,
                              string declineButtonText)
            : this(message,
                   acceptButtonText,
                   onAccept,
                   canAccept,
                   onAccepted,
                   declineButtonText,
                   null,
                   (x => true),
                   null)
        {}

        /// <summary>
        /// Displays a dialog with a message and two buttons: a button to accept and a button to decline
        /// </summary>
        /// <param name="message">The message to display in the dialog</param>
        /// <param name="acceptButtonText">The text to display on the OK/Yes button</param>
        /// <param name="onAccept">Code that is executed when the user clicks the OK/Yes button</param>
        /// <param name="canAccept">Code to evaluate the OK/Yes button should be enabled</param>
        /// <param name="onAccepted">Code that is executed based on the result of the "onAccept" function</param>
        /// <param name="declineButtonText">The text to display on the Decline/No button</param>
        /// <param name="onDecline">Code that is executed when the user clicks the Decline/No button</param>
        /// <param name="canDecline">Code to evaluate the Decline/No button should be enabled</param>
        /// <param name="onDeclined">Code that is executed based on the result of the "onDecline" function</param>
        public DialogViewModel(string message,
                               string acceptButtonText,
                               Func<object, bool> onAccept, 
                               Predicate<object> canAccept, 
                               Action<bool> onAccepted,
                               string declineButtonText,
                               Func<object, bool> onDecline,
                               Predicate<object> canDecline, 
                               Action<bool> onDeclined)
        {
            Message = message;
            AcceptButtonText = acceptButtonText;
            DeclineButtonText = declineButtonText;

            Action<object> onAcceptAction = obj =>
            {
                if (onAccept != null)
                    _onAcceptResult = onAccept(obj);
            };

            Action<object> onDeclineAction = obj =>
            {
                if (onDecline != null)
                    _onDeclineResult = onDecline(obj);
            };

            AcceptCommand = new RelayCommand(onAcceptAction + Close, canAccept ?? (x => true));
            DeclineCommand = new RelayCommand(onDeclineAction + Close, canDecline ?? (x => true));

            _onAccepted = onAccepted;
            _onDeclined = onDeclined;
        }

        public DialogViewModel(string message) : 
            this (message, String.Empty)
        { }


        public DialogViewModel(string message, TimeSpan duration) : 
            this(message)
        {
            _duration = duration;

            _timer = new DispatcherTimer();
            _timer.Interval = _duration.Value;
            _timer.Tick += (o, e) =>
                {
                    _timer.Stop();
                    Close();
                };
            _timer.Start();
        }

        public void Show()
        {
            ViewLocator.CreateView(this);
            if (Window != null)
            {
                Window.ShowDialog();
            }
            else
            {
                MainWindowViewModel mwvm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                mwvm.PushModalDialog(Element);
            }
        }

        public override void Close()
        {
            Close(null);
        }

        protected void Close(object obj)
        {
            if (Window == null)
            {
                MainWindowViewModel mwvm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
                mwvm.PopModalDialog(Element);
            }
            else
            {
                Window.Close();
            }
            OnClosed();
        }

        protected override void OnClosed()
        {
            base.OnClosed();

            if (_onAccepted != null && _onAcceptResult.HasValue)
                _onAccepted(_onAcceptResult.Value);

            if (_onDeclined != null && _onDeclineResult.HasValue)
                _onDeclined(_onDeclineResult.Value);
        }
    }
}
