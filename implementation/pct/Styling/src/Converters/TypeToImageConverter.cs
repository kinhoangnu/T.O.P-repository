using System;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Convert a type to a bitmap.
    /// Use the same assembly that hold the type.
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class TypeToImageConverter : BitmapConverter
    {
        protected override string ConvertToName(object value, ref Assembly assembly)
        {
            Type type = value as Type;
            if (type == null)
            {
                return null;
            }
            if (assembly == null)
            {
                assembly = Assembly.GetAssembly(type);
            }
            return type.Name;
        }
    }

}
