using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WildlifeTracker.Helper_classes
{
    public class InputValidator
    {
        public static bool IsStringValid(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        public static bool IsNumberValid(string input)
        {
            return int.TryParse(input, out int result);
        }

        public static bool IsDoubleValid(string input)
        {
            return double.TryParse(input, out double result);
        }

        public static void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
