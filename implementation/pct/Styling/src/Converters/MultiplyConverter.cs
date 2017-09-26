/*
*  Copyright (c) 2017 Vanderlande Industries
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
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(double), typeof(double))]
    public class MultiplyConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val1 = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
                double val2 = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
                return val1*val2; 
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Cannot calculate {0} * {1}", value, parameter == null ? "null" : parameter.ToString()));
                Trace.WriteLine(ex.Message);
            }
            return 1.0;
        }
    }
}
