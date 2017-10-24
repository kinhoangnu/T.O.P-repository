using System;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationGroup : ContentViewModel
    {
        private string wgName;
        private string wgDescription;

        public string Uuid { get; set; }

        public string WgName
        {
            get { return wgName; }
            set { ChangeProperty(ref wgName, value); }
        }

        public string WgDescription
        {
            get { return wgDescription; }
            set { ChangeProperty(ref wgDescription, value); }
        }

        public WorkstationGroup()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}