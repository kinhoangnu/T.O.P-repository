/*
*  Copyright (c) 2015 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/

using System.Windows;
using com.vanderlande.wpf;

namespace Your
{
    public class YourApplication : ViApplication
    {
        public static NotificationViewModel Alarm;
        public static NotificationViewModel Warning;
        public static NotificationViewModel Information;
        public static event ClickOnNotification OnAlarm;
        public static event ClickOnNotification OnWarning;
        public static event ClickOnNotification OnInfo;

        public delegate void ClickOnNotification();

        public YourApplication() :
            base("Configuration tool ", "T.O.P Project")
        {
            OnAlarm += NoAction; // Add 'no action' to avoid NullReferenceExceptions
            OnWarning += NoAction;
            OnInfo += NoAction;

            Alarm = new AlarmNotificationViewModel(new RelayCommand(x => OnAlarm()));
            Warning = new WarningNotificationViewModel(new RelayCommand(x => OnWarning()));
            Information = new InfoNotificationViewModel(new RelayCommand(x => OnInfo()));
        }

        protected override MainWindowViewModel CreateMainWindowViewModel()
        {
            var mainWnd = base.CreateMainWindowViewModel();
            mainWnd.RegisterContent(typeof(BuffersViewModel));
            mainWnd.RegisterContent(typeof(ProductionAreasViewModel));
            mainWnd.RegisterContent(typeof(ProcessesViewModel));
            mainWnd.RegisterContent(typeof(SecondaryActivitiesViewModel));
            mainWnd.RegisterContent(typeof(WorkstationClassesViewModel));
            mainWnd.RegisterContent(typeof(WorkstationGroupViewModel));
            mainWnd.RegisterContent(typeof(WorkstationsViewModel));
            mainWnd.RegisterContent(typeof(OperatorsViewModel));
            mainWnd.RegisterContent(typeof(OperatorActivitiesViewModel));
            mainWnd.RegisterContent(typeof(OperatorPresencesViewModel));

            mainWnd.ActivateContent(typeof(LoadXmlViewModel));
            mainWnd.RegisterContent(typeof(WarningsViewModel));

            return mainWnd;
        }

        protected override void OnStartup(object sender, StartupEventArgs args)
        {
            base.OnStartup(sender, args);

            MainWindowViewModel.AddNotification(Alarm);
            MainWindowViewModel.AddNotification(Warning);
            MainWindowViewModel.AddNotification(Information);
        }

        private void NoAction()
        {
        }
    }
}