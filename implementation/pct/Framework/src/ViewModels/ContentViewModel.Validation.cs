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
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MvvmValidation;


namespace com.vanderlande.wpf
{
    // Properties and methods of Validation in the ContentViewModel
    public partial class ContentViewModel 
    {
        // Override Error property to process validation in a Composite ContentViewModel (Parent/Child)
        public override string Error
        {
            get
            {
                return GetValidationResult().ToString();
            }
            protected set
            {
                RaisePropertyChanged();
                if (Parent != null)
                    Parent.Error = value;
            }
        }

        // Override HasErrors to process validation in a Composite ContentViewModel (Parent/Child)
        public override bool HasErrors
        {
            get
            {
                if (base.HasErrors == true)
                    return true;
                return _childViewModels.Any(cv => cv.HasErrors);
            }
            protected set
            {
                RaisePropertyChanged();
                if (Parent != null)
                    Parent.HasErrors = value;
            }
        }

        // Expose each error seperately
        public ObservableCollection<ValidationError> ErrorList { get; private set; }
        

        private void ValidateAll()
        {
            foreach (ContentViewModel cv in _childViewModels)
            {
                cv.Validator.ValidateAll();
            }
            Validator.ValidateAll();
        }


        private void InitializeValidation()
        {
            ErrorList = new ObservableCollection<ValidationError>();
            ErrorsChanged += UpdateErrorList;
        }


        private void UpdateErrorList(object sender, DataErrorsChangedEventArgs e)
        {
            ValidationResult result = GetValidationResult();
            List<ValidationError> errList = result.ErrorList.ToList();
            foreach (ValidationError ve in ErrorList.ToList())
            {
                if (errList.Contains(ve) == false)
                    ErrorList.Remove(ve);
            }
            foreach (ValidationError ve in errList)
            {
                if (ErrorList.Contains(ve) == false)
                    ErrorList.Add(ve);
            }
            if (Parent != null)
                Parent.UpdateErrorList(sender, e);
        }


        private ValidationResult GetValidationResult()
        {
            ValidationResult result = Validator.GetResult();
            foreach (ContentViewModel cv in _childViewModels)
            {
                result = result.Combine(cv.GetValidationResult());
            }
            return result;
        }
    }
}
