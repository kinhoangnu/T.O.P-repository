using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;

namespace Your
{
    public class Process : ContentViewModel
    {
        private Guid uuid;

        private string pc_name;
        private string editpc_name;
        private string pc_comID;
        private string pc_description;        
        private Buffer inbufferRef;
        private Buffer outbufferRef;
        private ProdArea prodRef;
        private ObservableCollection<ProdArea> _observableProdArea;
        private ObservableCollection<Buffer> _observableBuffer;

        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return BufferList.Buffers; }
            set { ChangeProperty(ref _observableBuffer, value); }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return ProdAreaList.ProdAreas; }
            set { ChangeProperty(ref _observableProdArea, value); }
        }

        private bool isReplenished;
        private bool exclFromKPI;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public bool ExclFromKPI
        {
            get { return exclFromKPI; }
            set 
            {
                ChangeProperty(ref exclFromKPI, value);
            }
        }
        public bool IsReplenished
        {
            get { return isReplenished; }
            set { ChangeProperty(ref isReplenished, value); }
        }
        public ProdArea ProdRef
        {
            get { return prodRef; }
            set { ChangeProperty(ref prodRef, value); }
        }
        public Buffer OutbufferRef
        {
            get { return outbufferRef; }
            set { ChangeProperty(ref outbufferRef, value); }
        }

        public Buffer InbufferRef
        {
            get { return inbufferRef; }
            set { ChangeProperty(ref inbufferRef, value); }
        }

        public string PC_description
        {
            get
            {
                return pc_description;
            }
            set
            {
                ChangeProperty(ref pc_description, value);
            }
        }
        public string PC_name
        {
            get
            {
                return pc_name;
            }
            set
            {
                ChangeProperty(ref pc_name, value);
            }
        }
        public string PC_comID
        {
            get
            {
                return pc_comID;
            }
            set
            {
                ChangeProperty(ref pc_comID, value);
            }
        }

        public Process()
        {
            uuid = Guid.NewGuid();
        }
    }
}

