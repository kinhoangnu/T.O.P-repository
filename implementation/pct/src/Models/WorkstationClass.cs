using System;
using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationClass : ContentViewModel
    {
        private string wcName;
        private string wcType;
        private string wcHandlingType;
        private ObservableCollection<Process> observableProcess;
        //private ObservableCollection<SecondaryActivity> _observableSecondaryActivity;
        private Process processRef;
        private ObservableCollection<SecondaryActivity> secondaryactivityRef;

        public SecondaryActivity ScRef { get; set; }

        //public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        //{
        //    get { return SecondaryActivityList.SecondaryActivities; }
        //    set { ChangeProperty(ref _observableSecondaryActivity, value); }
        //}

        public string Uuid { get; set; }

        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { ChangeProperty(ref observableProcess, value); }
        }

        public Process ProcessRef
        {
            get { return processRef; }
            set { ChangeProperty(ref processRef, value); }
        }

        public ObservableCollection<SecondaryActivity> SecondaryactivityRef
        {
            get { return secondaryactivityRef; }
            set { ChangeProperty(ref secondaryactivityRef, value); }
        }

        public string WcType
        {
            get { return wcType; }
            set { ChangeProperty(ref wcType, value); }
        }

        public string WcName
        {
            get { return wcName; }
            set { ChangeProperty(ref wcName, value); }
        }

        public string WcHandlingType
        {
            get { return wcHandlingType; }
            set { ChangeProperty(ref wcHandlingType, value); }
        }

        public WorkstationClass()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}