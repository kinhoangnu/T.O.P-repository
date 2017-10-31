using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class OperatorList : ContentViewModel
    {
        public static ObservableCollection<Operator> Operators { get; set; }

        public OperatorList()
        {
            Operators = new ObservableCollection<Operator>();
        }

        public static ObservableCollection<Operator> GetOperatorList()
        {
            //generateOperators();
            return Operators;
        }

        public static Operator GetAOperator(string s)
        {
            //generateOperators();
            foreach (var o in Operators)
            {
                if (o.Uuid == s)
                {
                    return o;
                }
            }
            return null;
        }

        public void DeleteAOperator(Operator o)
        {
            Operators.Remove(o);
        }

        private static void GenerateOperators()
        {
            Operators = new ObservableCollection<Operator>();
        }
    }
}