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
    class ProdAreaManagerViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private ProdArea selectedProdArea;
        private ObservableCollection<ProdArea> _observableProdArea;
        private ProdArea _tobeEditedItem;

        public ProdAreaList Plist;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        #endregion

        #region contructor
        public ProdAreaManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            Plist = new ProdAreaList();
            ObservableProdArea = new ObservableCollection<ProdArea>();
            ObservableProdArea = ProdAreaList.GetProdAreaList();
            this.SelectedProdArea = ObservableProdArea.FirstOrDefault();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Item that is being selected on the list 
        /// </summary>
        public ProdArea SelectedProdArea
        {
            get { return selectedProdArea; }
            set
            {
                selectedProdArea = value;
                if (SelectedProdArea != null)
                {
                    TobeEditedItem = new ProdArea()
                    {
                        P_objName = SelectedProdArea.P_objName,
                        P_ComID = SelectedProdArea.P_ComID,
                        P_description = SelectedProdArea.P_description,
                        P_Type = SelectedProdArea.P_Type,
                    };
                }
            }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public ProdArea TobeEditedItem
        {
            get { return _tobeEditedItem; }
            set
            {
                ChangeProperty(ref _tobeEditedItem, value);
            }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return _observableProdArea; }
            set { ChangeProperty(ref _observableProdArea, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a new item with properties on the input controls
        /// </summary>
        public void Add()
        {
            this.ObservableProdArea.Add(new ProdArea()
            {
                P_objName = this.TobeEditedItem.P_objName,
                P_description = this.TobeEditedItem.P_description,
                P_ComID = this.TobeEditedItem.P_ComID,
                P_Type = this.TobeEditedItem.P_Type
            });
            ProdAreaList.ProdAreas = ObservableProdArea;
        }

        /// <summary>
        /// Update the current selected item on the list
        /// </summary>
        public void Update()
        {
            if (SelectedProdArea != null)
            {
                SelectedProdArea.P_objName = TobeEditedItem.P_objName;
                SelectedProdArea.P_description = TobeEditedItem.P_description;
                SelectedProdArea.P_ComID = TobeEditedItem.P_ComID;
                SelectedProdArea.P_Type = TobeEditedItem.P_Type;
            }
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            this.ObservableProdArea.Remove(this.SelectedProdArea);
        }
        #endregion


    }
}
