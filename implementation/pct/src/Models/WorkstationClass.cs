using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationClass : ContentViewModel
    {
        private Guid uuid;

        private string wc_name;
        private string editwc_name;
        private string wc_type;
        private string editwc_type;
        private string wc_handlingType;
        private string editwc_handlingType;
        private Process processRef;
        private Process editprocessRef;
        private SecondaryActivity secondaryactivityRef;
        private SecondaryActivity editsecondaryactivityRef;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public Process ProcessRef
        {
            get { return processRef; }
            set { ChangeProperty(ref processRef, value); }
        }
        public Process EditprocessRef
        {
            get { return editprocessRef; }
            set { ChangeProperty(ref editprocessRef, value); }
        } 
        public SecondaryActivity SecondaryactivityRef
        {
            get { return secondaryactivityRef; }
            set { ChangeProperty(ref secondaryactivityRef, value); }
        }

        public SecondaryActivity EditsecondaryactivityRef
        {
            get { return editsecondaryactivityRef; }
            set { ChangeProperty(ref editsecondaryactivityRef, value); }
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
        public string editWC_type
        {
            get
            {
                return editwc_type;
            }
            set
            {
                ChangeProperty(ref editwc_type, value);
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
        public string editWC_name
        {
            get
            {
                return editwc_name;
            }
            set
            {
                ChangeProperty(ref editwc_name, value);
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
        public string editWC_handlingType
        {
            get
            {
                return editwc_handlingType;
            }
            set
            {
                ChangeProperty(ref editwc_handlingType, value);
            }
        }

        public WorkstationClass()
        {
            uuid = Guid.NewGuid();
        }
    }
}

