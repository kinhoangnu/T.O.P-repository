/*
*  Copyright (c) 2015 Vanderlande Industries
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
using System.Reflection;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Class to convert a value (of any type) to a bitmap.
    /// It requires a ConvertToPath method which returns a string and the assembly it can find the bitmap resource.
    /// </summary>
    public abstract class BitmapConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Assembly assembly = null;
                string name = value == null ? null : ConvertToName(value, ref assembly);
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                string pack = string.Format("pack://application:,,,/{0};component/src/Images/", assembly.GetName());
                if (parameter != null)
                {
                    pack += parameter + "/";
                }
                pack += name + ".png";
                Uri uri = new Uri(pack, UriKind.Absolute);
                return new BitmapImage(uri);

            }
            catch (Exception)
            {}
            return null;
        }

        protected abstract string ConvertToName(object value, ref Assembly assem);

    }

}
