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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Convert one (or more) code string/enum (key) to resource string(s).
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class StringToStringConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(IEnumerable)) // Also support an array of strings.
            {
                return Convert(value as IEnumerable, targetType, parameter, culture);
            }

            if (value == null)
            {
                return string.Empty;
            }

            var str = ConvertFromEnum(value, targetType, parameter, culture);
            if (str != null)
            {
                return str;
            }

            str = value.ToString();
            if (parameter != null)
            {
                str += parameter;
            }

            return ConvertToString(str);
        }

        private object Convert(IEnumerable values, Type targetType, object parameter, CultureInfo culture)
        {
            var list = new List<string>();
            foreach (var obj in values)
            {
                list.Add(Convert(obj, typeof(string), parameter, culture) as string);
            }
            return list;
        }

        // If object is an Enumeration, prefix it with the typename.
        // Also check if it is a flagged enum and process each value.
        // The converted values are separated by a comma.
        private string ConvertFromEnum(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType().IsEnum == false)
            {
                return null;
            }
            var result = string.Empty;
            var str = value.ToString();
            foreach (var sub in str.Split(','))
            {
                var key = value.GetType().Name + "." + sub.Trim();
                if (result != string.Empty)
                {
                    result += ", ";
                }
                result += Convert(key as object, targetType, parameter, culture);
            }
            return result;
        }

        private string ConvertToString(string key)
        {
            var resource = Application.Current.TryFindResource(key);
            if (resource != null)
            {
                return resource.ToString();
            }

            // Log that a translation doesn't exist.
            var language =
                Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                    x => x.Source.ToString().Contains("Language"));
            var languageFile = language == null
                ? "(unknown language file)"
                : language.Source.ToString().Split('/').Last();
            Logger.LogError(string.Format("Internal value {0} does not have a translation for language {1}", key,
                languageFile));

            return "\"" + key + "\"";
        }
    }
}