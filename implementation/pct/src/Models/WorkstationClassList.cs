using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Your
{
    public class WorkstationClassList
    {
        public ObservableCollection<WorkstationClass> WorkstationClasses;
        ObservableCollection<Process> pcList;
        ObservableCollection<SecondaryActivity> scList;
        public WorkstationClassList()
         {
             scList = SecondaryActivityList.SecondaryActivities;
             pcList = new ObservableCollection<Process>();
             WorkstationClasses = new ObservableCollection<WorkstationClass>
            {
                new WorkstationClass{ WC_name = "Receiving Normal",WC_type="Normal Pallet Receiving Process", WC_handlingType="Receiving", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList .GetASecondaryActivity(1)},
                new WorkstationClass{ WC_name = "Receiving Xdock", WC_type = "Xdock Receiving Process", WC_handlingType = "XdockReceiving", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(3)},
                new WorkstationClass{ WC_name = "Depalletising", WC_type = "ACP Depalletising Process", WC_handlingType = "AmsReplenishment", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
                new WorkstationClass{ WC_name = "ACP Picking", WC_type = "ACP Palletising Process", WC_handlingType = "Palletising", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(2)},
                new WorkstationClass{ WC_name = "NC Picking", WC_type = "NC Picking Process", WC_handlingType = "NcPicking", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
                new WorkstationClass{ WC_name = "Goods to Person", WC_type = "Goods to Person", WC_handlingType = "GoodsToPerson", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(5)},
                new WorkstationClass{ WC_name = "Bulk Picking", WC_type = "Bulk Picking Process", WC_handlingType = "BulkPicking", ProcessRef = pcList.ElementAt(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(7)}
            };
        }

        public ObservableCollection<WorkstationClass> GetWorkstationClassList()
        {
            return WorkstationClasses;
        }

    }
}
