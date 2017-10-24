using System;
using System.Collections.ObjectModel;
using com.vanderlande.wpf;

namespace Your
{
    public class OperatorActivity : ContentViewModel
    {
        private ObservableCollection<Process> observableProcess;
        private ObservableCollection<Operator> observableOperator;

        public string Unit { get; set; }

        public bool UseCustomOffset { get; set; }

        public bool UseCustomBoundaries { get; set; }

        public bool UseCustomFractional { get; set; }

        public Process ProcessRef { get; set; }

        public Operator OperatorRef { get; set; }

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

        public OperatorActivity()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }
}