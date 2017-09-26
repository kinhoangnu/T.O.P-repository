using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace com.vanderlande.wpf
{
    /// <summary>
    /// Convert a language string to a bitmap.
    /// Use the full language string (en-US).
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class StringToImageConverter : BitmapConverter
    {
        protected override string ConvertToName(object value, ref Assembly assembly)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetAssembly(GetType());
            }
            return value.ToString();
        }
    }

}
