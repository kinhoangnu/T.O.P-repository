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
    public class WorkstationClassesViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private WorkstationClass selectedWorkstationClass;
        private WorkstationClass _tobeEditedItem;

        private static ObservableCollection<WorkstationClass> _observableWorkstationClass;
        private ObservableCollection<Process> _observableProcess;
        private ObservableCollection<SecondaryActivity> _observableSecondaryActivity;

        public WorkstationClassList WClist;
        public ProcessList PClist;
        public SecondaryActivity SClist;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public WorkstationClassesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
                      
            ObservableSecondaryActivity = new ObservableCollection<SecondaryActivity>();
            ObservableSecondaryActivity = SecondaryActivityList.GetSecondaryActivityList();
            ObservableProcess = new ObservableCollection<Process>();
            ObservableProcess = ProcessList.GetProcessList();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();  
            ObservableWorkstationClass = WorkstationClassList.GetWorkstationClassList();
            this.SelectedWorkstationClass = ObservableWorkstationClass.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public static ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { _observableWorkstationClass = value; }
        }

        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { _observableProcess = value; }
        }

        public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        {
            get { return SecondaryActivityList.SecondaryActivities; }
            set { ChangeProperty(ref _observableSecondaryActivity, value); }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public WorkstationClass TobeEditedItem
        {
            get { return _tobeEditedItem; }
            set
            {
                ChangeProperty(ref _tobeEditedItem, value);
            }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public WorkstationClass SelectedWorkstationClass
        {
            get { return selectedWorkstationClass; }
            set
            {
                ChangeProperty(ref selectedWorkstationClass, value);
                if (SelectedWorkstationClass != null)
                {
                    TobeEditedItem = new WorkstationClass()
                    {
                        WC_name = SelectedWorkstationClass.WC_name,
                        WC_type = SelectedWorkstationClass.WC_type,
                        WC_handlingType = SelectedWorkstationClass.WC_handlingType,
                        ProcessRef = new Process(),                        
                        SecondaryactivityRef = new SecondaryActivity(),
                        EditprocessRef = SelectedWorkstationClass.ProcessRef,
                        EditsecondaryactivityRef = SelectedWorkstationClass.SecondaryactivityRef,
                    };
                    TobeEditedItem.ProcessRef.PC_name = SelectedWorkstationClass.ProcessRef.PC_name;
                    TobeEditedItem.SecondaryactivityRef.SC_name = SelectedWorkstationClass.SecondaryactivityRef.SC_name;
                    TobeEditedItem.EditprocessRef.editPC_name = SelectedWorkstationClass.ProcessRef.PC_name;
                    TobeEditedItem.EditsecondaryactivityRef.editSC_name = SelectedWorkstationClass.SecondaryactivityRef.SC_name;
                }
            }
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Update current selected item
        /// </summary>
        public void Update()
        {
            if (SelectedWorkstationClass != null && SelectedWorkstationClass != TobeEditedItem)
            {
                SelectedWorkstationClass.WC_name = TobeEditedItem.WC_name;
                SelectedWorkstationClass.WC_type = TobeEditedItem.WC_type;
                SelectedWorkstationClass.WC_handlingType = TobeEditedItem.WC_handlingType;
                SelectedWorkstationClass.ProcessRef.PC_name = TobeEditedItem.EditprocessRef.editPC_name;
                SelectedWorkstationClass.SecondaryactivityRef.SC_name = TobeEditedItem.EditsecondaryactivityRef.editSC_name;
            }
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            TobeEditedItem.ProcessRef.PC_name = TobeEditedItem.EditprocessRef.editPC_name;
            TobeEditedItem.SecondaryactivityRef.SC_name = TobeEditedItem.EditsecondaryactivityRef.editSC_name;
            ObservableWorkstationClass.Add(new WorkstationClass()
            {
                WC_name = this.TobeEditedItem.WC_name,
                WC_type = this.TobeEditedItem.WC_type,
                WC_handlingType = this.TobeEditedItem.WC_handlingType,
                ProcessRef = this.TobeEditedItem.ProcessRef,
                SecondaryactivityRef = this.TobeEditedItem.SecondaryactivityRef
            });
            TobeEditedItem = new WorkstationClass();
            SelectedWorkstationClass = ObservableWorkstationClass.ElementAt(ObservableWorkstationClass.Count - 1);
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            if (checkMatchedWorkstationClass())
            {
                MessageBox.Show("This Workstation class is currently attached to a Workstation. Please:" +
                    " \n\nRemove the Workstation in \"Workstations\" tab first" +
                    "\n..Or.." +
                    "\nChange the attached Wrokstation class to another one");
            }
            else
            {
                ObservableWorkstationClass.Remove(this.SelectedWorkstationClass);
                SelectedWorkstationClass = ObservableWorkstationClass.ElementAt(ObservableWorkstationClass.Count - 1);
            }
        }

        /// <summary>
        /// Return true if a matched WorkstationClass is found being used in a item of Workstation list
        /// </summary>
        /// <returns></returns>
        private bool checkMatchedWorkstationClass()
        {
            foreach (Workstation w in WorkstationsViewModel.ObservableWorkstation)
            {
                if (w.WorkstationclassRef.WC_name == SelectedWorkstationClass.WC_name && w.WorkstationclassRef.WC_handlingType == SelectedWorkstationClass.WC_handlingType && w.WorkstationclassRef.WC_type == SelectedWorkstationClass.WC_type)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
