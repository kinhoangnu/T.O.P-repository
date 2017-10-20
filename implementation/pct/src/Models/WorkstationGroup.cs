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
        private string wgName;
        private string wgDescription;

        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }
        public string WgName
        {
            get
            {
                return wgName;
            }
            set
            {
                ChangeProperty(ref wgName, value);
            }
        }
        public string WgDescription
        {
            get
            {
                return wgDescription;
            }
            set
            {
                ChangeProperty(ref wgDescription, value);
            }
        }

        public WorkstationGroup()
        {
        }
    }
}

