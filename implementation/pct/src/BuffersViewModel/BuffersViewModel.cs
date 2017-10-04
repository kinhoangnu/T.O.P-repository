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
    class BuffersViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private Buffer selectedBuffer;
        private ObservableCollection<Buffer> _observableBuffer;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        #endregion

        #region Constructor
        public BuffersViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableBuffer = BufferList.GetBufferList();
        }
        #endregion

        #region properties
        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return BufferList.Buffers; }
            set { ChangeProperty(ref _observableBuffer, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public Buffer SelectedBuffer
        {
            get { return selectedBuffer; }
            set
            {
                ChangeProperty(ref selectedBuffer, value);
            }
        }
        #endregion

        #region Add, Update and Delete

        /// <summary>
        /// Add a new buffer to Buffer list
        /// </summary>
        public void Add()
        {
            this.ObservableBuffer.Add(new Buffer()
            {
                B_name = "",
                B_description = "",
                B_comID = "",
                B_unit = ""
            });
        }
        
        /// <summary>
        /// Delete current selected Buffer
        /// </summary>
        public void Delete()
        {
            if (checkMatchedBuffer() != null)
            {
                MessageBox.Show("This Buffer is currently attached to a Process ("+checkMatchedBuffer().PC_name+"). Please:" +
                       " \n\nRemove the Process in \"Processes\" tab first" +
                       "\n..Or.." +
                       "\nChange the attached buffer to another one");
            }
            else
            {
                this.ObservableBuffer.Remove(this.SelectedBuffer);
            }
        }

        /// <summary>
        /// Return true if a matched Buffer is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private Process checkMatchedBuffer()
        {
            foreach (Process p in ProcessesViewModel.ObservableProcess)
            {
                if ((p.InbufferRef.B_name == SelectedBuffer.B_name) ||
                    (p.OutbufferRef.B_name == SelectedBuffer.B_name))
                {                    
                    return p;
                }
            }
            return null;
        }

        #endregion


    }
}
