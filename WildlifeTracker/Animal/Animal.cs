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
        private GenderType gender;
        private CategoryType category;
        private string color;
        private string ID;
        private static int nextIDNumber; // Static variable to keep track of the next ID number
        #endregion

        #region // Properties //
        
        // Property to get and set the gender of the animal
        public GenderType GenderType { get => gender; set => gender = value; }

        // Property to get and set the category type of the animal
        public CategoryType CategoryType { get => category; set => category = value; }

        // Property to get and set the domesticated status of the animal
        public bool IsDomesticated { get => isDomesticated; set => isDomesticated = value; }

        // Prperty to get and setthe name of the animal
        public string Name
        {
            // Get the name
            get => name;
            set
            { // Check if the value is not null or empty, and then set the name
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

        // Property to get and set the color of the animal
        public string Color { 
            get => color;
            set { // Check if the value is not null or empty, and then set the color
                if (!string.IsNullOrEmpty(value))
                    color = value;
                else // If the value is null or empty, set the color to unknown
                    color = "Unknown";
            }
        }

        // Property to get the ID of the animal, no set method because the ID is generated in the constructor and should not be changed
        public string GetID { get => ID;}
        #endregion

        #region // Constructors //
        // Default constructor
        public Animal()
        {
        }
        public Animal(string typePrefix)
        {
            // Generate the ID based on the type of animal, type is sent from the derived class constructor
            ID = GenerateID(typePrefix);
        }

        // Constructor with all parameters
        public Animal(string typePrefix, string name, int age, bool isDomesticated, GenderType gender, CategoryType category, string color)
        {
            // Generate the ID based on the type of animal, type is sent from the derived class constructor
            ID = GenerateID(typePrefix);
            this.name = name;
            this.age = age;
            this.isDomesticated = isDomesticated;
            this.gender = gender;
            this.category = category;
            this.color = color;
        }

        // Chainded constructor that sets gender to unknown
        public Animal(string typePrefix, string name, int age, bool isDomesticated,  CategoryType category, string color) : 
            this(typePrefix, name, age, isDomesticated, GenderType.Unknown, category, color)
        {
        }
        #endregion

        #region // Methods //
        // Method to generate the ID for the animal based on the type of animal, might refactor and move to a utility class/ interface later
        private string GenerateID(string typePrefix)
        {
            // Set the ID to the prefix and the next ID number with a 3 digit format
            string id = $"{typePrefix}{nextIDNumber:D3}";
            // Increment the next ID number 
            nextIDNumber++;
            // Return the ID
            return id;
        }
        #endregion
    }
}