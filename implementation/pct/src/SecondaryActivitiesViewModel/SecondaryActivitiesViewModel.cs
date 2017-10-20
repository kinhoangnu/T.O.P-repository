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
using System.Windows.Forms;

namespace Your
{
    class SecondaryActivitiesViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private SecondaryActivity selectedSecondaryActivity;
        private ObservableCollection<SecondaryActivity> observableSecondaryActivity;

        public SecondaryActivityList SClist;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        #endregion

        #region Constructor
        public SecondaryActivitiesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            SecondaryActivityList.SecondaryActivities = new ObservableCollection<SecondaryActivity>();
            ObservableSecondaryActivity = new ObservableCollection<SecondaryActivity>();
        }
        #endregion

        #region properties
        public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        {
            get { return SecondaryActivityList.SecondaryActivities; }
            set { ChangeProperty(ref observableSecondaryActivity, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public SecondaryActivity SelectedSecondaryActivity
        {
            get { return selectedSecondaryActivity; }
            set
            {
                ChangeProperty(ref selectedSecondaryActivity, value);
            }
        }
        #endregion

        #region Add, Update and Delete

        /// <summary>
        /// Add a new buffer to SelectedSecondaryActivity list
        /// </summary>
        public void Add()
        {
            this.ObservableSecondaryActivity.Add(new SecondaryActivity()
            {
                ScName = "",
                ScDescription = "",
                ScComId = ""
            });
        }

        /// <summary>
        /// Delete current selected SelectedSecondaryActivity
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedSecondaryActivity() != null)
            {
                MessageBox.Show("This Secondary Activity is currently attached to a Workstation Class ("+CheckMatchedSecondaryActivity().WcName+"). Please:"+
                    " \n\nRemove the Workstation Class in \"WorkstationClasses\" tab first"+
                    "\n..Or.."+
                    "\nChange the attached activity to another one");
            }
            else
            {
                this.ObservableSecondaryActivity.Remove(this.SelectedSecondaryActivity);
            }
        }

        /// <summary>
        /// Return true if a matched Secondary Activity is found being used in a item of WorkstationClass list
        /// </summary>
        /// <returns></returns>
        private WorkstationClass CheckMatchedSecondaryActivity()
        {
            foreach (WorkstationClass wc in WorkstationClassesViewModel.ObservableWorkstationClass)
            {
                //if (wc.SecondaryactivityRef.SC_name == SelectedSecondaryActivity.SC_name)
                //{
                //    return wc;
                //}
            }
            return null;
        }

        #endregion


    }
}
