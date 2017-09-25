using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;

namespace Your
{
    public class BufferList : ContentViewModel
    {
        private static ObservableCollection<Buffer> _buffers;

        public static ObservableCollection<Buffer> Buffers
        {
            get
            {
                return _buffers;
            }
            set
            {
                //ChangeProperty(ref _buffers, value);
                _buffers = value;
            }
        }
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

        private static void generateBuffers()
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
        public static ObservableCollection<Buffer> GetBufferList()
        {
            return Buffers;
        }

        public static Buffer GetABuffer(int n)
        {
            generateBuffers();
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

        public void DeleteABuffer(Buffer b)
        {
            Buffers.Remove(b);
        }

    }
}
