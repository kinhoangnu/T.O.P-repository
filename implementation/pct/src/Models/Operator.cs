using System;

namespace Your
{
    public class Operator
    {
        public bool OUseCustomUpperLimit { get; set; }

        public string Uuid { get; set; }

        public string ODescription { get; set; }

        public string OName { get; set; }

        public Operator()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}