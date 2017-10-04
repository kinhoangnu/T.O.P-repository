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
        private Guid uuid;

        private string wc_name;
        private string wc_type;
        private string wc_handlingType;
        private ObservableCollection<Process> _observableProcess;
        private ObservableCollection<SecondaryActivity> _observableSecondaryActivity;
        private Process processRef;
        private SecondaryActivity secondaryactivityRef;

        public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        {
            get { return SecondaryActivityList.SecondaryActivities; }
            set { ChangeProperty(ref _observableSecondaryActivity, value); }
        }

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { ChangeProperty(ref _observableProcess, value); }
        }
        public Process ProcessRef
        {
            get { return processRef; }
            set { ChangeProperty(ref processRef, value); }
        }
        public SecondaryActivity SecondaryactivityRef
        {
            get { return secondaryactivityRef; }
            set { ChangeProperty(ref secondaryactivityRef, value); }
        }

        public string WC_type
        {
            get
            {
                return wc_type;
            }
            set
            {
                ChangeProperty(ref wc_type, value);
            }
        }
        public string WC_name
        {
            get
            {
                return wc_name;
            }
            set
            {
                ChangeProperty(ref wc_name, value);
            }
        }
        public string WC_handlingType
        {
            get
            {
                return wc_handlingType;
            }
            set
            {
                ChangeProperty(ref wc_handlingType, value);
            }
        }
        

        public WorkstationClass()
        {
            uuid = Guid.NewGuid();
        }
    }
}

