using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class ProductionAreasViewModel : ContentViewModel
    {
        private ProdArea selectedProdArea;
        private ObservableCollection<ProdArea> observableProdArea;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public ProdArea SelectedProdArea
        {
            get { return selectedProdArea; }
            set { ChangeProperty(ref selectedProdArea, value); }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return ProdAreaList.ProdAreas; }
            set { ChangeProperty(ref observableProdArea, value); }
        }

        public ProductionAreasViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            ProdAreaList.ProdAreas = new ObservableCollection<ProdArea>();
            ObservableProdArea = new ObservableCollection<ProdArea>();
        }

        /// <summary>
        /// Add a new item with properties on the input controls
        /// </summary>
        public void Add()
        {
            ObservableProdArea.Add(new ProdArea
            {
                PName = SelectedProdArea.PName,
                PDescription = SelectedProdArea.PDescription,
                PComId = SelectedProdArea.PComId,
                PType = SelectedProdArea.PType
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
                SelectedProdArea.PName = SelectedProdArea.PName;
                SelectedProdArea.PDescription = SelectedProdArea.PDescription;
                SelectedProdArea.PComId = SelectedProdArea.PComId;
                SelectedProdArea.PType = SelectedProdArea.PType;
            }
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedProdArea() != null)
            {
                MessageBox.Show("This Production Area is currently attached to a Process (" +
                                CheckMatchedProdArea().PcName + "). Please:" +
                                " \n\nRemove the Process in \"Processes\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached Production area to another one");
            }
            else
            {
                ObservableProdArea.Remove(SelectedProdArea);
            }
        }

        /// <summary>
        /// Return true if a matched Production Area is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private Process CheckMatchedProdArea()
        {
            foreach (var p in ProcessesViewModel.ObservableProcess)
            {
                if (p.ProdRef.Uuid == SelectedProdArea.Uuid)
                {
                    return p;
                }
            }
            return null;
        }
    }
}