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
    public class WorkstationList : ContentViewModel
    {
        private static ObservableCollection<Workstation> _workstations;

        public static ObservableCollection<Workstation> Workstations
        {
            get { return WorkstationList._workstations; }
            set { WorkstationList._workstations = value; }
        }
        public WorkstationList()
         {
             Workstations = new ObservableCollection<Workstation>
            {
                new Workstation{ W_name = "Pallet Infeed A1", W_description="Pallet Infeed A1 (semi-automatic)", W_comID="RCV/Rcv/EntryA1", WorkstationclassRef = WorkstationClassList.WorkstationClasses[0], WorkstationgroupRef = WorkstationGroupList.WorkstationGroups[0]},
                new Workstation{ W_name = "Pallet Infeed A2", W_description = "Pallet Infeed A2 (semi-automatic)", W_comID = "RCV/Rcv/EntryA2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(0), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed A3", W_description = "Pallet Infeed A3 (semi-automatic)", W_comID = "RCV/Rcv/EntryA3", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(0), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed B1", W_description = "Pallet Infeed B1 (semi-automatic)", W_comID = "RCV/Rcv/EntryB1", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(1), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed B2", W_description = "Pallet Infeed B2 (semi-automatic)", W_comID = "RCV/Rcv/EntryB2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(1), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed C1", W_description = "Pallet Infeed C1 (semi-automatic)", W_comID = "RCV/Rcv/EntryC1", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(2), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(1)},
                new Workstation{ W_name = "Pallet Infeed C2", W_description = "Pallet Infeed C2 (semi-automatic)", W_comID = "RCV/Rcv/EntryC2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(2), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(3)},
                new Workstation{ W_name = "Auto Pal 27", W_description = "Palletiser 27 (automatic)", W_comID = "PAL/AutoPal27", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(3), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(5)},
                new Workstation{ W_name = "Auto Pal 28", W_description = "Palletiser 28 (automatic)", W_comID = "PAL/AutoPal28", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(3), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(7)}
            };
        }

        private static void generateWorkstations()
        {
            Workstations = new ObservableCollection<Workstation>
            {
                new Workstation{ W_name = "Pallet Infeed A1", W_description="Pallet Infeed A1 (semi-automatic)", W_comID="RCV/Rcv/EntryA1", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(0), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed A2", W_description = "Pallet Infeed A2 (semi-automatic)", W_comID = "RCV/Rcv/EntryA2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(0), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed A3", W_description = "Pallet Infeed A3 (semi-automatic)", W_comID = "RCV/Rcv/EntryA3", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(0), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed B1", W_description = "Pallet Infeed B1 (semi-automatic)", W_comID = "RCV/Rcv/EntryB1", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(1), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed B2", W_description = "Pallet Infeed B2 (semi-automatic)", W_comID = "RCV/Rcv/EntryB2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(1), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(0)},
                new Workstation{ W_name = "Pallet Infeed C1", W_description = "Pallet Infeed C1 (semi-automatic)", W_comID = "RCV/Rcv/EntryC1", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(2), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(1)},
                new Workstation{ W_name = "Pallet Infeed C2", W_description = "Pallet Infeed C2 (semi-automatic)", W_comID = "RCV/Rcv/EntryC2", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(2), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(3)},
                new Workstation{ W_name = "Auto Pal 27", W_description = "Palletiser 27 (automatic)", W_comID = "PAL/AutoPal27", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(3), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(5)},
                new Workstation{ W_name = "Auto Pal 28", W_description = "Palletiser 28 (automatic)", W_comID = "PAL/AutoPal28", WorkstationclassRef = WorkstationClassList.GetAWorkstationClass(3), WorkstationgroupRef = WorkstationGroupList.GetAWorkstationGroup(7)}
            };
        }

        public static ObservableCollection<Workstation> GetWorkstationList()
        {
            generateWorkstations();
            return Workstations;
        }

        public static Workstation GetAWorkstation(int n)
        {
            generateWorkstations();
            return Workstations.ElementAt(n);
        }

        public void DeleteAProcess(Workstation p)
        {
            Workstations.Remove(p);
        }

    }
}
