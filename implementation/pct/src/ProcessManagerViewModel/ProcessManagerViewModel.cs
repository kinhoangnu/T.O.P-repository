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
            Plist = new ProdAreaList();
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableProcess = new ObservableCollection<Process>();
            ObservableProdArea = new ObservableCollection<ProdArea>();
            ObservableProdArea = ProdAreaList.GetProdAreaList();
            ObservableBuffer = BufferList.GetBufferList();
            ObservableProcess = ProcessList.GetProcessList();
            this.SelectedProcess = ObservableProcess.FirstOrDefault(); 
        }
        #endregion

        #region Properties
        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
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
                        PC_name = SelectedProcess.PC_name,
                        PC_description = SelectedProcess.PC_description,
                        PC_comID = SelectedProcess.PC_comID,
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
                SelectedProcess.PC_name = TobeEditedItem.PC_name;
                SelectedProcess.PC_description = TobeEditedItem.PC_description;
                SelectedProcess.PC_comID = TobeEditedItem.PC_comID;
                SelectedProcess.ProdRef.P_name = TobeEditedItem.EditProdRef.editP_name;
                SelectedProcess.InbufferRef.B_name = TobeEditedItem.EditInbufferRef.editB_name;
                SelectedProcess.OutbufferRef.B_name = TobeEditedItem.EditOutbufferRef.editB_name;
                SelectedProcess.IsReplenished = TobeEditedItem.IsReplenished;
                SelectedProcess.ExclFromKPI = TobeEditedItem.ExclFromKPI;
            }
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            TobeEditedItem.EditInbufferRef.B_name = TobeEditedItem.EditInbufferRef.editB_name;
            this.ObservableProcess.Add(new Process()
            {
                PC_name = this.TobeEditedItem.PC_name,
                PC_description = this.TobeEditedItem.PC_description,
                PC_comID = this.TobeEditedItem.PC_comID,
                ProdRef = this.TobeEditedItem.EditProdRef,
                InbufferRef = this.TobeEditedItem.EditInbufferRef,
                OutbufferRef = this.TobeEditedItem.EditOutbufferRef,
                IsReplenished = this.TobeEditedItem.IsReplenished,
                ExclFromKPI = this.TobeEditedItem.ExclFromKPI
            });
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            Process temp = new Process();
            temp = SelectedProcess;
            this.ObservableProcess.Remove(this.SelectedProcess);
            PClist.DeleteAProcess(temp);
        }
        #endregion

    }
}
