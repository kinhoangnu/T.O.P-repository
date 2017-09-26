using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class ProcessManagerViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private Process selectedProcess;

        private ObservableCollection<Process> _observableProcess;
        private ObservableCollection<Buffer> _observableBuffer;
        private ObservableCollection<ProdArea> _observableProdArea;
        private Process _tobeEditedItem;

        public ProcessList PClist;
        public BufferList Blist;
        public ProdAreaList Plist;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public ProcessManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            PClist = new ProcessList();
            Blist = new BufferList();
            Plist = new ProdAreaList();
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableProcess = new ObservableCollection<Process>();
            ObservableProdArea = new ObservableCollection<ProdArea>();
            ObservableProdArea = ProdAreaList.GetProdAreaList();
            ObservableBuffer = BufferList.GetBufferList();
            ObservableProcess = PClist.Processes;
            this.TobeEditedItem = ObservableProcess.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public ObservableCollection<Process> ObservableProcess
        {
            get { return _observableProcess; }
            set { ChangeProperty(ref _observableProcess, value); }
        }

        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return BufferList.Buffers; }
            set { _observableBuffer = value; }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return ProdAreaList.ProdAreas; }
            set { ChangeProperty(ref _observableProdArea, value); }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public Process TobeEditedItem
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
        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set
            {
                selectedProcess = value;
                if (SelectedProcess != null)
                {
                    TobeEditedItem = new Process()
                    {
                        PC_objName = SelectedProcess.PC_objName,
                        PC_description = SelectedProcess.PC_description,
                        PC_ComID = SelectedProcess.PC_ComID,
                        ProdRef = SelectedProcess.ProdRef,
                        EditProdRef = SelectedProcess.ProdRef,
                        EditInbufferRef = SelectedProcess.InbufferRef,
                        EditOutbufferRef = SelectedProcess.OutbufferRef,
                        IsReplenished = SelectedProcess.IsReplenished,
                        ExclFromKPI = SelectedProcess.ExclFromKPI
                    };
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
            if (SelectedProcess != null && SelectedProcess != TobeEditedItem)
            {
                SelectedProcess.PC_objName = TobeEditedItem.PC_objName;
                SelectedProcess.PC_description = TobeEditedItem.PC_description;
                SelectedProcess.PC_ComID = TobeEditedItem.PC_ComID;
                SelectedProcess.ProdRef.P_objName = TobeEditedItem.EditProdRef.editP_objName;
                SelectedProcess.InbufferRef.B_objName = TobeEditedItem.EditInbufferRef.editB_objName;
                SelectedProcess.OutbufferRef.B_objName = TobeEditedItem.EditOutbufferRef.editB_objName;
                SelectedProcess.IsReplenished = TobeEditedItem.IsReplenished;
                SelectedProcess.ExclFromKPI = TobeEditedItem.ExclFromKPI;
            }
            SelectedProcess = null;
            TobeEditedItem = null;
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            TobeEditedItem.EditInbufferRef.B_objName = TobeEditedItem.EditInbufferRef.editB_objName;
            this.ObservableProcess.Add(new Process()
            {
                PC_objName = this.TobeEditedItem.PC_objName,
                PC_description = this.TobeEditedItem.PC_description,
                PC_ComID = this.TobeEditedItem.PC_ComID,
                ProdRef = this.TobeEditedItem.EditProdRef,
                InbufferRef = this.TobeEditedItem.EditInbufferRef,
                OutbufferRef = this.TobeEditedItem.EditOutbufferRef,
                IsReplenished = this.TobeEditedItem.IsReplenished,
                ExclFromKPI = this.TobeEditedItem.ExclFromKPI
            });
            this.PClist.Processes = this.ObservableProcess;
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {            
            this.ObservableProcess.Remove(this.SelectedProcess);
        }
        #endregion

    }
}
