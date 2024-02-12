using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WildlifeTracker.Helper_classes
{
    /// <summary>
    /// Helper class to convert a boolean value to "Yes" or "No" and the other way around
    /// Is inspired by this solution: https://wpf-tutorial.com/data-binding/value-conversion-with-ivalueconverter/
    /// </summary>
    public class BooleanToYesNoConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to "Yes" or "No"
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                    return "Yes";
                else
                    return "No";
            }
            return "No";
        }

        /// <summary>
        /// Converts a string value to a boolean value, "Yes" to true and "No" to false
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if ((string)value == "Yes")
                    return true;
                else
                    return false;
            }
            return false;
        }

    }
}
