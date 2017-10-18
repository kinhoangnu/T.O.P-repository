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
        private static WorkstationClass selectedWorkstationClass;

        private static ObservableCollection<ObservableCollection<SecondaryActivity>> _slist;

        private static ObservableCollection<WorkstationClass> _observableWorkstationClass;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        #endregion

        #region Constructor
        public WorkstationClassesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            WorkstationClassList.WorkstationClasses = new ObservableCollection<WorkstationClass>();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();
            SelectedWorkstationClass = new WorkstationClass();
        }
        #endregion

        #region Properties
        public static ObservableCollection<ObservableCollection<SecondaryActivity>> Slist
        {
            get { return _slist; }
            set { _slist = value; }
        }

        public static ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { _observableWorkstationClass = value; }
        }


        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public static WorkstationClass SelectedWorkstationClass
        {
            get 
            {
                return selectedWorkstationClass;
            }
            set
            {                
                //ChangeProperty(ref selectedWorkstationClass, value);
                selectedWorkstationClass = value;
                if (SelectedWorkstationClass == null)
                {
                    SelectedWorkstationClass = new WorkstationClass();
                } 
                if (SelectedWorkstationClass != null)
                {
                    if (SelectedWorkstationClass.SecondaryactivityRef == null)
                    {
                        SelectedWorkstationClass.SecondaryactivityRef = SecondaryActivityList.SecondaryActivities;
                    }
                }
            }
        }
        #endregion

        #region Methods  
        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            ObservableWorkstationClass.Add(new WorkstationClass()
            {
                WC_name = "",
                WC_type = "",
                WC_handlingType = "",
                SecondaryactivityRef = SecondaryActivityList.SecondaryActivities
            });
            SelectedWorkstationClass = ObservableWorkstationClass.ElementAt(ObservableWorkstationClass.Count - 1);
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            if (checkMatchedWorkstationClass() != null)
            {
                MessageBox.Show("This Workstation class is currently attached to a Workstation ("+checkMatchedWorkstationClass().W_name+"). Please:" +
                    " \n\nRemove the Workstation in \"Workstations\" tab first" +
                    "\n..Or.." +
                    "\nChange the attached Wrokstation class to another one");
            }
            else
            {
                ObservableWorkstationClass.Remove(SelectedWorkstationClass); 
            }
        }

        /// <summary>
        /// Return true if a matched WorkstationClass is found being used in a item of Workstation list
        /// </summary>
        /// <returns></returns>
        private Workstation checkMatchedWorkstationClass()
        {
            foreach (Workstation w in WorkstationsViewModel.ObservableWorkstation)
            {
                if (w.WorkstationclassRef.WC_name == SelectedWorkstationClass.WC_name)
                {
                    return w;
                }
            }
            return null;
        }



        #endregion

    }
}
