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
        private string scName;
        private string scComId;
        private string scDescription;
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
        public string ScDescription
        {
            get
            {
                return scDescription;
            }
            set
            {
                ChangeProperty(ref scDescription, value);
            }
        }
        public string ScName
        {
            get
            {
                return scName;
            }
            set
            {
                ChangeProperty(ref scName, value);
            }
        }
        public string ScComId
        {
            get
            {
                return scComId;
            }
            set
            {
                ChangeProperty(ref scComId, value);
            }
        }

        public SecondaryActivity()
        {
        }

    }
}
