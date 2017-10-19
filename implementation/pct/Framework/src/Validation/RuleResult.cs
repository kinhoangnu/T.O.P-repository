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
using MvvmValidation;
using System;

namespace com.vanderlande.wpf
{
    public class RuleResult : MvvmValidation.RuleResult
    {
        public new static RuleResult Assert(bool condition, string errorMessage)
        {
            return condition ? new RuleResult() : new RuleResult(errorMessage);
        }

		public new static RuleResult Invalid(string error)
		{
			return new RuleResult(error);
		}

        public new static RuleResult Valid()
        {
            return new RuleResult();
        }


        private RuleResult()
        {}


        private RuleResult(string str)
        {
            AddError(str);
        }
    }

}
