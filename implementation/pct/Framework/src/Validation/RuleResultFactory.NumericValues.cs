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

namespace com.vanderlande.wpf
{
    public static partial class RuleResultFactory
    {
        public enum NumericErrors
        {
            NotZero,
            SmallerThan, 
            SmallerEqual,
            LargerThan,
            LargerEqual,
            NotEqualTo,
            BetweenIncludingIncluding,
            BetweenLargeSmaller,
            BetweenLargerIncluding,
            BetweenIncludingSmaller,
            OutsideIncludingIncluding,
            OutsideLargeSmaller,
            OutsideSmallerIncluding,
            OutsideIncludingLarger
        }


        #region Numeric values

        public static RuleResult MayNotBeZero(this long val)
        {
            if (val == 0)
                return Valid();
            return Invalid(NumericErrors.NotZero);
        }

        public static RuleResult MustBeSmallerThan(this long val, long max)
        {
            if (val < max)
                return Valid();
            return Invalid(NumericErrors.SmallerThan, "Value", val, "Max", max);
        }

        public static RuleResult MustBeSmallerEqualThan(this long val, long max)
        {
            if (val <= max)
                return Valid();
            return Invalid(NumericErrors.SmallerEqual, "Value", val, "Max", max);
        }

        public static RuleResult MustBeLargerThan(this long val, long min)
        {
            if (val > min)
                return Valid();
            return Invalid(NumericErrors.LargerThan, "Value", val, "Min", min);
        }

        public static RuleResult MustBeLargerEqualThan(this long val, long min)
        {
            if (val >= min)
                return Valid();
            return Invalid(NumericErrors.LargerEqual, "Value", val, "Min", min);
        }

        public static RuleResult MayNotBeEqualTo(this long val, long val2)
        {
            if (val == val2)
                return Valid();
            return Invalid(NumericErrors.NotEqualTo, "Value", val, "Value2", val2);
        }

        public static RuleResult MustBeBetweenIncludingIncluding(this long val, long min, long max)
        {
            if ((val >= min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargeSmaller(this long val, long min, long max)
        {
            if ((val > min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargerIncluding(this long val, long min, long max)
        {
            if ((val > min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenIncludingSmaller(this long val, long min, long max)
        {
            if ((val >= min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingIncluding(this long val, long min, long max)
        {
            if ((val <= min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerLarger(this long val, long min, long max)
        {
            if ((val < min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerIncluding(this long val, long min, long max)
        {
            if ((val < min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideSmallerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingLarger(this long val, long min, long max)
        {
            if ((val <= min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingLarger, "Value", val, "Min", min, "Max", max);
        }

        #endregion

        #region Unsigned values

        public static RuleResult MayNotBeZero(this ulong val)
        {
            if (val == 0)
                return Valid();
            return Invalid(NumericErrors.NotZero);
        }

        public static RuleResult MustBeSmallerThan(this ulong val, ulong max)
        {
            if (val < max)
                return Valid();
            return Invalid(NumericErrors.SmallerThan, "Value", val, "Max", max);
        }

        public static RuleResult MustBeSmallerEqualThan(this ulong val, ulong max)
        {
            if (val <= max)
                return Valid();
            return Invalid(NumericErrors.SmallerEqual, "Value", val, "Max", max);
        }

        public static RuleResult MustBeLargerThan(this ulong val, ulong min)
        {
            if (val > min)
                return Valid();
            return Invalid(NumericErrors.LargerThan, "Value", val, "Min", min);
        }

        public static RuleResult MustBeLargerEqualThan(this ulong val, ulong min)
        {
            if (val >= min)
                return Valid();
            return Invalid(NumericErrors.LargerEqual, "Value", val, "Min", min);
        }

        public static RuleResult MayNotBeEqualTo(this ulong val, ulong val2)
        {
            if (val == val2)
                return Valid();
            return Invalid(NumericErrors.NotEqualTo, "Value", val, "Value2", val2);
        }

        public static RuleResult MustBeBetweenIncludingIncluding(this ulong val, ulong min, ulong max)
        {
            if ((val >= min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargeSmaller(this ulong val, ulong min, ulong max)
        {
            if ((val > min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargerIncluding(this ulong val, ulong min, ulong max)
        {
            if ((val > min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenIncludingSmaller(this ulong val, ulong min, ulong max)
        {
            if ((val >= min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingIncluding(this ulong val, ulong min, ulong max)
        {
            if ((val <= min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerLarger(this ulong val, ulong min, ulong max)
        {
            if ((val < min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerIncluding(this ulong val, ulong min, ulong max)
        {
            if ((val < min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideSmallerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingLarger(this ulong val, ulong min, ulong max)
        {
            if ((val <= min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingLarger, "Value", val, "Min", min, "Max", max);
        }

        #endregion

        #region Double values

        public static RuleResult MayNotBeZero(this double val, double eps = 0.0)
        {
            if (Math.Abs(val) > Math.Abs(eps))
                return Valid();
            return Invalid(NumericErrors.NotZero);
        }

        public static RuleResult MustBeSmallerThan(this double val, double max)
        {
            if (val < max)
                return Valid();
            return Invalid(NumericErrors.SmallerThan, "Value", val, "Max", max);
        }

        public static RuleResult MustBeSmallerEqualThan(this double val, double max)
        {
            if (val <= max)
                return Valid();
            return Invalid(NumericErrors.SmallerEqual, "Value", val, "Max", max);
        }

        public static RuleResult MustBeLargerThan(this double val, double min)
        {
            if (val > min)
                return Valid();
            return Invalid(NumericErrors.LargerThan, "Value", val, "Min", min);
        }

        public static RuleResult MustBeLargerEqualThan(this double val, double min)
        {
            if (val >= min)
                return Valid();
            return Invalid(NumericErrors.LargerEqual, "Value", val, "Min", min);
        }

        public static RuleResult MayNotBeEqualTo(this double val, double val2, double eps = 0.0)
        {
            if (Math.Abs(val - val2) > Math.Abs(eps))
                return Valid();
            return Invalid(NumericErrors.NotEqualTo, "Value", val, "Value2", val2);
        }

        public static RuleResult MustBeBetweenIncludingIncluding(this double val, double min, double max)
        {
            if ((val >= min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargeSmaller(this double val, double min, double max)
        {
            if ((val > min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenLargerIncluding(this double val, double min, double max)
        {
            if ((val > min) && (val <= max))
                return Valid();
            return Invalid(NumericErrors.BetweenLargerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeBetweenIncludingSmaller(this double val, double min, double max)
        {
            if ((val >= min) && (val < max))
                return Valid();
            return Invalid(NumericErrors.BetweenIncludingSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingIncluding(this double val, double min, double max)
        {
            if ((val <= min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerLarger(this double val, double min, double max)
        {
            if ((val < min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideLargeSmaller, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideSmallerIncluding(this double val, double min, double max)
        {
            if ((val < min) || (val >= max))
                return Valid();
            return Invalid(NumericErrors.OutsideSmallerIncluding, "Value", val, "Min", min, "Max", max);
        }

        public static RuleResult MustBeOutsideIncludingLarger(this double val, double min, double max)
        {
            if ((val <= min) || (val > max))
                return Valid();
            return Invalid(NumericErrors.OutsideIncludingLarger, "Value", val, "Min", min, "Max", max);
        }

        #endregion
    }

}
