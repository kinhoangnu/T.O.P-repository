/*
*  Copyright (c) 2016 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*
*  For Validation (IDataErrorInfo and INotifyDataErrorInfo), see https://mvvmvalidation.codeplex.com/
*  
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmValidation;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Base class for all ValidationViewModels
    /// </summary>
    public class ValidationViewModel : ViewModel, IDataErrorInfo, INotifyDataErrorInfo
    {
        #region IDataErrorInfo implementation

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string propertyName]
        {
            get
            {
                return Validator.GetResult(propertyName).ToString();
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this viewmodel.
        /// Set a dummy value to trigger notifications to XAML.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this viewmodel. The default is an empty string ("").</returns>
        public virtual string Error
        {
            get
            {
                return Validator.GetResult().ToString();
            }
            protected set
            {
                base.RaisePropertyChanged();
            }
        }
        #endregion

        #region INotifyDataErrorInfo implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { NotifyDataErrorInfoAdapter.ErrorsChanged += value; }
            remove { NotifyDataErrorInfoAdapter.ErrorsChanged -= value; }
        }


        public virtual bool HasErrors
        {
            get
            {
                return NotifyDataErrorInfoAdapter.HasErrors;
            }
            protected set
            {
                base.RaisePropertyChanged();
            }
        }


        public IEnumerable GetErrors(string propertyName)
        {
            return NotifyDataErrorInfoAdapter.GetErrors(propertyName);
        }


        #endregion

        protected Validation Validator { get; private set; }
        private NotifyDataErrorInfoAdapter NotifyDataErrorInfoAdapter { get; set; }

        protected ValidationViewModel()
        {
            Validator = new Validation();
            NotifyDataErrorInfoAdapter = new NotifyDataErrorInfoAdapter(Validator);
                             // Set a dummy values to trigger RaisePropertyChanged.
            ErrorsChanged += (o, e) => Error = "";
            ErrorsChanged += (o, e) => HasErrors = true;
        }


        // Check if this specific property is valid.
        protected bool IsPropertyValid([CallerMemberName] string id = null)
        {
            if (id == null)
                return true;
            return Validator.GetResult(id).IsValid;
        }


        // Do the validation before the notification RaisePropertyChanged is send.
        protected override void RaisePropertyChanged([CallerMemberName] string id = null)
        {
            if (id == null)
                return;
            Validator.Validate(id);
            if ((Validator.CheckViewModelOnEveryChange == true) && (id != "Error") && (id != "HasErrors"))
                Validator.CheckViewModel();
            base.RaisePropertyChanged(id);
        }


        // Refresh error texts when a language has changed.
        protected override void RefreshProperties()
        {
            // Disable the check on every change to optimize the calls to CheckViewModel in RaisePropetyChanged.
            using (new Validation.CheckOnEveryChangeGuard (Validator, false))
            {
                Validator.ValidateAll();
            }
            base.RefreshProperties();
        }

    }
}
