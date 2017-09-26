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

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MvvmValidation;

namespace com.vanderlande.wpf
{
    // Derived ValidationHelper to add dedicated methods/properties with suit the VI_WPF framework.
    public class Validation : ValidationHelper
    {
        internal class CheckOnEveryChangeGuard : IDisposable
        {
            private readonly Validation _validator;
            private readonly bool _previous;

            internal CheckOnEveryChangeGuard(Validation val, bool set)
            {
                _validator = val;
                _previous = val.CheckViewModelOnEveryChange;
                val.CheckViewModelOnEveryChange = set;
            }

            public void Dispose()
            {
                _validator.CheckViewModelOnEveryChange = _previous;
            }
        }


        public bool CheckViewModelOnEveryChange { get; set; }

        static Validation()
        {
            CopyrightInfo ci = new CopyrightInfo(Assembly.GetAssembly(typeof (ValidationHelper)));
            ci.Add();
        }

        // Change order of arguments to enable an easy use of no, one or multiple properties for a single rule.
        public IValidationRule AddRule(Func<RuleResult> validateDelegate, params Expression<Func<object>> [] properties)
        {
            if (properties.Any())
                return AddRule(properties, validateDelegate);
            return AddRule(this, validateDelegate);
        }

        public ValidationResult CheckViewModel()
        {
                        // Disable the check on every change to optimize the calls to CheckViewModel in RaisePropetyChanged.
            using (new Validation.CheckOnEveryChangeGuard(this, false))
            {
                return Validate(this);
            }
        }

    }

}
