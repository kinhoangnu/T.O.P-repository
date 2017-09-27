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
    class WorkstationGroupViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private WorkstationGroup selectedWorkstationGroup;
        private ObservableCollection<WorkstationGroup> _observableWorkstationGroup;
        private WorkstationGroup _tobeEditedItem;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        #endregion

        #region contructor
        public WorkstationGroupViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            ObservableWorkstationGroup = new ObservableCollection<WorkstationGroup>();
            ObservableWorkstationGroup = WorkstationGroupList.GetWorkstationGroupList();
            this.SelectedWorkstationGroup = ObservableWorkstationGroup.FirstOrDefault();
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
                selectedWorkstationGroup = value;
                if (SelectedWorkstationGroup != null)
                {
                    TobeEditedItem = new WorkstationGroup()
                    {
                        WG_name = SelectedWorkstationGroup.WG_name,
                        WG_description = SelectedWorkstationGroup.WG_description,
                    };
                }
            }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public WorkstationGroup TobeEditedItem
        {
            get { return _tobeEditedItem; }
            set
            {
                ChangeProperty(ref _tobeEditedItem, value);
            }
        }

        public ObservableCollection<WorkstationGroup> ObservableWorkstationGroup
        {
            get { return WorkstationGroupList.WorkstationGroups; }
            set { ChangeProperty(ref _observableWorkstationGroup, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new item with properties on the input controls
        /// </summary>
        public void Add()
        {
            this.ObservableWorkstationGroup.Add(new WorkstationGroup()
            {
                WG_name = this.TobeEditedItem.WG_name,
                WG_description = this.TobeEditedItem.WG_description,
            });
            WorkstationGroupList.WorkstationGroups = ObservableWorkstationGroup;
        }

        /// <summary>
        /// Update the current selected item on the list
        /// </summary>
        public void Update()
        {
            if (SelectedWorkstationGroup != null)
            {
                SelectedWorkstationGroup.WG_name = TobeEditedItem.WG_name;
                SelectedWorkstationGroup.WG_description = TobeEditedItem.WG_description;
            }
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            this.ObservableWorkstationGroup.Remove(this.SelectedWorkstationGroup);
        }
        #endregion


    }
}
