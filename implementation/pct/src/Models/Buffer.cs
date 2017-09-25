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

        private string b_objName;
        private string editb_objName;
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

        public string editB_objName
        {
            get
            {
                return editb_objName;
            }
            set
            {
                ChangeProperty(ref editb_objName, value);
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

        public Buffer NewCopyBuffer(Buffer bCopy)
        {
            Buffer b = new Buffer();
            b.B_objName = bCopy.B_objName;
            b.B_description = bCopy.B_description;
            b.B_ComID = bCopy.B_ComID;
            b.B_Unit = bCopy.B_Unit;
            b.Uuid = bCopy.Uuid;
            return b;
        }
    }
}
