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
    public class WorkstationClassList : ContentViewModel
    {
        private static ObservableCollection<WorkstationClass> _workstationClasses;
        public static ObservableCollection<WorkstationClass> WorkstationClasses
        {
            get
            {
                return _workstationClasses;
            }
            set
            {
                _workstationClasses = value;
            }
        }
        public WorkstationClassList()
         {
            // WorkstationClasses = new ObservableCollection<WorkstationClass>
            //{
            //    new WorkstationClass{ WC_name = "Receiving Normal",WC_type="Normal Pallet Receiving Process", WC_handlingType="Receiving", ProcessRef = ProcessList.GetAProcess(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "Receiving Xdock", WC_type = "Xdock Receiving Process", WC_handlingType = "XdockReceiving", ProcessRef = ProcessList.GetAProcess(2), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(3)},
            //    new WorkstationClass{ WC_name = "Depalletising", WC_type = "ACP Depalletising Process", WC_handlingType = "AmsReplenishment", ProcessRef = ProcessList.GetAProcess(3), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "ACP Picking", WC_type = "ACP Palletising Process", WC_handlingType = "Palletising", ProcessRef = ProcessList.GetAProcess(4), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(2)},
            //    new WorkstationClass{ WC_name = "NC Picking", WC_type = "NC Picking Process", WC_handlingType = "NcPicking", ProcessRef = ProcessList.GetAProcess(5), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "Goods to Person", WC_type = "Goods to Person", WC_handlingType = "GoodsToPerson", ProcessRef = ProcessList.GetAProcess(6), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(0)},
            //    new WorkstationClass{ WC_name = "Bulk Picking", WC_type = "Bulk Picking Process", WC_handlingType = "BulkPicking", ProcessRef = ProcessList.GetAProcess(0), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(2)}
            //};
        }

        private static void generateWorkstationClasses()
        {
            //WorkstationClasses = new ObservableCollection<WorkstationClass>
            //{
            //    new WorkstationClass{ WC_name = "Receiving Normal",WC_type="Normal Pallet Receiving Process", WC_handlingType="Receiving", ProcessRef = ProcessList.GetAProcess(1), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "Receiving Xdock", WC_type = "Xdock Receiving Process", WC_handlingType = "XdockReceiving", ProcessRef = ProcessList.GetAProcess(2), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(3)},
            //    new WorkstationClass{ WC_name = "Depalletising", WC_type = "ACP Depalletising Process", WC_handlingType = "AmsReplenishment", ProcessRef = ProcessList.GetAProcess(3), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "ACP Picking", WC_type = "ACP Palletising Process", WC_handlingType = "Palletising", ProcessRef = ProcessList.GetAProcess(4), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(2)},
            //    new WorkstationClass{ WC_name = "NC Picking", WC_type = "NC Picking Process", WC_handlingType = "NcPicking", ProcessRef = ProcessList.GetAProcess(5), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(1)},
            //    new WorkstationClass{ WC_name = "Goods to Person", WC_type = "Goods to Person", WC_handlingType = "GoodsToPerson", ProcessRef = ProcessList.GetAProcess(6), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(0)},
            //    new WorkstationClass{ WC_name = "Bulk Picking", WC_type = "Bulk Picking Process", WC_handlingType = "BulkPicking", ProcessRef = ProcessList.GetAProcess(0), SecondaryactivityRef = SecondaryActivityList.GetASecondaryActivity(2)}
            //};
        }

        public static ObservableCollection<WorkstationClass> GetWorkstationClassList()
        {
            //generateWorkstationClasses();
            return WorkstationClasses;
        }

        public static WorkstationClass GetAWorkstationClass(int n)
        {
            generateWorkstationClasses();
            return WorkstationClasses.ElementAt(n);
        }
    }
}
