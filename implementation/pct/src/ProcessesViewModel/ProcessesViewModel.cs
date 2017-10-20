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
    class ProcessesViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private Process selectedProcess;

        private static ObservableCollection<Process> observableProcess;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public ProcessesViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            ProcessList.Processes = new ObservableCollection<Process>();
            ObservableProcess = new ObservableCollection<Process>();
            SelectedProcess = new Process();
        }
        #endregion

        #region Properties
        public static ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { observableProcess = value; }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public Process SelectedProcess
        {
            get 
            { 

                return selectedProcess; 
            }
            set
            {                
                ChangeProperty(ref selectedProcess, value);
                if (SelectedProcess == null)
                {
                    SelectedProcess = new Process();
                }
                if (SelectedProcess != null)
                {
                    if (SelectedProcess.ObservableOutBuffer == null)
                    {
                        SelectedProcess.ObservableOutBuffer = BufferList.Buffers;
                    }
                }
            }
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            ObservableProcess.Add(new Process()
            {
                PcName = "",
                PcDescription = "",
                PcComId = "",
                ObservableOutBuffer = BufferList.Buffers
            });
            SelectedProcess = ObservableProcess.ElementAt(ObservableProcess.Count - 1);
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedProcess() != null)
            {
                MessageBox.Show("This Process is currently in attached to a Workstation Class ("+CheckMatchedProcess().WcName+"). Please:" +
                       " \n\nRemove the Workstation Class in \"WorkstationClasses\" tab first" +
                       "\n..Or.." +
                       "\nChange the attached process to another one");
            }
            else
            {
                ProcessList.Processes.Remove(SelectedProcess);
            }
        }

        /// <summary>
        /// Return true if a matched Process is found being used in a item of Workstation Class list
        /// </summary>
        /// <returns></returns>
        private WorkstationClass CheckMatchedProcess()
        {
            foreach (WorkstationClass wc in WorkstationClassesViewModel.ObservableWorkstationClass)
            {
                if (wc.ProcessRef.PcName == SelectedProcess.PcName)
                {
                    return wc;
                }
            }
            return null;
        }

        #endregion

    }
}
