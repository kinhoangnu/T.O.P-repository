/*
*  Copyright (c) 2014 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*  
*/
using System;
using System.Windows.Input;

namespace com.vanderlande.wpf
{

    /// <summary>
    /// ICommand implementation using Relay command pattern.
    ///
    /// Usage:
    ///     public ICommand YourCommand { get; private set; }
    ///     ..
    ///     YourCommand = new RelayCommand(OnYourCommandExecute, OnYourCommandCanExecute)
    ///     ..
    ///     private void OnYourCommandExecute() {..}
    ///     private bool OnYourCommandCanExecute() {..; return true; }
    ///
    /// In XAML:
    ///     <Button Command="{Binding YourCommand}" ... />
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private bool _enabled = true;
        #endregion

        #region Properties
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;

                if (CanExecuteChanged != null)
                {
                    // Only change the variable when CanExecuteChanged is not null.
                    // Otherwise a difference can be created between UI and this property.
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }
        
        #endregion
        
        #region Events
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Methods
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {}

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
            Invalidate();
        }


        public bool CanExecute(object parameter)
        {
            return ((_enabled == true) && ((_canExecute == null || _canExecute(parameter))));
        }


        public void Execute(object parameter)
        {
            string cmd = _execute.Method.Name;
            string par = (parameter == null) ? "" : parameter.ToString();
            string from = (_execute.Method.DeclaringType == null) ? "" : _execute.Method.DeclaringType.Name;
            try
            {
                _execute(parameter);
            }
            catch (Exception)
            {
                Logger.LogCommand(cmd, par, from, false);
                throw;
            }
            Logger.LogCommand(cmd, par, from, true);
        }
        

        public void Invalidate(object parameter = null)
        {
            if (_canExecute != null)
            {
                Enabled = _canExecute(parameter);
            }
        }

        #endregion
    }
}
