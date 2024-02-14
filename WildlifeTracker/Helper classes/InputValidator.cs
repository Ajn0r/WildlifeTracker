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
            bool okInt = int.TryParse(input, out int result);
            if (result >= 0) // check that the number is greater than, or equal to 0
                return okInt; // return the result of the parse
            else // if the number is less than 0, return false no matter the result of the parse
                return false;
        }

        public static bool IsDoubleValid(string input)
        {
            bool okDouble = double.TryParse(input, out double result);
            if (result >= 0) // check that the number is greater than, or equal to 0
                return okDouble; // return the result of the parse
            else // if the number is less than 0, return false no matter the result of the parse
                return false;
        }

        public static void DisplayErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
