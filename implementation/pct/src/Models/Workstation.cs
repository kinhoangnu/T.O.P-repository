﻿using System;
using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class Workstation : ContentViewModel
    {
        private string wName;
        private string wDescription;
        private string wComId;
        private WorkstationGroup workstationgroupRef;
        private WorkstationClass workstationclassRef;
        private ObservableCollection<WorkstationGroup> observableWorkstationGroup;
        private ObservableCollection<WorkstationClass> observableWorkstationClass;

        public ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { ChangeProperty(ref observableWorkstationClass, value); }
        }

        public ObservableCollection<WorkstationGroup> ObservableWorkstationGroup
        {
            get { return WorkstationGroupList.WorkstationGroups; }
            set { ChangeProperty(ref observableWorkstationGroup, value); }
        }

        public string Uuid { get; set; }

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

        public string WName
        {
            get { return wName; }
            set { ChangeProperty(ref wName, value); }
        }

        public string WDescription
        {
            get { return wDescription; }
            set { ChangeProperty(ref wDescription, value); }
        }

        public string WComId
        {
            get { return wComId; }
            set { ChangeProperty(ref wComId, value); }
        }

        public Workstation()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}