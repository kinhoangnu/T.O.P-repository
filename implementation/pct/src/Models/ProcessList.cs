using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Your
{
    public class ProcessList
    {
        public ObservableCollection<Process> Processes;
        ObservableCollection<Buffer> bList;
        ObservableCollection<ProdArea> pList;
        BufferList Blist;
        ProdAreaList Plist;
        public ProcessList()
         {
             bList = BufferList.Buffers;
             pList = ProdAreaList.ProdAreas;
             Processes = new ObservableCollection<Process>
            {
                new Process{ PC_objName = "Receiving Normal",PC_description="Normal Pallet Receiving Process", PC_ComID="Receiving", ProdRef = ProdAreaList.GetAProdArea(1), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(2), IsReplenished=true, ExclFromKPI=false},
                new Process{ PC_objName = "Receiving Xdock", PC_description = "Xdock Receiving Process", PC_ComID = "XdockReceiving", ProdRef = ProdAreaList.GetAProdArea(2), InbufferRef = BufferList.GetABuffer(3), OutbufferRef = BufferList.GetABuffer(4), IsReplenished = false, ExclFromKPI = false },
                new Process{ PC_objName = "Depalletising", PC_description = "ACP Depalletising Process", PC_ComID = "AmsReplenishment", ProdRef = ProdAreaList.GetAProdArea(3), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(7), IsReplenished = true, ExclFromKPI = false },
                new Process{ PC_objName = "ACP Picking", PC_description = "ACP Palletising Process", PC_ComID = "Palletising", ProdRef = ProdAreaList.GetAProdArea(4), InbufferRef = BufferList.GetABuffer(2), OutbufferRef = BufferList.GetABuffer(0), IsReplenished = true, ExclFromKPI = true },
                new Process{ PC_objName = "NC Picking", PC_description = "NC Picking Process", PC_ComID = "NcPicking", ProdRef = ProdAreaList.GetAProdArea(5), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(3), IsReplenished = true, ExclFromKPI = true },
                new Process{ PC_objName = "Goods to Person", PC_description = "Goods to Person", PC_ComID = "GoodsToPerson", ProdRef = ProdAreaList.GetAProdArea(6), InbufferRef = BufferList.GetABuffer(5), OutbufferRef = BufferList.GetABuffer(6), IsReplenished = true, ExclFromKPI = false },
                new Process{ PC_objName = "Bulk Picking", PC_description = "Bulk Picking Process", PC_ComID = "BulkPicking", ProdRef = ProdAreaList.GetAProdArea(0), InbufferRef = BufferList.GetABuffer(7), OutbufferRef = BufferList.GetABuffer(1), IsReplenished = false, ExclFromKPI = false }
            };
        }        

        public ObservableCollection<Process> GetProcessList()
        {
            return Processes;
        }

    }
}
