using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using com.vanderlande.wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Forms;

namespace Your
{
    class OperatorsViewModel : ContentViewModel
    {
        #region Fields and auto-implement properties
        private Operator selectedOperator;
        private ObservableCollection<Operator> observableOperator;

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        #endregion

        #region Constructor
        public OperatorsViewModel()
        {
            this.DeleteCommand = new RelayCommand((obj) => Delete());
            this.AddCommand = new RelayCommand((obj) => Add());
            OperatorList.Operators = new ObservableCollection<Operator>();
            ObservableOperator = new ObservableCollection<Operator>();
        }
        #endregion

        #region properties
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
            set
            {
                ChangeProperty(ref selectedOperator, value);
            }
        }
        #endregion

        #region Add, Update and Delete

        /// <summary>
        /// Add a new buffer to Operator list
        /// </summary>
        public void Add()
        {
            this.ObservableOperator.Add(new Operator()
            {
            });
        }
        
        /// <summary>
        /// Delete current selected Operator
        /// </summary>
        public void Delete()
        {
            if (CheckMatchedOperator() != null)
            {
                MessageBox.Show("This Operator is currently attached to a Process ("+CheckMatchedOperator().PcName+"). Please:" +
                       " \n\nRemove the Process in \"Processes\" tab first" +
                       "\n..Or.." +
                       "\nChange the attached buffer to another one");
            }
            else
            {
                this.ObservableOperator.Remove(this.SelectedOperator);
            }
        }

        /// <summary>
        /// Return true if a matched Operator is found being used in a item of Process list
        /// </summary>
        /// <returns></returns>
        private Process CheckMatchedOperator()
        {
            //if (ProcessList.Processes != null)
            //{
            //    foreach (Process p in ProcessesViewModel.ObservableProcess)
            //    {
            //        if ((p.InbufferRef.B_name == SelectedOperator.B_name) ||
            //            (p.OutbufferRef.B_name == SelectedOperator.B_name))
            //        {
            //            return p;
            //        }
            //    }
            //    return null;
            //}
            //else
            return null;
        }

        #endregion


    }
}
