using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class BufferList : ContentViewModel
    {
        public static ObservableCollection<Buffer> Buffers { get; set; }

        public BufferList()
        {
            Buffers = new ObservableCollection<Buffer>();
        }

        public static ObservableCollection<Buffer> GetBufferList()
        {
            //generateBuffers();
            return Buffers;
        }

        public static Buffer ReturnaBuffer(string s)
        {
            foreach (var b in Buffers)
            {
                if (b.BName == s)
                {
                    return b;
                }
            }
            return null;
        }

        public static Buffer GetABuffer(string s)
        {
            //generateBuffers();
            foreach (var b in Buffers)
            {
                if (b.Uuid == s)
                {
                    var tempb = new Buffer();
                    tempb.BName = b.BName;
                    tempb.BDescription = b.BDescription;
                    tempb.BComId = b.BComId;
                    tempb.BUnit = b.BUnit;
                    tempb.IsSelected = true;
                    tempb.Uuid = b.Uuid;
                    return tempb;
                }
            }
            return null;
        }

        public static Buffer GetANotSelectedBuffer(string s)
        {
            //generateBuffers();
            foreach (var b in Buffers)
            {
                if (b.Uuid == s)
                {
                    var tempb = new Buffer();
                    tempb.BName = b.BName;
                    tempb.BDescription = b.BDescription;
                    tempb.BComId = b.BComId;
                    tempb.BUnit = b.BUnit;
                    tempb.IsSelected = false;
                    tempb.Uuid = b.Uuid;
                    return tempb;
                }
            }
            return null;
        }

        public void DeleteABuffer(Buffer b)
        {
            Buffers.Remove(b);
        }

        private static void GenerateBuffers()
        {
            Buffers = new ObservableCollection<Buffer>();
        }
    }
}