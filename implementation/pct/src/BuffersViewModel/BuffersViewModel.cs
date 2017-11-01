using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class BuffersViewModel : ContentViewModel
    {
        private Buffer selectedBuffer;
        private ObservableCollection<Buffer> observableBuffer;

        private Validation validation = new Validation();

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return BufferList.Buffers; }
            set { ChangeProperty(ref observableBuffer, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public Buffer SelectedBuffer
        {
            get { return selectedBuffer; }
            set { ChangeProperty(ref selectedBuffer, value); }
        }

        public BuffersViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            BufferList.Buffers = new ObservableCollection<Buffer>();
            ObservableBuffer = new ObservableCollection<Buffer>();
        }

        /// <summary>
        /// Add a new buffer to Buffer list
        /// </summary>
        public void Add()
        {
            ObservableBuffer.Add(new Buffer
            {
                BName = "",
                BDescription = "",
                BComId = "",
                BUnit = ""
            });
        }

        /// <summary>
        /// Delete current selected Buffer
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedBuffer() != null)
            {
                MessageBox.Show("This Buffer is currently attached to a Process (" + CheckMatchedBuffer().PcName +
                                "). Please:" +
                                " \n\nRemove the Process in \"Processes\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached buffer to another one");
            }
            else
            {
                ObservableBuffer.Remove(SelectedBuffer);
            }
        }

        /// <summary>
        /// Return true if a matched Buffer is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private Process CheckMatchedBuffer()
        {
            return ProcessList.Processes.FirstOrDefault(process => process.InbufferRef.BName == SelectedBuffer.BName);
        }
    }
}