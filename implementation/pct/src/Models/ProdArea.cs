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
        private string pName;
        private string pComId;        
        private string pDescription;
        private string pType;

        public string PType
        {
            get { return pType; }
            set { pType = value; }
        }
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public string PDescription
        {
            get
            {
                return pDescription;
            }
            set
            {
                ChangeProperty(ref pDescription, value);
            }
        }
        public string PName
        {
            get
            {
                return pName;
            }
            set
            {
                ChangeProperty(ref pName, value);
            }
        }
        public string PComId
        {
            get
            {
                return pComId;
            }
            set
            {
                ChangeProperty(ref pComId, value);
            }
        }
        public ProdArea()
        {
        }
    }
}
