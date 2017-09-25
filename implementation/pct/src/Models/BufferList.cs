using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Your
{
    public class BufferList
    {
        public ObservableCollection<Buffer> Buffers;
        public BufferList()
        {
            Buffers = new ObservableCollection<Buffer>
            {
                new Buffer{B_objName = "Receiving",B_description="General Receiving Buffer", B_ComID="ReceivingBuffer",B_Unit="Pallet"},
                new Buffer{B_objName="High bay",B_description="High Bay",B_ComID="HighBay",B_Unit="Tray"},
                new Buffer{B_objName="Tray store",B_description="Marshalling Area",B_ComID="Marshallow",B_Unit="Pallet"},
                new Buffer{B_objName="Xdock",B_description="Xdock",B_ComID="Xdock",B_Unit="Pallet"},
                new Buffer{B_objName="Receiving",B_description="General Receiving Buffer",B_ComID="ReceivingBuffer",B_Unit="Tray"},
                new Buffer{B_objName="Tray Store GtP",B_description="Tray Store GtP",B_ComID="Ams",B_Unit="Tray"},
                new Buffer{B_objName="Marshalling",B_description="Marshalling Area",B_ComID="Marshallow",B_Unit="Tray"},
                new Buffer{B_objName="High bay",B_description="General Receiving Buffer",B_ComID="HighBay",B_Unit="Tray"}            
            };
        }

        public ObservableCollection<Buffer> GetBufferList()
        {
            return this.Buffers;
        }

        public void RemoveABuffer(Buffer b)
        {
            this.Buffers.Remove(b);
        }

        public Buffer GetABuffer(int n)
        {
            return Buffers.ElementAt(n); 
        }

        public Buffer ReturnABuffer(string s)
        {
            foreach (Buffer b in Buffers)
            {
                if (b.B_objName == s)
                    return b;
            }

            return null;
        }

    }
}
