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
    public class WorkstationGroupList : ContentViewModel
    {
        private static ObservableCollection<WorkstationGroup> workstationGroups;

        public static ObservableCollection<WorkstationGroup> WorkstationGroups
        {
            get { return WorkstationGroupList.workstationGroups; }
            set { WorkstationGroupList.workstationGroups = value; }
        }
        public WorkstationGroupList()
         {
            // WorkstationGroups = new ObservableCollection<WorkstationGroup>
            //{
            //    new WorkstationGroup{ WG_name = "<None>"},
            //    new WorkstationGroup{ WG_name = "Super Module 1", WG_description="Super Module 1"},
            //    new WorkstationGroup{ WG_name = "Super Module 2", WG_description = "Super Module 2"},
            //    new WorkstationGroup{ WG_name = "Super Module 3", WG_description = "Super Module 3"},
            //    new WorkstationGroup{ WG_name = "Super Module 4", WG_description = "Super Module 4"},
            //    new WorkstationGroup{ WG_name = "Super Module 5", WG_description = "Super Module 5"},
            //    new WorkstationGroup{ WG_name = "Super Module 6", WG_description = "Super Module 6"},
            //    new WorkstationGroup{ WG_name = "Super Module 7", WG_description = "Super Module 7"},
            //    new WorkstationGroup{ WG_name = "Super Module 8", WG_description = "Super Module 8"},
            //    new WorkstationGroup{ WG_name = "Super Module 9", WG_description = "Super Module 9"}
            //};
        }

        private static void GenerateWorkstationGroups()
        {
            //WorkstationGroups = new ObservableCollection<WorkstationGroup>
            //{
            //    new WorkstationGroup{ WG_name = "<None>"},
            //    new WorkstationGroup{ WG_name = "Super Module 1", WG_description = "Super Module 1"},
            //    new WorkstationGroup{ WG_name = "Super Module 2", WG_description = "Super Module 2"},
            //    new WorkstationGroup{ WG_name = "Super Module 3", WG_description = "Super Module 3"},
            //    new WorkstationGroup{ WG_name = "Super Module 4", WG_description = "Super Module 4"},
            //    new WorkstationGroup{ WG_name = "Super Module 5", WG_description = "Super Module 5"},
            //    new WorkstationGroup{ WG_name = "Super Module 6", WG_description = "Super Module 6"},
            //    new WorkstationGroup{ WG_name = "Super Module 7", WG_description = "Super Module 7"},
            //    new WorkstationGroup{ WG_name = "Super Module 8", WG_description = "Super Module 8"},
            //    new WorkstationGroup{ WG_name = "Super Module 9", WG_description = "Super Module 9"}
            //};
        }

        public static ObservableCollection<WorkstationGroup> GetWorkstationGroupList()
        {
            //generateWorkstationGroups();
            return WorkstationGroups;
        }

        public static WorkstationGroup GetAWorkstationGroup(string s)
        {
            foreach (WorkstationGroup wg in WorkstationGroups)
            {
                if (wg.Uuid == s)
                    return wg;
            }
            return null;
        }

        public void DeleteAProcess(WorkstationGroup p)
        {
            WorkstationGroups.Remove(p);
        }

    }
}
