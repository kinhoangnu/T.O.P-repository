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
        public ProcessList()
         {
             bList = BufferList.Buffers;
             pList = ProdAreaList.ProdAreas;
             Processes = new ObservableCollection<Process>
            {
                new Process{ PC_name = "Receiving Normal",PC_description="Normal Pallet Receiving Process", PC_comID="Receiving", ProdRef = ProdAreaList.GetAProdArea(1), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(1), IsReplenished=true, ExclFromKPI=false},
                new Process{ PC_name = "Receiving Xdock", PC_description = "Xdock Receiving Process", PC_comID = "XdockReceiving", ProdRef = ProdAreaList.GetAProdArea(2), InbufferRef = BufferList.GetABuffer(3), OutbufferRef = BufferList.GetABuffer(4), IsReplenished = false, ExclFromKPI = false },
                new Process{ PC_name = "Depalletising", PC_description = "ACP Depalletising Process", PC_comID = "AmsReplenishment", ProdRef = ProdAreaList.GetAProdArea(3), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(7), IsReplenished = true, ExclFromKPI = false },
                new Process{ PC_name = "ACP Picking", PC_description = "ACP Palletising Process", PC_comID = "Palletising", ProdRef = ProdAreaList.GetAProdArea(4), InbufferRef = BufferList.GetABuffer(2), OutbufferRef = BufferList.GetABuffer(0), IsReplenished = true, ExclFromKPI = true },
                new Process{ PC_name = "NC Picking", PC_description = "NC Picking Process", PC_comID = "NcPicking", ProdRef = ProdAreaList.GetAProdArea(5), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(3), IsReplenished = true, ExclFromKPI = true },
                new Process{ PC_name = "Goods to Person", PC_description = "Goods to Person", PC_comID = "GoodsToPerson", ProdRef = ProdAreaList.GetAProdArea(6), InbufferRef = BufferList.GetABuffer(5), OutbufferRef = BufferList.GetABuffer(6), IsReplenished = true, ExclFromKPI = false },
                new Process{ PC_name = "Bulk Picking", PC_description = "Bulk Picking Process", PC_comID = "BulkPicking", ProdRef = ProdAreaList.GetAProdArea(0), InbufferRef = BufferList.GetABuffer(7), OutbufferRef = BufferList.GetABuffer(1), IsReplenished = false, ExclFromKPI = false }
            };
        }        

        public ObservableCollection<Process> GetProcessList()
        {
            return Processes;
        }

        public Process GetAProcess(int n)
        {
            return Processes.ElementAt(n);
        }

    }
}
