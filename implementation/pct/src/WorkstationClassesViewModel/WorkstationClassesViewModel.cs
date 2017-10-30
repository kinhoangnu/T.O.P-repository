using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    public class WorkstationClassesViewModel : ContentViewModel
    {
        private static ObservableCollection<WorkstationClass> observableWorkstationClass;
        private WorkstationClass selectedWorkstationClass;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        public static ObservableCollection<ObservableCollection<SecondaryActivity>> Slist { get; set; }

        public static ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { observableWorkstationClass = value; }
        }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public WorkstationClass SelectedWorkstationClass
        {
            get { return selectedWorkstationClass; }
            set { ChangeProperty(ref selectedWorkstationClass, value); }
        }

        public WorkstationClassesViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            WorkstationClassList.WorkstationClasses = new ObservableCollection<WorkstationClass>();
            ObservableWorkstationClass = new ObservableCollection<WorkstationClass>();
            SelectedWorkstationClass = new WorkstationClass();
        }

        /// <summary>
        /// Add a new item with properties filled by the input controls
        /// </summary>
        public void Add()
        {
            ObservableWorkstationClass.Add(new WorkstationClass
            {
                WcName = "",
                WcType = "",
                WcHandlingType = "",
                SecondaryactivityRef = SecondaryActivityList.SecondaryActivities
            });
            SelectedWorkstationClass = ObservableWorkstationClass.ElementAt(ObservableWorkstationClass.Count - 1);
        }

        /// <summary>
        /// Delete current selected item
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedWorkstationClass() != null)
            {
                MessageBox.Show("This Workstation class is currently attached to a Workstation (" +
                                CheckMatchedWorkstationClass().WName + "). Please:" +
                                " \n\nRemove the Workstation in \"Workstations\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached Wrokstation class to another one");
            }
            else
            {
                ObservableWorkstationClass.Remove(SelectedWorkstationClass);
            }
        }

        /// <summary>
        /// Return true if a matched WorkstationClass is found being used in a item of Workstation list
        /// </summary>
        /// <returns></returns>
        private Workstation CheckMatchedWorkstationClass()
        {
            return
                WorkstationsViewModel.ObservableWorkstation.FirstOrDefault(
                    w => w.WorkstationclassRef.WcName == SelectedWorkstationClass.WcName);
        }
    }
}