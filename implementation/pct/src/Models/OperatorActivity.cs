using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using com.vanderlande.wpf;

namespace Your
{
    public class OperatorActivity : ContentViewModel
    {
        private string uuid;
        private string unit;
        private bool useCustomFractional;
        private bool useCustomOffset;
        private bool useCustomBoundaries;
        
        private Process processRef;
        private Operator operatorRef;
        private ObservableCollection<Process> _observableProcess;
        private ObservableCollection<Operator> _observableOperator;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public bool UseCustomOffset
        {
            get { return useCustomOffset; }
            set { useCustomOffset = value; }
        }
        public bool UseCustomBoundaries
        {
            get { return useCustomBoundaries; }
            set { useCustomBoundaries = value; }
        }
        public bool UseCustomFractional
        {
            get { return useCustomFractional; }
            set { useCustomFractional = value; }
        }

        public Process ProcessRef
        {
            get { return processRef; }
            set { processRef = value; }
        }

        public Operator OperatorRef
        {
            get { return operatorRef; }
            set { operatorRef = value; }
        }

        public ObservableCollection<Operator> ObservableOperator
        {
            get { return OperatorList.Operators; }
            set { ChangeProperty(ref _observableOperator, value); }
        }

        public ObservableCollection<Process> ObservableProcess
        {
            get { return ProcessList.Processes; }
            set { ChangeProperty(ref _observableProcess, value); }
        }
        
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }

        public OperatorActivity()
        {
        }
    }
}

