/*
*  Copyright (c) 2015 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/
using System.Collections.ObjectModel;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// The StatusBarViewModel contains the following elements:
    /// - Status bar text at the bottom left side of the bar.
    /// - Icon area (connected, disconnected) on the right most area of the bar.
    /// - Command area (settings) on the left of the Icon area.
    /// - Notification area (errors, warnings) on the left of the Command area.
    /// </summary>
    public class StatusBarViewModel : ViewModel
    {
        private string _statusBarText = string.Empty;
        public string Text
        {
            get { return _statusBarText; }
            set { ChangeProperty(ref _statusBarText, value); }
        }

        private ObservableCollection<NotificationViewModel> _notifications;
        public ObservableCollection<NotificationViewModel> Notifications
        {
            get { return _notifications; }
            set { ChangeProperty(ref _notifications, value); }
        }

        private ObservableCollection<CommandViewModel> _commands;
        public ObservableCollection<CommandViewModel> Commands
        {
            get { return _commands; }
            set { ChangeProperty(ref _commands, value); }
        }

        private ObservableCollection<IconViewModel> _icons;
        public ObservableCollection<IconViewModel> Icons
        {
            get { return _icons; }
            set { ChangeProperty(ref _icons, value); }
        }


        internal StatusBarViewModel()
        {
            _notifications = new ObservableCollection<NotificationViewModel>();
            _commands = new ObservableCollection<CommandViewModel>();
            _icons = new ObservableCollection<IconViewModel>();
        }

    }
}
