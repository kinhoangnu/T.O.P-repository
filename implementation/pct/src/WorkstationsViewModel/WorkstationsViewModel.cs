﻿using System;
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

        private static ObservableCollection<Workstation> observableWorkstation;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public WorkstationsViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            WorkstationList.Workstations = new ObservableCollection<Workstation>();
            ObservableWorkstation = WorkstationList.GetWorkstationList();
        }
        #endregion

        #region Properties
        public static ObservableCollection<Workstation> ObservableWorkstation
        {
            get { return WorkstationList.Workstations; }
            set { observableWorkstation = value; }
        }


        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public Workstation SelectedWorkstation
        {
            get { return selectedWorkstation; }
            set
            {
                ChangeProperty(ref selectedWorkstation, value);
            }
        }
        #endregion

        #region Methods


        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            ObservableWorkstation.Add(new Workstation()
            {
                WName = "",
                WDescription = "",
                WComId = ""
            });
            SelectedWorkstation = ObservableWorkstation.ElementAt(ObservableWorkstation.Count - 1);
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {        
            ObservableWorkstation.Remove(this.SelectedWorkstation);
        }
        #endregion

    }
}
