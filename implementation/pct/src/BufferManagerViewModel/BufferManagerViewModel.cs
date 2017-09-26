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
        #region Fields and auto-implement properties
        private Buffer selectedBuffer;
        private ObservableCollection<Buffer> _observableBuffer;
        private Buffer _tobeEditedItem;

        public BufferList Blist;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }

        #endregion

        #region Constructor
        public BufferManagerViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            this.UpdateCommand = new RelayCommand((obj) => Update());
            Blist = new BufferList();
            ObservableBuffer = new ObservableCollection<Buffer>();
            ObservableBuffer = BufferList.GetBufferList();
            this.SelectedBuffer = ObservableBuffer.FirstOrDefault();
        }
        #endregion

        #region properties
        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return _observableBuffer; }
            set { ChangeProperty(ref _observableBuffer, value); }
        }

        /// <summary>
        /// Item that is being filled on the input controls
        /// </summary>
        public Buffer TobeEditedItem
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
        public Buffer SelectedBuffer
        {
            get { return selectedBuffer; }
            set
            {
                selectedBuffer = value;
                if (SelectedBuffer != null)
                {
                    TobeEditedItem = new Buffer()
                    {
                        B_name = SelectedBuffer.B_name,
                        B_comID = SelectedBuffer.B_comID,
                        B_description = SelectedBuffer.B_description,
                        B_unit = SelectedBuffer.B_unit,
                    };
                }
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
                B_name = this.TobeEditedItem.B_name,
                B_description = this.TobeEditedItem.B_description,
                B_comID = this.TobeEditedItem.B_comID,
                B_unit = this.TobeEditedItem.B_unit
            });
        }

        /// <summary>
        /// Update current selected Buffer
        /// </summary>
        public void Update()
        {
            if (SelectedBuffer != null)
            {
                SelectedBuffer.B_name = TobeEditedItem.B_name;
                SelectedBuffer.B_description = TobeEditedItem.B_description;
                SelectedBuffer.B_comID = TobeEditedItem.B_comID;
                SelectedBuffer.B_unit = TobeEditedItem.B_unit;
            }
        }

        /// <summary>
        /// Delete current selected Buffer
        /// </summary>
        public void Delete()
        {
            Buffer temp = new Buffer();
            temp = SelectedBuffer;
            this.ObservableBuffer.Remove(this.SelectedBuffer);
            Blist.DeleteABuffer(temp);
        }

        #endregion


    }
}
