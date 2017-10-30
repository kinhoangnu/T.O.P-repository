using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using com.vanderlande.wpf;

namespace Your
{
    internal class WorkstationGroupViewModel : ContentViewModel
    {
        private static ObservableCollection<WorkstationGroup> observableWorkstationGroup;
        private WorkstationGroup selectedWorkstationGroup;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        /// <summary>
        /// Item that is being selected on the list
        /// </summary>
        public WorkstationGroup SelectedWorkstationGroup
        {
            get { return selectedWorkstationGroup; }
            set { ChangeProperty(ref selectedWorkstationGroup, value); }
        }

        public static ObservableCollection<WorkstationGroup> ObservableWorkstationGroup
        {
            get { return WorkstationGroupList.WorkstationGroups; }
            set { observableWorkstationGroup = value; }
        }

        public WorkstationGroupViewModel()
        {
            DeleteCommand = new RelayCommand(obj => Delete());
            AddCommand = new RelayCommand(obj => Add());
            WorkstationGroupList.WorkstationGroups = new ObservableCollection<WorkstationGroup>();
            ObservableWorkstationGroup = WorkstationGroupList.GetWorkstationGroupList();
        }

        /// <summary>
        /// Add a new item with properties on the input controls
        /// </summary>
        public void Add()
        {
            ObservableWorkstationGroup.Add(new WorkstationGroup
            {
                WgName = "",
                WgDescription = ""
            });
        }

        /// <summary>
        /// Delete the current selected on the list
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedWorkstationGroup() != null)
            {
                MessageBox.Show("This Workstation group (" + CheckMatchedWorkstationGroup().WorkstationgroupRef.WgName +
                                ") is currently attached to a Workstation (" + CheckMatchedWorkstationGroup().WName +
                                "). Please:" +
                                " \n\nRemove the Workstation in \"Workstations\" tab first" +
                                "\n..Or.." +
                                "\nChange the attached Wrokstation class to another one");
            }
            else
            {
                ObservableWorkstationGroup.Remove(SelectedWorkstationGroup);
            }
        }

        /// <summary>
        /// Return true if a matched WorkstationGroup is found being used in a item of Workstation list
        /// </summary>
        /// <returns></returns>
        private Workstation CheckMatchedWorkstationGroup()
        {
            return
                WorkstationsViewModel.ObservableWorkstation.FirstOrDefault(
                    w => w.WorkstationgroupRef.WgName == SelectedWorkstationGroup.WgName);
        }
    }
}