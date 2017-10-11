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

        private static ObservableCollection<WorkstationClass> _observableWorkstationClass;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand AddsCommand { get; set; }
        #endregion

        #region Constructor
        public WorkstationClassesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.AddsCommand = new RelayCommand((obj) => Adds());
            WorkstationClassList.WorkstationClasses = new ObservableCollection<WorkstationClass>();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();
            //this.SelectedWorkstationClass = ObservableWorkstationClass.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public static ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { _observableWorkstationClass = value; }
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
                ObservableWorkstationClass.Remove(this.SelectedWorkstationClass); 
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


        public void Adds()
        {
            SelectedWorkstationClass.Sclist = new ObservableCollection<ObservableCollection<SecondaryActivity>>();
            SelectedWorkstationClass.Sclist.Add(SelectedWorkstationClass.ObservableSecondaryActivity);
            
        }


        #endregion

    }
}
