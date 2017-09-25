using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class Buffer : ContentViewModel
    {
        private Guid uuid;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        private string b_objName;
        private string b_comID;
        private string b_unit;
        private string b_description;

        public string B_description
        {
            get
            {
                return b_description;
            }
            set
            {
                ChangeProperty(ref b_description, value);
            }
        }

        public string B_objName
        {
            get
            {
                return b_objName;
            }
            set
            {
                ChangeProperty(ref b_objName, value);
            }
        }
        public string B_ComID
        {
            get
            {
                return b_comID;
            }
            set
            {
                ChangeProperty(ref b_comID, value);
            }
        }
        public string B_Unit
        {
            get
            {
                return b_unit;
            }
            set
            {
                ChangeProperty(ref b_unit, value);
            }
        }

        public Buffer()
        {
            uuid = Guid.NewGuid();
        }
    }
}
