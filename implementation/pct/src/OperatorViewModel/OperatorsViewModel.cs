using System.Collections.ObjectModel;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class OperatorsViewModel : ContentViewModel
    {
        private Operator selectedOperator;
        private ObservableCollection<Operator> observableOperator;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<Operator> ObservableOperator
        {
            get { return OperatorList.Operators; }
            set { ChangeProperty(ref observableOperator, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public Operator SelectedOperator
        {
            get { return selectedOperator; }
            set { ChangeProperty(ref selectedOperator, value); }
        }

        public OperatorsViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            OperatorList.Operators = new ObservableCollection<Operator>();
            ObservableOperator = new ObservableCollection<Operator>();
        }

        /// <summary>
        /// Add a new buffer to Operator list
        /// </summary>
        public void Add()
        {
            ObservableOperator.Add(new Operator());
        }

        /// <summary>
        /// Delete current selected Operator
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedOperator() != null)
            {
                MessageBox.Show("This Operator is currently attached to a Process (" + CheckMatchedOperator().PcName +
                                "). Please:" +
                                " \n\nRemove the Process in \"Processes\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached buffer to another one");
            }
            else
            {
                ObservableOperator.Remove(SelectedOperator);
            }
        }

        /// <summary>
        /// Return true if a matched Operator is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private static Process CheckMatchedOperator()
        {
            return null;
        }
    }
}