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
    public class Workstation : ContentViewModel
    {
        private Guid uuid;

        private string w_name;
        private string w_description;
        private string w_comID;
        private WorkstationGroup workstationgroupRef;
        private WorkstationClass workstationclassRef;
        private ObservableCollection<WorkstationGroup> _observableWorkstationGroup;
        private ObservableCollection<WorkstationClass> _observableWorkstationClass;


        public ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { ChangeProperty(ref _observableWorkstationClass, value); }
        }

        public ObservableCollection<WorkstationGroup> ObservableWorkstationGroup
        {
            get { return WorkstationGroupList.WorkstationGroups; }
            set { ChangeProperty(ref _observableWorkstationGroup, value); }
        }

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
        public WorkstationClass WorkstationclassRef
        {
            get { return workstationclassRef; }
            set { ChangeProperty(ref workstationclassRef, value); }
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

        public Workstation()
        {
            uuid = Guid.NewGuid();
        }
    }
}

