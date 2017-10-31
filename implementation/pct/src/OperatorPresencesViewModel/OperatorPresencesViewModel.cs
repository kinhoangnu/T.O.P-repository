using System.Collections.ObjectModel;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class OperatorPresencesViewModel : ContentViewModel
    {
        private OperatorPresences selectedOperatorPresences;
        private ObservableCollection<OperatorPresences> observableOperatorPresences;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<OperatorPresences> ObservableOperatorPresences
        {
            get { return OperatorPresencesList.OperatorPresences; }
            set { ChangeProperty(ref observableOperatorPresences, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public OperatorPresences SelectedOperatorPresences
        {
            get { return selectedOperatorPresences; }
            set { ChangeProperty(ref selectedOperatorPresences, value); }
        }

        public OperatorPresencesViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            OperatorPresencesList.OperatorPresences = new ObservableCollection<OperatorPresences>();
            ObservableOperatorPresences = new ObservableCollection<OperatorPresences>();
        }

        /// <summary>
        /// Add a new buffer to Operator list
        /// </summary>
        public void Add()
        {
            ObservableOperatorPresences.Add(new OperatorPresences());
        }

        /// <summary>
        /// Delete current selected Operator
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedOperatorPresences() != null)
            {
                MessageBox.Show("This Operator is currently attached to a Process (" +
                                CheckMatchedOperatorPresences().PcName +
                                "). Please:" +
                                " \n\nRemove the Process in \"Processes\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached buffer to another one");
            }
            else
            {
                ObservableOperatorPresences.Remove(SelectedOperatorPresences);
            }
        }

        /// <summary>
        /// Return true if a matched Operator is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private static Process CheckMatchedOperatorPresences()
        {
            return null;
        }
    }
}