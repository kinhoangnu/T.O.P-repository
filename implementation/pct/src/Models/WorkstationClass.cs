using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationClass : ContentViewModel
    {
        //Fields
        private string wcName;
        private string wcType;
        private string wcHandlingType;
        private List<string> wcHandlingTypeList;

        private ObservableCollection<Process> observableProcess;
        private Process processRef;
        private ObservableCollection<SecondaryActivity> secondaryactivityRef;

        public List<string> WcHandlingTypeList
        {
            get { return new List<string> { "Manual", "Automatic", "SemiAutomatic" }; }
            set { ChangeProperty(ref wcHandlingTypeList, value); }
        }

        //Properties
        public SecondaryActivity ScRef { get; set; }

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

        //Constructor
        public WorkstationClass()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}