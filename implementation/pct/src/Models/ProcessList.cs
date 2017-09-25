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
        BufferList bList;
        ProdAreaList pList;
        public ProcessList()
         {
             bList = new BufferList();
             pList = new ProdAreaList();
             Processes = new ObservableCollection<Process>
            {
                new Process { PC_objName = "Receiving Normal",PC_description="Normal Pallet Receiving Process", PC_ComID="Receiving", ProdRef = pList.GetAProdArea(1), InbufferRef = bList.GetABuffer(1), OutbufferRef = bList.GetABuffer(2), IsReplenished=true, ExclFromKPI=false},
                new Process { PC_objName = "Receiving Xdock", PC_description = "Xdock Receiving Process", PC_ComID = "XdockReceiving", ProdRef = pList.GetAProdArea(2), InbufferRef = bList.GetABuffer(3), OutbufferRef = bList.GetABuffer(4), IsReplenished = false, ExclFromKPI = false },
                new Process { PC_objName = "Depalletising", PC_description = "ACP Depalletising Process", PC_ComID = "AmsReplenishment", ProdRef = pList.GetAProdArea(3), InbufferRef = bList.GetABuffer(1), OutbufferRef = bList.GetABuffer(7), IsReplenished = true, ExclFromKPI = false },
                new Process { PC_objName = "ACP Picking", PC_description = "ACP Palletising Process", PC_ComID = "Palletising", ProdRef = pList.GetAProdArea(4), InbufferRef = bList.GetABuffer(2), OutbufferRef = bList.GetABuffer(0), IsReplenished = true, ExclFromKPI = true },
                new Process { PC_objName = "NC Picking", PC_description = "NC Picking Process", PC_ComID = "NcPicking", ProdRef = pList.GetAProdArea(5), InbufferRef = bList.GetABuffer(1), OutbufferRef = bList.GetABuffer(3), IsReplenished = true, ExclFromKPI = true },
                new Process { PC_objName = "Goods to Person", PC_description = "Goods to Person", PC_ComID = "GoodsToPerson", ProdRef = pList.GetAProdArea(6), InbufferRef = bList.GetABuffer(5), OutbufferRef = bList.GetABuffer(6), IsReplenished = true, ExclFromKPI = false },
                new Process { PC_objName = "Bulk Picking", PC_description = "Bulk Picking Process", PC_ComID = "BulkPicking", ProdRef = pList.GetAProdArea(0), InbufferRef = bList.GetABuffer(7), OutbufferRef = bList.GetABuffer(1), IsReplenished = false, ExclFromKPI = false }
            };
        }        

        public ObservableCollection<Process> GetProcessList()
        {
            return Processes;
        }
    }
}
