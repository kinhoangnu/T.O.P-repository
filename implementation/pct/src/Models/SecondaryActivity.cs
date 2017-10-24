using System;
using com.vanderlande.wpf;

namespace Your
{
    public class SecondaryActivity : ContentViewModel
    {
        private string scName;
        private string scComId;
        private string scDescription;

        public bool IsSelected { get; set; }

        public string Uuid { get; set; }

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