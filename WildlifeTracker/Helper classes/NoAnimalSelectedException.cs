using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WildlifeTracker.Helper_classes
{
    /// <summary>
    /// A custom exception class for when no animal is selected
    /// </summary>
    public class NoAnimalSelectedException : Exception
    {
        public NoAnimalSelectedException() : base("No animal selected")
        {
        }
        public NoAnimalSelectedException(string message) : base(message) 
        {
        }

    }
}
