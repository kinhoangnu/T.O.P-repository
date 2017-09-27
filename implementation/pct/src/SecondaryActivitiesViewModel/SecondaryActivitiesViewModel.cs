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
    class SecondaryActivitiesViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private SecondaryActivity selectedSecondaryActivity;
        private ObservableCollection<SecondaryActivity> _observableSecondaryActivity;
        private SecondaryActivity _tobeEditedItem;

        public SecondaryActivityList SClist;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        #endregion

        #region Constructor
        public SecondaryActivitiesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            SClist = new SecondaryActivityList();
            ObservableSecondaryActivity = new ObservableCollection<SecondaryActivity>();
            ObservableSecondaryActivity = SecondaryActivityList.GetSecondaryActivityList();
            this.SelectedSecondaryActivity = ObservableSecondaryActivity.FirstOrDefault();
        }
        #endregion

        #region properties
        public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        {
            get { return SecondaryActivityList.SecondaryActivities; }
            set { ChangeProperty(ref _observableSecondaryActivity, value); }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public SecondaryActivity TobeEditedItem
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
        public SecondaryActivity SelectedSecondaryActivity
        {
            get { return selectedSecondaryActivity; }
            set
            {
                selectedSecondaryActivity = value;
                if (SelectedSecondaryActivity != null)
                {
                    TobeEditedItem = new SecondaryActivity()
                    {
                        SC_name = SelectedSecondaryActivity.SC_name,
                        SC_comID = SelectedSecondaryActivity.SC_comID,
                        SC_description = SelectedSecondaryActivity.SC_description,
                    };
                }
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
                SC_name = this.TobeEditedItem.SC_name,
                SC_description = this.TobeEditedItem.SC_description,
                SC_comID = this.TobeEditedItem.SC_comID
            });
        }

        /// <summary>
        /// Update current selected SelectedSecondaryActivity
        /// </summary>
        public void Update()
        {
            if (SelectedSecondaryActivity != null)
            {
                SelectedSecondaryActivity.SC_name = TobeEditedItem.SC_name;
                SelectedSecondaryActivity.SC_description = TobeEditedItem.SC_description;
                SelectedSecondaryActivity.SC_comID = TobeEditedItem.SC_comID;
            }
        }

        /// <summary>
        /// Delete current selected SelectedSecondaryActivity
        /// </summary>
        public void Delete()
        {
            SecondaryActivity temp = new SecondaryActivity();
            temp = SelectedSecondaryActivity;
            this.ObservableSecondaryActivity.Remove(this.SelectedSecondaryActivity);
            SClist.DeleteASecondaryActivity(temp);
        }

        #endregion


    }
}
