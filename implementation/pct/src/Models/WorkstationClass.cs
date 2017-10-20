using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationClass : ContentViewModel
    {
        private string uuid;

        private string wcName;
        private string wcType;
        private string wcHandlingType;
        private ObservableCollection<Process> observableProcess;
        //private ObservableCollection<SecondaryActivity> _observableSecondaryActivity;
        private Process processRef;
        private ObservableCollection<SecondaryActivity> secondaryactivityRef;
        private SecondaryActivity scRef;

        public SecondaryActivity ScRef
        {
            get { return scRef; }
            set { scRef = value; }
        }
        //public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        //{
        //    get { return SecondaryActivityList.SecondaryActivities; }
        //    set { ChangeProperty(ref _observableSecondaryActivity, value); }
        //}

        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
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
            get
            {
                return wcType;
            }
            set
            {
                ChangeProperty(ref wcType, value);
            }
        }
        public string WcName
        {
            get
            {
                return wcName;
            }
            set
            {
                ChangeProperty(ref wcName, value);
            }
        }
        public string WcHandlingType
        {
            get
            {
                return wcHandlingType;
            }
            set
            {
                ChangeProperty(ref wcHandlingType, value);
            }
        }
        

        public WorkstationClass()
        {
        }
    }
}

