using System;
using com.vanderlande.wpf;

namespace Your
{
    public class ProdArea : ContentViewModel
    {
        private string pName;
        private string pComId;
        private string pDescription;

        public string PType { get; set; }

        public string Uuid { get; set; }

        public string PDescription
        {
            get { return pDescription; }
            set { ChangeProperty(ref pDescription, value); }
        }

        public string PName
        {
            get { return pName; }
            set { ChangeProperty(ref pName, value); }
        }

        public string PComId
        {
            get { return pComId; }
            set { ChangeProperty(ref pComId, value); }
        }

        public ProdArea()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}