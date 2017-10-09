using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;
using System.Xml.Serialization;

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
                _buffers = value;
            }
        }
        public BufferList()
        {
            Buffers = new ObservableCollection<Buffer>
            {
            //new Buffer{B_name ="",B_description="", B_comID="",B_unit=""},
            //    new Buffer{B_name="High bay",B_description="High Bay",B_comID="HighBay",B_unit="Tray"},
            //    new Buffer{B_name="Tray store",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Pallet"},
            //    new Buffer{B_name="Xdock",B_description="Xdock",B_comID="Xdock",B_unit="Pallet"},
            //    new Buffer{B_name="Receiving",B_description="General Receiving Buffer",B_comID="ReceivingBuffer",B_unit="Tray"},
            //    new Buffer{B_name="Tray Store GtP",B_description="Tray Store GtP",B_comID="Ams",B_unit="Tray"},
            //    new Buffer{B_name="Marshalling",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Tray"},
            //    new Buffer{B_name="High bay",B_description="General Receiving Buffer",B_comID="HighBay",B_unit="Tray"}            
            };
        }

        private static void generateBuffers()
        {
            Buffers = new ObservableCollection<Buffer>
            {
                //new Buffer{B_name = "DEFAULT",B_description="DEFAULT", B_comID="DEFAULT",B_unit="DEFAULT"},
                //new Buffer{B_name="High bay",B_description="High Bay",B_comID="HighBay",B_unit="Tray"},
                //new Buffer{B_name="Tray store",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Pallet"},
                //new Buffer{B_name="Xdock",B_description="Xdock",B_comID="Xdock",B_unit="Pallet"},
                //new Buffer{B_name="Receiving",B_description="General Receiving Buffer",B_comID="ReceivingBuffer",B_unit="Tray"},
                //new Buffer{B_name="Tray Store GtP",B_description="Tray Store GtP",B_comID="Ams",B_unit="Tray"},
                //new Buffer{B_name="Marshalling",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Tray"},
                //new Buffer{B_name="High bay",B_description="General Receiving Buffer",B_comID="HighBay",B_unit="Tray"}              
            };
        }
        public static ObservableCollection<Buffer> GetBufferList()
        {
            //generateBuffers();
            return Buffers;
        }

        public static Buffer GetABuffer(string s)
        {
            //generateBuffers();
            foreach (Buffer b in Buffers)
            {
                if (b.Uuid == s)
                    return b;
            }
            return null;
        }

        public Buffer ReturnABuffer(string s)
        {
            foreach (Buffer b in Buffers)
            {
                if (b.B_name == s)
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
