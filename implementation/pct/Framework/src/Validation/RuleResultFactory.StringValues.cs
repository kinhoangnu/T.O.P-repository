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
namespace com.vanderlande.wpf
{
    public static partial class RuleResultFactory
    {
        public enum StringErrors
        {
            NotEmpty,
            NotNumeric, 
            NotDecimal,
            TooShort,
            TooLong
        }


        #region String values
        public static RuleResult MayNotBeEmpty(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Valid();
            return Invalid(StringErrors.NotEmpty);
        }

        public static RuleResult MustBeNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return Invalid(StringErrors.NotEmpty);
            long val;
            if (long.TryParse(str, out val) == true)
                return Valid();
            return Invalid(StringErrors.NotNumeric, "Value", str);
        }

        public static RuleResult MustBeDecimal(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return Invalid(StringErrors.NotEmpty);
            double val;
            if (double.TryParse(str, out val) == true)
                return Valid();
            return Invalid(StringErrors.NotDecimal, "Value", str);
        }

        public static RuleResult MustBeLongerThan(this string str, int len)
        {
            if (str.Length > len)
                return Valid();
            return Invalid(StringErrors.TooShort, "Value", str, "Length", len);
        }

        public static RuleResult MustBeShorterThan(this string str, int len)
        {
            if (str.Length < len)
                return Valid();
            return Invalid(StringErrors.TooLong, "Value", str, "Length", len);
        }

        #endregion
    }

}
