using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Animal
    {
        #region // Instance variables //
        private string name;
        private int age;
        private bool isDomesticated;
        private bool isEndangered;
        private GenderType gender;
        private CategoryType category;
        private string color;
        #endregion

        #region // Properties //

        // Property to get and set the gender of the animal
        public GenderType GenderType { get => gender; set => gender = value; }

        // Property to get and set the category type of the animal
        public CategoryType CategoryType { get => category; set => category = value; }

        // Property to get and set the domesticated status of the animal
        public bool IsDomisticated { get => isDomesticated; set => isDomesticated = value; }

        // Property to get and set the endangered status of the animal
        public bool IsEndangered { get => isEndangered; set => isEndangered = value; }

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

        #endregion

        #region // Constructors //
        // Default constructor
        public Animal()
        {
        }

        // Constructor with all parameters
        public Animal(string name, int age, bool isDomesticated, bool isEndangered, GenderType gender, CategoryType category)
        {
            this.name = name;
            this.age = age;
            this.isDomesticated = isDomesticated;
            this.isEndangered = isEndangered;
        }

        // Chainded constructor that sets gender to unknown
        public Animal(string name, int age, bool isDomesticated, bool isEndangered, CategoryType category) : this(name, age, isDomesticated, isEndangered, GenderType.Unknown, category)
        {
            this.name = name;
            this.age = age;
            this.isDomesticated = isDomesticated;
            this.isEndangered = isEndangered;
            this.category = category;
        }
        #endregion
    }
}