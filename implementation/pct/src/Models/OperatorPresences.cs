using System;
using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class OperatorPresences : ContentViewModel
    {
        private ObservableCollection<Process> observableProcess;
        private ObservableCollection<Operator> observableOperator;
        private ObservableCollection<WorkstationClass> observableWorkstationClass;

        public string Unit { get; set; }

        public Process ProcessRef { get; set; }

        public Operator OperatorRef { get; set; }

        public WorkstationClass WorkstationClassRef { get; set; }

        public ObservableCollection<WorkstationClass> ObservableWorkstationClass
        {
            get { return WorkstationClassList.WorkstationClasses; }
            set { ChangeProperty(ref observableWorkstationClass, value); }
        }

        public ObservableCollection<Operator> ObservableOperator
        {
            get { return OperatorList.Operators; }
            set { ChangeProperty(ref observableOperator, value); }
        }

        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { ChangeProperty(ref observableProcess, value); }
        }

        public string Uuid { get; set; }

        public OperatorPresences()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}