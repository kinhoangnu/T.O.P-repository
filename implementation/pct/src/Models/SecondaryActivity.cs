using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class SecondaryActivity : ContentViewModel
    {
        private string uuid;
        private string sc_name;
        private string editsc_name;
        private string sc_comID;
        private string sc_description;

        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public string SC_description
        {
            get
            {
                return sc_description;
            }
            set
            {
                ChangeProperty(ref sc_description, value);
            }
        }
        public string SC_name
        {
            get
            {
                return sc_name;
            }
            set
            {
                ChangeProperty(ref sc_name, value);
            }
        }

        public string editSC_name
        {
            get
            {
                return editsc_name;
            }
            set
            {
                ChangeProperty(ref editsc_name, value);
            }
        }
        public string SC_comID
        {
            get
            {
                return sc_comID;
            }
            set
            {
                ChangeProperty(ref sc_comID, value);
            }
        }

        public SecondaryActivity()
        {
        }

        public SecondaryActivity NewCopySecondaryActivity(SecondaryActivity scCopy)
        {
            SecondaryActivity sc = new SecondaryActivity();
            sc.SC_name = scCopy.SC_name;
            sc.SC_description = scCopy.SC_description;
            sc.SC_comID = scCopy.SC_comID;
            sc.Uuid = scCopy.Uuid;
            return sc;
        }
    }
}
