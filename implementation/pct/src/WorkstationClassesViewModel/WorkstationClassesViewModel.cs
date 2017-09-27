using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Your
{
    public class WorkstationClassesViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private WorkstationClass selectedWorkstationClass;
        private WorkstationClass _tobeEditedItem;

        private ObservableCollection<WorkstationClass> _observableWorkstationClass;
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
            WClist = new WorkstationClassList();
            PClist = new ProcessList();
            SClist = new SecondaryActivity();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();            
            ObservableSecondaryActivity = new ObservableCollection<SecondaryActivity>();
            ObservableSecondaryActivity = SecondaryActivityList.GetSecondaryActivityList();
            ObservableProcess = new ObservableCollection<Process>();
            ObservableProcess = ProcessList.GetProcessList();
            ObservableWorkstationClass = WClist.GetWorkstationClassList();
            this.TobeEditedItem = ObservableWorkstationClass.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return _observableWorkstationClass; }
            set { ChangeProperty(ref _observableWorkstationClass, value); }
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
                selectedWorkstationClass = value;
                if (SelectedWorkstationClass != null)
                {
                    TobeEditedItem = new WorkstationClass()
                    {
                        WC_name = SelectedWorkstationClass.WC_name,
                        WC_type = SelectedWorkstationClass.WC_type,
                        WC_handlingType = SelectedWorkstationClass.WC_handlingType,
                        ProcessRef = SelectedWorkstationClass.ProcessRef,                        
                        SecondaryactivityRef = SelectedWorkstationClass.SecondaryactivityRef,
                        EditprocessRef = SelectedWorkstationClass.ProcessRef,
                        EditsecondaryactivityRef = SelectedWorkstationClass.SecondaryactivityRef,
                    };
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
            this.ObservableWorkstationClass.Add(new WorkstationClass()
            {
                WC_name = this.TobeEditedItem.WC_name,
                WC_type = this.TobeEditedItem.WC_type,
                WC_handlingType = this.TobeEditedItem.WC_handlingType,
                ProcessRef = this.TobeEditedItem.ProcessRef,
                SecondaryactivityRef = this.TobeEditedItem.SecondaryactivityRef
            });
            this.WClist.WorkstationClasses = this.ObservableWorkstationClass;
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            this.ObservableWorkstationClass.Remove(this.SelectedWorkstationClass);
        }
        #endregion

    }
}
