using System;
using System.Globalization;

namespace com.vanderlande.wpf
{
    public class BaseConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
