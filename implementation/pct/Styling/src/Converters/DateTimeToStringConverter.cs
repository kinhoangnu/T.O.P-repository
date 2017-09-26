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

namespace com.vanderlande.wpf
{
    public class DateTimeToStringConverter: BaseConverter
    {
        public static TimeZoneInfo TimeZone = TimeZoneInfo.Local;
        public static bool AddMilliseconds = false;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = (DateTime) value;
            DateTime dtZone = TimeZoneInfo.ConvertTime(dt, TimeZone);
            string result = string.Format("{0:d} {0:T}", dtZone);
            if (AddMilliseconds)
                result += string.Format(".{0:D3}", dtZone.Millisecond);
            return result;
        }
    }
}
