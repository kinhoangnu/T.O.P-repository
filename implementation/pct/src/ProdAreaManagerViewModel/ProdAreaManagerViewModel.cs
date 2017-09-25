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
        private ProdArea selectedProdArea;
        
        private ObservableCollection<ProdArea> _observableProdArea;
        public ProdAreaList Plist;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }


        public ProdArea SelectedProdArea
        {
            get { return selectedProdArea; }
            set { ChangeProperty(ref selectedProdArea, value); }
        }

        public ProdAreaManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            Plist = new ProdAreaList();
            ObservableProdArea = new ObservableCollection<ProdArea>();
            ObservableProdArea = Plist.ProdAreas;
            this.SelectedProdArea = ObservableProdArea.FirstOrDefault();
        }

        public void Add()
        {
            this.ObservableProdArea.Add(new ProdArea() { P_objName = this.SelectedProdArea.P_objName, P_description = this.SelectedProdArea.P_description, P_ComID = this.SelectedProdArea.P_ComID, P_Type = this.SelectedProdArea.P_Type });
            this.Plist.ProdAreas = ObservableProdArea;
        }

        public void Update()
        {
            foreach (ProdArea p in ObservableProdArea)
            {
                if (p == SelectedProdArea)
                {
                    p.P_ComID = this.SelectedProdArea.P_ComID;
                    p.P_description = this.SelectedProdArea.P_description;
                    p.P_objName = this.SelectedProdArea.P_objName;
                    p.P_Type = this.SelectedProdArea.P_Type;
                }
            }
            Plist.ProdAreas = ObservableProdArea;
        }

        public void Delete()
        {
            ProdArea temp = SelectedProdArea;
            this.ObservableProdArea.Remove(this.SelectedProdArea);
            Plist.ProdAreas.Remove(this.SelectedProdArea);
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return _observableProdArea; }
            set { ChangeProperty(ref _observableProdArea, value); }
        }

    }
}
