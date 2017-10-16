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
    public class OperatorActivityList : ContentViewModel
    {
        private static ObservableCollection<OperatorActivity> _operatorActivities;
        public static ObservableCollection<OperatorActivity> OperatorActivities
        {
            get
            {
                return _operatorActivities;
            }
            set
            {
                _operatorActivities = value;
            }
        }
        public OperatorActivityList()
         {
            // OperatorActivityes = new ObservableCollection<OperatorActivity>
            //{
            //    new OperatorActivity{ PC_name = "Receiving Normal",PC_description="Normal Pallet Receiving OperatorActivity", PC_comID="Receiving", ProdRef = ProdAreaList.GetAProdArea(1), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(1), IsReplenished=true, ExclFromKPI=false},
            //    new OperatorActivity{ PC_name = "Receiving Xdock", PC_description = "Xdock Receiving OperatorActivity", PC_comID = "XdockReceiving", ProdRef = ProdAreaList.GetAProdArea(2), InbufferRef = BufferList.GetABuffer(3), OutbufferRef = BufferList.GetABuffer(4), IsReplenished = false, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "Depalletising", PC_description = "ACP Depalletising OperatorActivity", PC_comID = "AmsReplenishment", ProdRef = ProdAreaList.GetAProdArea(3), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(7), IsReplenished = true, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "ACP Picking", PC_description = "ACP Palletising OperatorActivity", PC_comID = "Palletising", ProdRef = ProdAreaList.GetAProdArea(4), InbufferRef = BufferList.GetABuffer(2), OutbufferRef = BufferList.GetABuffer(0), IsReplenished = true, ExclFromKPI = true },
            //    new OperatorActivity{ PC_name = "NC Picking", PC_description = "NC Picking OperatorActivity", PC_comID = "NcPicking", ProdRef = ProdAreaList.GetAProdArea(5), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(3), IsReplenished = true, ExclFromKPI = true },
            //    new OperatorActivity{ PC_name = "Goods to Person", PC_description = "Goods to Person", PC_comID = "GoodsToPerson", ProdRef = ProdAreaList.GetAProdArea(6), InbufferRef = BufferList.GetABuffer(5), OutbufferRef = BufferList.GetABuffer(6), IsReplenished = true, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "Bulk Picking", PC_description = "Bulk Picking OperatorActivity", PC_comID = "BulkPicking", ProdRef = ProdAreaList.GetAProdArea(0), InbufferRef = BufferList.GetABuffer(7), OutbufferRef = BufferList.GetABuffer(1), IsReplenished = false, ExclFromKPI = false }
            //};
        }

        public static void generateOperatorActivityes()
        {
            //OperatorActivityes = new ObservableCollection<OperatorActivity>
            //{
            //    new OperatorActivity{ PC_name = "Receiving Normal",PC_description="Normal Pallet Receiving OperatorActivity", PC_comID="Receiving", ProdRef = ProdAreaList.GetAProdArea(1), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(1), IsReplenished=true, ExclFromKPI=false},
            //    new OperatorActivity{ PC_name = "Receiving Xdock", PC_description = "Xdock Receiving OperatorActivity", PC_comID = "XdockReceiving", ProdRef = ProdAreaList.GetAProdArea(2), InbufferRef = BufferList.GetABuffer(3), OutbufferRef = BufferList.GetABuffer(4), IsReplenished = false, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "Depalletising", PC_description = "ACP Depalletising OperatorActivity", PC_comID = "AmsReplenishment", ProdRef = ProdAreaList.GetAProdArea(3), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(7), IsReplenished = true, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "ACP Picking", PC_description = "ACP Palletising OperatorActivity", PC_comID = "Palletising", ProdRef = ProdAreaList.GetAProdArea(4), InbufferRef = BufferList.GetABuffer(2), OutbufferRef = BufferList.GetABuffer(0), IsReplenished = true, ExclFromKPI = true },
            //    new OperatorActivity{ PC_name = "NC Picking", PC_description = "NC Picking OperatorActivity", PC_comID = "NcPicking", ProdRef = ProdAreaList.GetAProdArea(5), InbufferRef = BufferList.GetABuffer(1), OutbufferRef = BufferList.GetABuffer(3), IsReplenished = true, ExclFromKPI = true },
            //    new OperatorActivity{ PC_name = "Goods to Person", PC_description = "Goods to Person", PC_comID = "GoodsToPerson", ProdRef = ProdAreaList.GetAProdArea(6), InbufferRef = BufferList.GetABuffer(5), OutbufferRef = BufferList.GetABuffer(6), IsReplenished = true, ExclFromKPI = false },
            //    new OperatorActivity{ PC_name = "Bulk Picking", PC_description = "Bulk Picking OperatorActivity", PC_comID = "BulkPicking", ProdRef = ProdAreaList.GetAProdArea(0), InbufferRef = BufferList.GetABuffer(7), OutbufferRef = BufferList.GetABuffer(1), IsReplenished = false, ExclFromKPI = false }
            //};
        } 

        public static ObservableCollection<OperatorActivity> GetOperatorActivityList()
        {
            return OperatorActivities;
        }

        public static OperatorActivity GetAOperatorActivity(string s)
        {
            foreach (OperatorActivity pc in OperatorActivities)
            {
                if (pc.Uuid== s)
                    return pc;
            }
            return null;
        }

        public void DeleteAOperatorActivity(OperatorActivity p)
        {
            OperatorActivities.Remove(p);
        }
    }
}
