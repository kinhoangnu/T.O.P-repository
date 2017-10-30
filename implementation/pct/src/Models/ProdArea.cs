using System;
using System.Collections.Generic;
using com.vanderlande.wpf;

namespace Your
{
    public class ProdArea : ContentViewModel
    {
        //private ProductionType pType;
        private string pType;
        private string pName;
        private string pComId;
        private string pDescription;
        private List<string> pTypeList;

        public List<string> PTypeList
        {
            get { return new List<string> { "Inbound", "Outbound" }; }
            set { ChangeProperty(ref pTypeList, value); }
        }

        public string Uuid { get; set; }

        //public ProductionType PType
        //{
        //    get { return pType; }
        //    set { ChangeProperty(ref pType, value); }
        //}

        public string PType
        {
            get { return pType; }
            set { ChangeProperty(ref pType, value); }
        }

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

        //public IList<ProductionType> ProdTypes
        //{
        //    get { return Enum.GetValues(typeof(ProductionType)).Cast<ProductionType>().ToList(); }
        //}

        public ProdArea()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }

    //public enum ProductionType
    //{
    //    Inbound,
    //    Outbound
    //}
}