using System;
using System.Collections.Generic;
using com.vanderlande.wpf;

namespace Your
{
    public class SecondaryActivity : ContentViewModel
    {
        private string scName;
        private string scComId;
        private string scDescription;
        private bool maxAllowedSpecified;
        private int maxAllowed;
        private List<int> maxAllowList;

        public List<int> MaxAllowList
        {
            get { return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; }
            set { ChangeProperty(ref maxAllowList, value); }
        }

        public bool IsSelected { get; set; }

        public string Uuid { get; set; }

        public bool MaxAllowedSpecified
        {
            get { return maxAllowedSpecified; }
            set { ChangeProperty(ref maxAllowedSpecified, value); }
        }

        public int MaxAllowed
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