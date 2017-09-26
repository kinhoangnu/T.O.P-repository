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

        private string b_name;
        private string editb_name;
        private string b_comID;
        private string b_unit;
        private string b_description;

        public Guid Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
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
        public string B_name
        {
            get
            {
                return b_name;
            }
            set
            {
                ChangeProperty(ref b_name, value);
            }
        }

        public string editB_name
        {
            get
            {
                return editb_name;
            }
            set
            {
                ChangeProperty(ref editb_name, value);
            }
        }
        public string B_comID
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
        public string B_unit
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

        public Buffer NewCopyBuffer(Buffer bCopy)
        {
            Buffer b = new Buffer();
            b.B_name = bCopy.B_name;
            b.B_description = bCopy.B_description;
            b.B_comID = bCopy.B_comID;
            b.B_unit = bCopy.B_unit;
            b.Uuid = bCopy.Uuid;
            return b;
        }
    }
}
