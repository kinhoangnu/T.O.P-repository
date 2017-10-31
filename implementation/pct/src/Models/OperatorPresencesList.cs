using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class OperatorPresencesList : ContentViewModel
    {
        public static ObservableCollection<OperatorPresences> OperatorPresences { get; set; }

        public OperatorPresencesList()
        {
            OperatorPresences = new ObservableCollection<OperatorPresences>();
        }

        public static ObservableCollection<OperatorPresences> GetOperatorPresencesList()
        {
            //generateOperators();
            return OperatorPresences;
        }

        public static OperatorPresences GetAOperatorPresences(string s)
        {
            //generateOperators();
            foreach (var o in OperatorPresences)
            {
                if (o.Uuid == s)
                {
                    //return tempo;
                }
            }
            return null;
        }
    }
}