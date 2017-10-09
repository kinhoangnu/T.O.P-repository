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
        private string uuid;
        private string p_name;
        private string p_comID;        
        private string p_description;
        private string p_type;

        public string P_type
        {
            get { return p_type; }
            set { p_type = value; }
        }
        public string Uuid
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
        public ProdArea()
        {
        }
    }
}
