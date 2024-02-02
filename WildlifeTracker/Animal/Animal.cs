using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Animal
    {
        // Instance variables
        private string name;
        private int age;
        private bool isDomisticated;
        private bool isEndangered;
        private GenderType gender;
        private CategoryType category;

        // Property to get and set the gender of the animal
        public GenderType GenderType
        {
            // Get the gender
            get => gender;
            // Set the gender
            set => gender = value;
        }

        // Property to get and set the category type of the animal
        public CategoryType CategoryType
        {
            // get the category type
            get => category;
            // Set the category type
            set => category = value;
        }

        // Prperty to get and setthe name of the animal
        public string Name
        {
            // Get the name
            get => name;
            set
            {
                // Check if the value is not null or empty, and then set the name
                if (!string.IsNullOrEmpty(value))
                    name = value;
            }
        }

        // Property to get and set the age of the animal
        public int Age
        {
            get => age;
            set
            {
                if (value > 0)
                    age = value;
            }
        }

        public bool IsDomisticated { get => isDomisticated; set => isDomisticated = value; }
        public bool IsEndangered { get => isEndangered; set => isEndangered = value; }
    }
}