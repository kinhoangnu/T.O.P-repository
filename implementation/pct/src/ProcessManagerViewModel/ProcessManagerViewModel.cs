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
        #region Fields
        private Process selectedProcess;

        private ObservableCollection<Process> _observableProcess;
        private ObservableCollection<Buffer> _observableBuffer;
        private ObservableCollection<ProdArea> _observableProdArea;

        public ProcessList PClist;
        public BufferList Blist;
        public ProdAreaList Plist;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        #endregion

        #region Constructor
        public ProcessManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            PClist = new ProcessList();
            Blist = new BufferList();
            Plist = new ProdAreaList();
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableProcess = new ObservableCollection<Process>();
            ObservableProdArea = new ObservableCollection<ProdArea>();
            ObservableProdArea = Plist.ProdAreas;
            ObservableBuffer = Blist.GetBufferList();
            ObservableProcess = PClist.Processes;
            this.SelectedProcess = ObservableProcess.FirstOrDefault();
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
            get { return _observableBuffer; }
            set { ChangeProperty(ref _observableBuffer, value); }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return _observableProdArea; }
            set { ChangeProperty(ref _observableProdArea, value); }
        }
        #endregion

        #region Methods
        
        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set { ChangeProperty(ref selectedProcess, value); }
        }

        public void Add()
        {
            this.ObservableProcess.Add(new Process()
            {
                PC_objName = this.SelectedProcess.PC_objName,
                PC_description = this.SelectedProcess.PC_description,
                PC_ComID = this.SelectedProcess.PC_ComID,
                ProdRef = this.SelectedProcess.ProdRef,
                InbufferRef = this.SelectedProcess.InbufferRef,
                OutbufferRef = this.SelectedProcess.OutbufferRef,
                IsReplenished = this.SelectedProcess.IsReplenished,
                ExclFromKPI = this.SelectedProcess.ExclFromKPI
            });
            this.PClist.Processes = this.ObservableProcess;
        }

        public void Delete()
        {
            this.ObservableProcess.Remove(this.selectedProcess);
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }       

        #endregion      

    }
}
