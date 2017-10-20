using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;
using System.Xml.Serialization;

namespace Your
{
    public class Operator
    {
        private string uuid;
        private string oName;
        private string oDescription;
        private bool oUseCustomUpperLimit;
        private bool isSelected;

        public bool OUseCustomUpperLimit
        {
            get { return oUseCustomUpperLimit; }
            set { oUseCustomUpperLimit = value; }
        }

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
        public string ODescription
        {
            get
            {
                return oDescription;
            }
            set
            {
                oDescription = value;
            }
        }
        public string OName
        {
            get
            {
                return oName;
            }
            set
            {
                oName = value;
            }
        }

        public Operator()
        {
        }

    }

}
