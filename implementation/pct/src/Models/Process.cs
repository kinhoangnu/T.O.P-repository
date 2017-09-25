using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class Process : ContentViewModel
    {
        private Guid uuid;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        private string pc_objName;
        private string pc_comID;
        private string pc_description;
        private Buffer inbufferRef;
        private Buffer outbufferRef;
        private ProdArea prodRef;

        private bool isReplenished;
        private bool exclFromKPI;

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

        public string PC_objName
        {
            get
            {
                return pc_objName;
            }
            set
            {
                ChangeProperty(ref pc_objName, value);
            }
        }
        public string PC_ComID
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

