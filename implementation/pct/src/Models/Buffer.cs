using System;
using com.vanderlande.wpf;

namespace Your
{
    public class Buffer : ContentViewModel
    {
        public bool IsSelected { get; set; }

        public string Uuid { get; set; }

        public string BDescription { get; set; }

        public string BName { get; set; }

        public string BComId { get; set; }

        public string BUnit { get; set; }

        public Buffer()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}