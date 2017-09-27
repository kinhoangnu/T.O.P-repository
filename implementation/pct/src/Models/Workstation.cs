using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class Workstation : ContentViewModel
    {
        private Guid uuid;

        private string w_name;
        private string editw_name;
        private string w_description;
        private string editw_description;
        private string w_comID;
        private string editw_comID;
        private WorkstationGroup workstationgroupRef;
        private WorkstationGroup editworkstationgroupRef;
        private WorkstationClass workstationclassRef;
        private WorkstationClass editWorkstationclassRef;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }

        public WorkstationGroup WorkstationgroupRef
        {
            get { return workstationgroupRef; }
            set { ChangeProperty(ref workstationgroupRef, value); }
        }
        public WorkstationGroup EditWorkstationgroupRef
        {
            get { return editworkstationgroupRef; }
            set { ChangeProperty(ref editworkstationgroupRef, value); }
        }
        public WorkstationClass WorkstationclassRef
        {
            get { return workstationclassRef; }
            set { ChangeProperty(ref workstationclassRef, value); }
        }

        public WorkstationClass EditworkstationClassRef
        {
            get { return editWorkstationclassRef; }
            set { ChangeProperty(ref editWorkstationclassRef, value); }
        }

        public string editW_name
        {
            get
            {
                return editw_name;
            }
            set
            {
                ChangeProperty(ref editw_name, value);
            }
        }
        public string W_name
        {
            get
            {
                return w_name;
            }
            set
            {
                ChangeProperty(ref w_name, value);
            }
        }
        public string W_description
        {
            get
            {
                return w_description;
            }
            set
            {
                ChangeProperty(ref w_description, value);
            }
        }
        public string editW_description
        {
            get
            {
                return editw_description;
            }
            set
            {
                ChangeProperty(ref editw_description, value);
            }
        }
        public string W_comID
        {
            get
            {
                return w_comID;
            }
            set
            {
                ChangeProperty(ref w_comID, value);
            }
        }
        public string editW_comID
        {
            get
            {
                return editw_comID;
            }
            set
            {
                ChangeProperty(ref editw_comID, value);
            }
        }

        public Workstation()
        {
            uuid = Guid.NewGuid();
        }
    }
}

