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
        private string sc_comID;
        private string sc_description;
        private bool isSelected = false;

        public bool IsSelected 
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

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

    }
}
