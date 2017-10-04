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
    class BufferManagerViewModel : ContentViewModel
    {
        private Buffer selectedBuffer;

        private ObservableCollection<Buffer> _observableBuffer;
        public BufferList Blist;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }


        public BufferManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            Blist = new BufferList();
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableBuffer = Blist.GetBufferList();
            this.SelectedBuffer = ObservableBuffer.FirstOrDefault();
        }

        public void Add()
        {
            this.ObservableBuffer.Add(new Buffer() { B_objName = this.SelectedBuffer.B_objName, B_description = this.SelectedBuffer.B_description, B_ComID = this.SelectedBuffer.B_ComID, B_Unit = this.SelectedBuffer.B_Unit });
            this.Blist.Buffers = ObservableBuffer;
        }

        public void Update()
        {
            foreach (Buffer b in ObservableBuffer)
            {
                if (b == SelectedBuffer)
                {
                    b.B_ComID = this.SelectedBuffer.B_ComID;
                    b.B_description = this.SelectedBuffer.B_description;
                    b.B_objName = this.SelectedBuffer.B_objName;
                    b.B_Unit = this.SelectedBuffer.B_Unit;
                }
            }
            Blist.Buffers = ObservableBuffer;
        }

        public void Delete()
        {
            this.ObservableBuffer.Remove(this.SelectedBuffer);
            Blist.RemoveABuffer(this.SelectedBuffer);
        }

        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return _observableBuffer; }
            set { ChangeProperty(ref _observableBuffer, value); }
        }

        public Buffer SelectedBuffer
        {
            get { return selectedBuffer; }
            set { ChangeProperty(ref selectedBuffer, value); }
        }
    }
}
