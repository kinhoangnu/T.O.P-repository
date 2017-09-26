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
using System.Globalization;

namespace com.vanderlande.wpf
{
    public static class ResourceKeyToStringHelper
    {
        public static string ToResourceString(this string value, string parameter = null)
        {
            StringToStringConverter conv = new StringToStringConverter();
            return conv.Convert(value, typeof(string), parameter, CultureInfo.CurrentCulture) as string;
        }

        public static string ToResourceString(this Enum value, string parameter = null)
        {
            StringToStringConverter conv = new StringToStringConverter();
            return conv.Convert(value, typeof(string), parameter, CultureInfo.CurrentCulture) as string;
        }

    }
}
