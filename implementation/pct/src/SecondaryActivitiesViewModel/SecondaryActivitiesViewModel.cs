using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class SecondaryActivitiesViewModel : ContentViewModel
    {
        public SecondaryActivityList SClist;
        private SecondaryActivity selectedSecondaryActivity;
        private ObservableCollection<SecondaryActivity> observableSecondaryActivity;
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public ObservableCollection<SecondaryActivity> ObservableSecondaryActivity
        {
            get { return SecondaryActivityList.SecondaryActivities; }
            set { ChangeProperty(ref observableSecondaryActivity, value); }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public SecondaryActivity SelectedSecondaryActivity
        {
            get { return selectedSecondaryActivity; }
            set { ChangeProperty(ref selectedSecondaryActivity, value); }
        }

        public SecondaryActivitiesViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            SecondaryActivityList.SecondaryActivities = new ObservableCollection<SecondaryActivity>();
            ObservableSecondaryActivity = new ObservableCollection<SecondaryActivity>();
        }

        /// <summary>
        /// Add a new buffer to SelectedSecondaryActivity list
        /// </summary>
        public void Add()
        {
            ObservableSecondaryActivity.Add(new SecondaryActivity
            {
                ScName = "",
                ScDescription = "",
                ScComId = ""
            });
        }

        /// <summary>
        /// Delete current selected SelectedSecondaryActivity
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedSecondaryActivity() != null)
            {
                MessageBox.Show("This Secondary Activity is currently attached to a Workstation Class (" +
                                CheckMatchedSecondaryActivity().WcName + "). Please:" +
                                " \n\nRemove the Workstation Class in \"WorkstationClasses\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached activity to another one");
            }
            else
            {
                ObservableSecondaryActivity.Remove(SelectedSecondaryActivity);
            }
        }

        /// <summary>
        /// Return true if a matched Secondary Activity is found being used in a item of WorkstationClass list
        /// </summary>
        /// <returns></returns>
        private WorkstationClass CheckMatchedSecondaryActivity()
        {
            return
                WorkstationClassesViewModel.ObservableWorkstationClass.FirstOrDefault(
                    wc => wc.ScRef.ScName == SelectedSecondaryActivity.ScName);
        }
    }
}