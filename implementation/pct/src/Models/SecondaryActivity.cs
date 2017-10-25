using System;
using com.vanderlande.wpf;

namespace Your
{
    public class SecondaryActivity : ContentViewModel
    {
        private string scName;
        private string scComId;
        private string scDescription;
        private bool maxAllowedSpecified;
        private long maxAllowed;

        public bool IsSelected { get; set; }

        public string Uuid { get; set; }

        public bool MaxAllowedSpecified
        {
            get { return maxAllowedSpecified; }
            set { ChangeProperty(ref maxAllowedSpecified, value); }
        }

        public long MaxAllowed
        {
            get { return maxAllowed; }
            set { ChangeProperty(ref maxAllowed, value); }
        }

        public string ScDescription
        {
            get { return scDescription; }
            set { ChangeProperty(ref scDescription, value); }
        }

        public string ScName
        {
            get { return scName; }
            set { ChangeProperty(ref scName, value); }
        }

        public string ScComId
        {
            get { return scComId; }
            set { ChangeProperty(ref scComId, value); }
        }

        public SecondaryActivity()
        {
            IsSelected = false;
            Uuid = Guid.NewGuid().ToString();
        }
    }
}