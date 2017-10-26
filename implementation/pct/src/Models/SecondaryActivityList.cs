using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class SecondaryActivityList : ContentViewModel
    {
        public static ObservableCollection<SecondaryActivity> SecondaryActivities { get; set; }

        public static ObservableCollection<SecondaryActivity> GetSecondaryActivityList()
        {
            return SecondaryActivities;
        }

        public static SecondaryActivity GetASecondaryActivitywithMaxAllow(string s, long i)
        {
            foreach (var sc in SecondaryActivities)
            {
                if (sc.Uuid == s)
                {
                    var tempsc = new SecondaryActivity
                    {
                        ScName = sc.ScName,
                        Uuid = sc.Uuid,
                        ScComId = sc.ScComId,
                        ScDescription = sc.ScDescription,
                        IsSelected = true,
                        MaxAllowedSpecified = true,
                        MaxAllowed = i
                    };
                    tempsc.Uuid = s;
                    return tempsc;
                }
            }
            return null;
        }

        public static SecondaryActivity GetASecondaryActivitywithoutMaxAllow(string s)
        {
            foreach (var sc in SecondaryActivities)
            {
                if (sc.Uuid == s)
                {
                    var tempsc = new SecondaryActivity
                    {
                        ScName = sc.ScName,
                        Uuid = sc.Uuid,
                        ScComId = sc.ScComId,
                        ScDescription = sc.ScDescription,
                        IsSelected = true,
                        MaxAllowedSpecified = false
                    };
                    tempsc.Uuid = s;
                    return tempsc;
                }
            }
            return null;
        }

        public static SecondaryActivity GetANotSelectedSecondaryActivity(string s)
        {
            foreach (var sc in SecondaryActivities)
            {
                if (sc.Uuid == s)
                {
                    var tempsc = new SecondaryActivity
                    {
                        ScName = sc.ScName,
                        Uuid = sc.Uuid,
                        ScComId = sc.ScComId,
                        ScDescription = sc.ScDescription,
                        IsSelected = false
                    };
                    tempsc.Uuid = s;
                    return tempsc;
                }
            }
            return null;
        }

        public void DeleteASecondaryActivity(SecondaryActivity sc)
        {
            SecondaryActivities.Remove(sc);
        }

        private static void GenerateSecondaryActivities()
        {
            //SecondaryActivities = new ObservableCollection<SecondaryActivity>
            //{
            //    new SecondaryActivity{SC_name="Teaching",SC_description="Depalletising teaching", SC_comID="TEACHING"},
            //    new SecondaryActivity{SC_name="Return pallet building",SC_description="Return pallet building",SC_comID="RETURNS"},
            //    new SecondaryActivity{SC_name="Maintenance",SC_description="maintenance",SC_comID="MAINTENANCE"},
            //    new SecondaryActivity{SC_name="Extended maintenance",SC_description="Workstation + aisle maintenance",SC_comID="MAINTENANCE_MODULE"}
            //};
        }
    }
}