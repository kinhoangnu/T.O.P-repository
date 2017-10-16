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
        private string o_name;
        private string o_description;
        private bool o_useCustomUpperLimit;
        private bool isSelected;

        public bool O_useCustomUpperLimit
        {
            get { return o_useCustomUpperLimit; }
            set { o_useCustomUpperLimit = value; }
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
        public string O_description
        {
            get
            {
                return o_description;
            }
            set
            {
                o_description = value;
            }
        }
        public string O_name
        {
            get
            {
                return o_name;
            }
            set
            {
                o_name = value;
            }
        }

        public Operator()
        {
        }

    }

}
