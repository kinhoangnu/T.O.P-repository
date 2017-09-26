/*
*  Copyright (c) 2017 Vanderlande Industries
*  All rights reserved.
*
*  The copyright to the computer program(s) herein is the property of
*  Vanderlande Industries. The program(s) may be used and/or copied
*  only with the written permission of the owner or in accordance with
*  the terms and conditions stipulated in the contract under which the
*  program(s) have been supplied.
*/
using System;
using System.Globalization;
using System.Windows.Data;


namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(string), typeof(string))]
    public class PageToStringConverter : StringToStringConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string name = value.GetType().Name;
            name = name.Replace("ViewModel", "").Replace("PageButton", "");
            return base.Convert(name, targetType, parameter, culture);
        }
    }

}
