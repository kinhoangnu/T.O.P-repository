using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class ProdArea : ContentViewModel
    {
        private Guid uuid;

        private string p_name;
        private string editp_name;
        private string p_comID;
        private string p_type;
        private string p_description;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public string P_description
        {
            get
            {
                return p_description;
            }
            set
            {
                ChangeProperty(ref p_description, value);
            }
        }
        public string P_name
        {
            get
            {
                return p_name;
            }
            set
            {
                ChangeProperty(ref p_name, value);
            }
        }
        public string editP_name
        {
            get
            {
                return editp_name;
            }
            set
            {
                ChangeProperty(ref editp_name, value);
            }
        }
        public string P_comID
        {
            get
            {
                return p_comID;
            }
            set
            {
                ChangeProperty(ref p_comID, value);
            }
        }
        public string P_Type
        {
            get
            {
                return p_type;
            }
            set
            {
                ChangeProperty(ref p_type, value);
            }
        }

        public ProdArea()
        {
            uuid = Guid.NewGuid();
        }
    }
}
