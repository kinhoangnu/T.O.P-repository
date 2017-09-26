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
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Group any number of converters that convert a value one by one, like a pipe or chain.
    /// Based on: https://www.codeproject.com/Articles/15061/Piping-Value-Converters-in-WPF
    /// 
    /// Usage:
    ///         <wpf:GroupConverter x:Key="ConvertAandB">
    ///            <abc:ConverterA />
    ///            <def:ConverterB />
    ///         </wpf:GroupConverter>
    ///
    /// </summary>
    public class GroupConverter : List<IValueConverter>, IValueConverter
    {
        private readonly Dictionary<IValueConverter, ValueConversionAttribute>
            _conversionAttributes = new Dictionary<IValueConverter, ValueConversionAttribute>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object output = value; 
            for (int i = 0; i < Count; ++i)
            {
                IValueConverter converter = this[i];
                Type currentTargetType = GetTargetType(i, targetType, true);
                output = converter.Convert( output, currentTargetType, parameter, culture );
                if (output == Binding.DoNothing)     // If the converter returns 'DoNothing' then the binding operation should terminate.
                {
                    return Binding.DoNothing;
                }
            } 
            return output;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object output = value;
            for (int i = Count - 1; i >= 0; --i)
            {
                IValueConverter converter = this[i];
                Type currentTargetType = GetTargetType(i, targetType, false);
                output = converter.ConvertBack(output, currentTargetType, parameter, culture);
                if (output == Binding.DoNothing)    // If the converter returns 'DoNothing' then the binding operation should terminate.
                {
                    return Binding.DoNothing;
                }
            }
            return output;
        }


        /// <summary>
        /// Get the target type from the adjecent converter as it is defined by its attribute.
        /// </summary>
        private Type GetTargetType(int index, Type finalTargetType, bool takeNext)
        {
            IValueConverter nextConverter = GetAdjecentConverter(index, takeNext);
            if (nextConverter == null)           // If the current converter is the last one to be executed return the target type
            {
                return finalTargetType;
            }
            ValueConversionAttribute attr = GetAttributes(nextConverter);
                                                // If the Convert method is going to be called, we need to use the SourceType of the next 
                                                // converter in the list.  If ConvertBack is called, use the TargetType.
            return takeNext ? attr.SourceType : attr.TargetType;
        }


        /// <summary>
        /// If the current converter is not the last/first in the list, get a reference to the next/previous converter.
        /// </summary>
        private IValueConverter GetAdjecentConverter(int index, bool takeNext)
        {
            if ((takeNext) && (index < Count - 1))
            {
                return this[index + 1];
            }
            if ((!takeNext) && (index > 0))
            {
                return this[index - 1];
            }
            return null;
        }


        private ValueConversionAttribute GetAttributes(IValueConverter conv)
        {
            if (!_conversionAttributes.ContainsKey(conv))
            {
                object[] attributes = conv.GetType().GetCustomAttributes(typeof(ValueConversionAttribute), false);
                ValueConversionAttribute attr = (attributes.Length == 1)
                    ? attributes[0] as ValueConversionAttribute
                    : new ValueConversionAttribute(typeof(object), typeof(object));
                _conversionAttributes.Add(conv, attr);
            }
            return _conversionAttributes[conv];
        }

    }

}
