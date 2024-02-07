using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WildlifeTracker.Helper_classes
{
    internal class CategoryToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the selected category to visibility
            if (value is CategoryType category)
            {
                // If the parameter is not null and is Mammal, then return visibility as visable if the category is Mammal
                if (parameter != null && parameter.ToString() == "Mammal")
                {
                    return category == CategoryType.Mammal ? Visibility.Visible : Visibility.Hidden;
                } // If the parameter is not null and is Bird, then return visibility as visable if the category is Bird
                else if (parameter != null && parameter.ToString() == "Bird")
                {
                    return category == CategoryType.Bird ? Visibility.Visible : Visibility.Hidden;
                }
            } // If the value is not a category, then return hidden
            return Visibility.Hidden;
        }

        // Convert back is not implemented and not needed at this stage
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
