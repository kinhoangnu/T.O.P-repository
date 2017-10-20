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
        private string uuid;
        private string pcName;
        private string pcComId;
        private string pcDescription;
        private Buffer inbufferRef;
        private Buffer outbufferRef;
        private ProdArea prodRef;
        private ObservableCollection<ProdArea> observableProdArea;
        private ObservableCollection<Buffer> observableBuffer;
        private ObservableCollection<Buffer> observableOutBuffer;

        public ObservableCollection<Buffer> ObservableOutBuffer
        {
            get { return observableOutBuffer; }
            set { ChangeProperty(ref observableOutBuffer, value); }
        }

        public ObservableCollection<Buffer> ObservableBuffer
        {
            get { return BufferList.Buffers; }
            set { ChangeProperty(ref observableBuffer, value); }
        }

        public ObservableCollection<ProdArea> ObservableProdArea
        {
            get { return ProdAreaList.ProdAreas; }
            set { ChangeProperty(ref observableProdArea, value); }
        }

        private bool isReplenished;
        private bool exclFromKpi;

        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public bool ExclFromKpi
        {
            get { return exclFromKpi; }
            set 
            {
                ChangeProperty(ref exclFromKpi, value);
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

        public string PcDescription
        {
            get
            {
                return pcDescription;
            }
            set
            {
                ChangeProperty(ref pcDescription, value);
            }
        }
        public string PcName
        {
            get
            {
                return pcName;
            }
            set
            {
                ChangeProperty(ref pcName, value);
            }
        }
        public string PcComId
        {
            get
            {
                return pcComId;
            }
            set
            {
                ChangeProperty(ref pcComId, value);
            }
        }

        public Process()
        {
        }
    }
}

