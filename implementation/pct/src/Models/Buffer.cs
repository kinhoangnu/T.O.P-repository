using System;
using com.vanderlande.wpf;

namespace Your
{
    public class Buffer : ContentViewModel
    {
        private string bName;

        public bool IsSelected { get; set; }

        public string Uuid { get; set; }

        public string BDescription { get; set; }

        public string BName
        {
            get { return bName; }
            set { ChangeProperty(ref bName, value); }
        }

        public string BComId { get; set; }

        public string BUnit { get; set; }

        public Buffer()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}