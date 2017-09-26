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
using System.Globalization;
using System.Windows.Data;

namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntegerToBoolConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int param = int.Parse((string)parameter);
            return param == (int)value;
        }
    }
}
