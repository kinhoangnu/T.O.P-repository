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
    public class SecondaryActivityList : ContentViewModel
    {
        private static ObservableCollection<SecondaryActivity> _secondaryActivities;

        public static ObservableCollection<SecondaryActivity> SecondaryActivities
        {
            get
            {
                return _secondaryActivities;
            }
            set
            {
                _secondaryActivities = value;
            }
        }
        public SecondaryActivityList()
        {
            SecondaryActivities = new ObservableCollection<SecondaryActivity>
            {
                new SecondaryActivity{SC_name="Teaching",SC_description="Depalletising teaching", SC_comID="TEACHING"},
                new SecondaryActivity{SC_name="Return pallet building",SC_description="Return pallet building",SC_comID="RETURNS"},
                new SecondaryActivity{SC_name="Maintenance",SC_description="maintenance",SC_comID="MAINTENANCE"},
                new SecondaryActivity{SC_name="Extended maintenance",SC_description="Workstation + aisle maintenance",SC_comID="MAINTENANCE_MODULE"}
            };
        }

        private static void generateSecondaryActivities()
        {
            SecondaryActivities = new ObservableCollection<SecondaryActivity>
            {
                new SecondaryActivity{SC_name="Teaching",SC_description="Depalletising teaching", SC_comID="TEACHING"},
                new SecondaryActivity{SC_name="Return pallet building",SC_description="Return pallet building",SC_comID="RETURNS"},
                new SecondaryActivity{SC_name="Maintenance",SC_description="maintenance",SC_comID="MAINTENANCE"},
                new SecondaryActivity{SC_name="Extended maintenance",SC_description="Workstation + aisle maintenance",SC_comID="MAINTENANCE_MODULE"}
            };
        }
        public static ObservableCollection<SecondaryActivity> GetSecondaryActivityList()
        {
            return SecondaryActivities;
        }

        public static SecondaryActivity GetASecondaryActivity(int n)
        {
            generateSecondaryActivities();
            return SecondaryActivities.ElementAt(n); 
        }

        public SecondaryActivity ReturnASecondaryActivity(string s)
        {
            foreach (SecondaryActivity sc in SecondaryActivities)
            {
                if (sc.SC_name == s)
                    return sc;
            }

            return null;
        }

        public void DeleteASecondaryActivity(SecondaryActivity sc)
        {
            SecondaryActivities.Remove(sc);
        }


    }
}
