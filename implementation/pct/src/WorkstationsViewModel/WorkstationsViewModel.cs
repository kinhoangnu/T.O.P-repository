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
    public class WorkstationsViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private Workstation selectedWorkstation;
        private Workstation _tobeEditedItem;

        private ObservableCollection<Workstation> _observableWorkstation;
        private ObservableCollection<WorkstationClass> _observableWorkstationClass;
        
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public WorkstationsViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            ObservableWorkstation = new ObservableCollection<Workstation>();
            ObservableWorkstation = WorkstationList.GetWorkstationList();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();
            ObservableWorkstationClass = WorkstationClassList.GetWorkstationClassList();
            this.SelectedWorkstation = ObservableWorkstation.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public ObservableCollection<Workstation> ObservableWorkstation
        {
            get { return WorkstationList.Workstations; }
            set { ChangeProperty(ref _observableWorkstation, value); }
        }

        public ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { ChangeProperty(ref _observableWorkstationClass, value); }
        }
        

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public Workstation TobeEditedItem
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
        public Workstation SelectedWorkstation
        {
            get { return selectedWorkstation; }
            set
            {
                selectedWorkstation = value;
                if (SelectedWorkstation != null)
                {
                    TobeEditedItem = new Workstation()
                    {
                        W_name = SelectedWorkstation.W_name,
                        W_description = SelectedWorkstation.W_description,
                        W_comID = SelectedWorkstation.W_comID,
                        EditworkstationClassRef = SelectedWorkstation.WorkstationclassRef,
                        WorkstationclassRef = SelectedWorkstation.WorkstationclassRef,
                    };
                    TobeEditedItem.EditworkstationClassRef.editWC_name = SelectedWorkstation.WorkstationclassRef.WC_name;
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
            if (SelectedWorkstation != null && SelectedWorkstation != TobeEditedItem)
            {
                SelectedWorkstation.W_name = TobeEditedItem.W_name;
                SelectedWorkstation.W_description = TobeEditedItem.W_description;
                SelectedWorkstation.W_comID = TobeEditedItem.W_comID;
                SelectedWorkstation.WorkstationclassRef.WC_name = TobeEditedItem.EditworkstationClassRef.editWC_name;
            }
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            this.ObservableWorkstation.Add(new Workstation()
            {
                W_name = this.TobeEditedItem.W_name,
                W_description = this.TobeEditedItem.W_description,
                W_comID = this.TobeEditedItem.W_comID,
                WorkstationclassRef = this.TobeEditedItem.WorkstationclassRef
            });
            WorkstationList.Workstations = this.ObservableWorkstation;
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            this.ObservableWorkstation.Remove(this.SelectedWorkstation);
        }
        #endregion

    }
}
