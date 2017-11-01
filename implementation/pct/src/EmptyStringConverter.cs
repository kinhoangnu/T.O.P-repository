using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Your
{
    public class EmptyStringConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return value = "NULL";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            //Exception e = new NotImplementedException();
            //return MessageBox.Show(e.Message);
            return value;
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}