/*
*  Copyright (c) 2015 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/

using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Windows.Data;


namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class CollapseIfEmptyConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(IEnumerable))
            {
                return (value as IEnumerable).Cast<object>().Any() ? Visibility.Visible : Visibility.Collapsed;
            }

            if (value == null)
            {
                return Visibility.Collapsed;
            }
            string str = value.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
    }
}
