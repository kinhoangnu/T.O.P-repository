using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationGroup : ContentViewModel
    {
        private string uuid;
        private string wg_name;
        private string editwg_name;
        private string wg_description;
        private string editwg_description;

        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }

        public string Editwg_name
        {
            get
            {
                return editwg_name;
            }
            set
            {
                ChangeProperty(ref editwg_name, value);
            }
        }
        public string WG_name
        {
            get
            {
                return wg_name;
            }
            set
            {
                ChangeProperty(ref wg_name, value);
            }
        }
        public string WG_description
        {
            get
            {
                return wg_description;
            }
            set
            {
                ChangeProperty(ref wg_description, value);
            }
        }
        public string editWG_description
        {
            get
            {
                return editwg_description;
            }
            set
            {
                ChangeProperty(ref editwg_description, value);
            }
        }

        public WorkstationGroup()
        {
        }
    }
}

