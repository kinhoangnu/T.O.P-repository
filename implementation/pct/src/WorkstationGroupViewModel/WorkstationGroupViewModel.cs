using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using com.vanderlande.wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Forms;

namespace Your
{
    class WorkstationGroupViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private WorkstationGroup selectedWorkstationGroup;
        private static ObservableCollection<WorkstationGroup> observableWorkstationGroup;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        #endregion

        #region contructor
        public WorkstationGroupViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            WorkstationGroupList.WorkstationGroups = new ObservableCollection<WorkstationGroup>();
            ObservableWorkstationGroup = WorkstationGroupList.GetWorkstationGroupList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Item that is being selected on the list 
        /// </summary>
        public WorkstationGroup SelectedWorkstationGroup
        {
            get { return selectedWorkstationGroup; }
            set
            {
                ChangeProperty(ref selectedWorkstationGroup, value);
            }
        }


        public static ObservableCollection<WorkstationGroup> ObservableWorkstationGroup
        {
            get { return WorkstationGroupList.WorkstationGroups; }
            set { observableWorkstationGroup = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new item with properties on the input controls
        /// </summary>
        public void Add()
        {
            ObservableWorkstationGroup.Add(new WorkstationGroup()
            {
                WgName = "",
                WgDescription = "",                
            });
        }
        
        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedWorkstationGroup() != null)
            {
                MessageBox.Show("This Workstation group (" + CheckMatchedWorkstationGroup().WorkstationgroupRef.WgName + ") is currently attached to a Workstation (" + CheckMatchedWorkstationGroup().WName+ "). Please:" +
                    " \n\nRemove the Workstation in \"Workstations\" tab first" +
                    "\n..Or.." +
                    "\nChange the attached Wrokstation class to another one");
            }
            else
            {
                ObservableWorkstationGroup.Remove(this.SelectedWorkstationGroup);
            }
        }

        /// <summary>
        /// Return true if a matched WorkstationGroup is found being used in a item of Workstation list
        /// </summary>
        /// <returns></returns>
        private Workstation CheckMatchedWorkstationGroup()
        {
            foreach (Workstation w in WorkstationsViewModel.ObservableWorkstation)
            {
                if (w.WorkstationgroupRef.WgName == SelectedWorkstationGroup.WgName)
                {                    
                    return w;
                }
            }
            return null;
        }
        #endregion


    }
}
