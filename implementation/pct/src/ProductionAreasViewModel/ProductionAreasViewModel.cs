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
        private ObservableCollection<ProdArea> observableProdArea;
        private ProdArea tobeEditedItem;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        #endregion

        #region contructor
        public ProductionAreasViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            ProdAreaList.ProdAreas = new ObservableCollection<ProdArea>();
            ObservableProdArea = new ObservableCollection<ProdArea>();
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
                        PName = SelectedProdArea.PName,
                        PComId = SelectedProdArea.PComId,
                        PDescription = SelectedProdArea.PDescription,
                        PType = SelectedProdArea.PType,
                    };
                }
            }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public ProdArea TobeEditedItem
        {
            get { return tobeEditedItem; }
            set
            {
                ChangeProperty(ref tobeEditedItem, value);
            }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return ProdAreaList.ProdAreas; }
            set { ChangeProperty(ref observableProdArea, value); }
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
                PName = this.TobeEditedItem.PName,
                PDescription = this.TobeEditedItem.PDescription,
                PComId = this.TobeEditedItem.PComId,
                PType = this.TobeEditedItem.PType
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
                SelectedProdArea.PName = TobeEditedItem.PName;
                SelectedProdArea.PDescription = TobeEditedItem.PDescription;
                SelectedProdArea.PComId = TobeEditedItem.PComId;
                SelectedProdArea.PType = TobeEditedItem.PType;
            }
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedProdArea() != null)
            {
                MessageBox.Show("This Production Area is currently attached to a Process ("+CheckMatchedProdArea().PcName+"). Please:" +
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
        private Process CheckMatchedProdArea()
        {
            foreach (Process p in ProcessesViewModel.ObservableProcess)
            {
                if (p.ProdRef.PName == SelectedProdArea.PName) 
                {
                    return p;
                }
            }
            return null;
        }
        #endregion


    }
}
