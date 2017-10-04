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
    class ProductionAreasViewModel : ContentViewModel
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
        public ProductionAreasViewModel()
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
                ChangeProperty(ref selectedProdArea, value);
                if (SelectedProdArea != null)
                {
                    TobeEditedItem = new ProdArea()
                    {
                        P_name = SelectedProdArea.P_name,
                        P_comID = SelectedProdArea.P_comID,
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
            get { return ProdAreaList.ProdAreas; }
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
                P_name = this.TobeEditedItem.P_name,
                P_description = this.TobeEditedItem.P_description,
                P_comID = this.TobeEditedItem.P_comID,
                P_Type = this.TobeEditedItem.P_Type
            });
            ProdAreaList.ProdAreas = ObservableProdArea;
            SelectedProdArea = ObservableProdArea.ElementAt(ObservableProdArea.Count - 1);
        }

        /// <summary>
        /// Update the current selected item on the list
        /// </summary>
        public void Update()
        {
            if (SelectedProdArea != null)
            {
                SelectedProdArea.P_name = TobeEditedItem.P_name;
                SelectedProdArea.P_description = TobeEditedItem.P_description;
                SelectedProdArea.P_comID = TobeEditedItem.P_comID;
                SelectedProdArea.P_Type = TobeEditedItem.P_Type;
            }
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            if (checkMatchedProdArea() != null)
            {
                MessageBox.Show("This Production Area is currently attached to a Process ("+checkMatchedProdArea().PC_name+"). Please:" +
                    " \n\nRemove the Process in \"Processes\" tab first" +
                    "\n..Or.." +
                    "\nChange the attached Production area to another one");
            }
            else
            {
                this.ObservableProdArea.Remove(this.SelectedProdArea);
            }
        }

        /// <summary>
        /// Return true if a matched Production Area is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private Process checkMatchedProdArea()
        {
            foreach (Process p in ProcessesViewModel.ObservableProcess)
            {
                if (p.ProdRef.P_name == SelectedProdArea.P_name) 
                {
                    return p;
                }
            }
            return null;
        }
        #endregion


    }
}
