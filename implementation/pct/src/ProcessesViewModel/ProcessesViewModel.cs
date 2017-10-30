using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class ProcessesViewModel : ContentViewModel
    {
        private static ObservableCollection<Process> observableProcess;
        private Process selectedProcess;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; private set; }

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
            get { return selectedProcess; }
            set
            {
                ChangeProperty(ref selectedProcess, value);
                if (SelectedProcess == null)
                {
                    SelectedProcess = new Process();
                }
                if (SelectedProcess == null)
                {
                    return;
                }
                if (SelectedProcess.ObservableOutBuffer == null)
                {
                    SelectedProcess.ObservableOutBuffer = BufferList.Buffers;
                }
            }
        }

        public ProcessesViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            ProcessList.Processes = new ObservableCollection<Process>();
            ObservableProcess = new ObservableCollection<Process>();
            SelectedProcess = new Process();
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            ObservableProcess.Add(new Process
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
                MessageBox.Show("This Process is currently in attached to a Workstation Class (" +
                                CheckMatchedProcess().WcName + "). Please:" +
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
            return
                WorkstationClassesViewModel.ObservableWorkstationClass.FirstOrDefault(
                    wc => wc.ProcessRef.PcName == SelectedProcess.PcName);
        }
    }
}