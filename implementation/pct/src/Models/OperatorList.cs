using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;
using System.Xml.Serialization;

namespace Your
{
    public class OperatorList : ContentViewModel
    {
        private static ObservableCollection<Operator> _operators;

        public static ObservableCollection<Operator> Operators
        {
            get
            {
                return _operators;
            }
            set
            {
                _operators = value;
            }
        }
        public OperatorList()
        {
            Operators = new ObservableCollection<Operator>
            {
            //new Operator{B_name ="",B_description="", B_comID="",B_unit=""},
            //    new Operator{B_name="High bay",B_description="High Bay",B_comID="HighBay",B_unit="Tray"},
            //    new Operator{B_name="Tray store",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Pallet"},
            //    new Operator{B_name="Xdock",B_description="Xdock",B_comID="Xdock",B_unit="Pallet"},
            //    new Operator{B_name="Receiving",B_description="General Receiving Operator",B_comID="ReceivingOperator",B_unit="Tray"},
            //    new Operator{B_name="Tray Store GtP",B_description="Tray Store GtP",B_comID="Ams",B_unit="Tray"},
            //    new Operator{B_name="Marshalling",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Tray"},
            //    new Operator{B_name="High bay",B_description="General Receiving Operator",B_comID="HighBay",B_unit="Tray"}            
            };
        }

        private static void generateOperators()
        {
            Operators = new ObservableCollection<Operator>
            {
                //new Operator{B_name = "DEFAULT",B_description="DEFAULT", B_comID="DEFAULT",B_unit="DEFAULT"},
                //new Operator{B_name="High bay",B_description="High Bay",B_comID="HighBay",B_unit="Tray"},
                //new Operator{B_name="Tray store",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Pallet"},
                //new Operator{B_name="Xdock",B_description="Xdock",B_comID="Xdock",B_unit="Pallet"},
                //new Operator{B_name="Receiving",B_description="General Receiving Operator",B_comID="ReceivingOperator",B_unit="Tray"},
                //new Operator{B_name="Tray Store GtP",B_description="Tray Store GtP",B_comID="Ams",B_unit="Tray"},
                //new Operator{B_name="Marshalling",B_description="Marshalling Area",B_comID="Marshallow",B_unit="Tray"},
                //new Operator{B_name="High bay",B_description="General Receiving Operator",B_comID="HighBay",B_unit="Tray"}              
            };
        }
        public static ObservableCollection<Operator> GetOperatorList()
        {
            //generateOperators();
            return Operators;
        }

        public static Operator GetAOperator(string s)
        {
            //generateOperators();
            foreach (Operator o in Operators)
            {
                if (o.Uuid == s)
                {
                    Operator tempo = new Operator();
                    tempo.O_name = o.O_name;
                    tempo.O_description = o.O_description;
                    tempo.O_useCustomUpperLimit = o.O_useCustomUpperLimit;
                    tempo.IsSelected = true;
                    return tempo;
                }
            }
            return null;
        }

        public void DeleteAOperator(Operator o)
        {
            Operators.Remove(o);
        }

    }
}
