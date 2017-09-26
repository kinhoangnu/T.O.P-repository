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

using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(CultureInfo), typeof(BitmapImage))]
    public class CultureInfoToImageConverter : BitmapConverter
    {
        protected override string ConvertToName(object value, ref Assembly assembly)
        {
            CultureInfo ci = value as CultureInfo;
            if (ci == null)
            {
                return null;
            }
            if (assembly == null)
            {
                assembly = Assembly.GetAssembly(GetType());
            }
            // Use the last tokens to determine the country.
            string[] fields = ci.Name.Split('-');
            return fields[fields.Count()-1];
        }
    }

}
