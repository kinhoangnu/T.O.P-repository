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
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class PageToImageConverter : BitmapConverter
    {
        protected override string ConvertToName(object value, ref Assembly assembly)
        {
            if (value == null)
            {
                return null;
            }
            Type type = value.GetType();
            assembly = type.Assembly;
            return type.Name.Replace("PageButton", "");
        }
    }

}
