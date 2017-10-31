using System.Collections.ObjectModel;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class OperatorActivitiesViewModel : ContentViewModel
    {
        private OperatorActivity selectedOperatorActivity;
        private ObservableCollection<OperatorActivity> observableOperatorActivity;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<OperatorActivity> ObservableOperatorActivity
        {
            get { return OperatorActivityList.OperatorActivities; }
            set { ChangeProperty(ref observableOperatorActivity, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public OperatorActivity SelectedOperatorActivity
        {
            get { return selectedOperatorActivity; }
            set { ChangeProperty(ref selectedOperatorActivity, value); }
        }

        public OperatorActivitiesViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            OperatorActivityList.OperatorActivities = new ObservableCollection<OperatorActivity>();
            ObservableOperatorActivity = new ObservableCollection<OperatorActivity>();
        }

        /// <summary>
        /// Add a new buffer to Operator list
        /// </summary>
        public void Add()
        {
            ObservableOperatorActivity.Add(new OperatorActivity());
        }

        /// <summary>
        /// Delete current selected Operator
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedOperatorActivity() != null)
            {
                MessageBox.Show("This Operator is currently attached to a Process (" +
                                CheckMatchedOperatorActivity().PcName +
                                "). Please:" +
                                " \n\nRemove the Process in \"Processes\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached buffer to another one");
            }
            else
            {
                ObservableOperatorActivity.Remove(SelectedOperatorActivity);
            }
        }

        /// <summary>
        /// Return true if a matched Operator is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private static Process CheckMatchedOperatorActivity()
        {
            return null;
        }
    }
}