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
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Converter to convert an enumeration to a bitmap (name)
    /// The derived classes should supply a valid assembly.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(BitmapImage))]
    public class EnumToImageConverter : BitmapConverter
    {
        protected override string ConvertToName(object value, ref Assembly assembly)
        {
            if (value == null)
            {
                return null;
            }
            if (assembly == null)
            {
                assembly = Assembly.GetAssembly(GetType());
            }
            Type type = value.GetType();
            return type.Name + "." + value;
        }
    }
}
