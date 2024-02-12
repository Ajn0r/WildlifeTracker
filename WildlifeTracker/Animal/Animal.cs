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
        private string id;
        private string imagePath;
        private static int nextIDNumber = 1; // Static variable to keep track of the next ID number
        #endregion

        #region // Properties //

        // Property to get the type/species of animal, I use this instead of the "GetAnimalSpecificData"
        // method suggested in the help document with typeOf method.
        // GetType() will return the type of the derived class and .Name will return the name of the class.
        // I then use databinding to display the type of animal in the UI with the property.
        public string AnimalType => GetType().Name; 

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

        // Property to get and set the image path of the animal
        public string ImagePath { get => imagePath; set => imagePath = value; }

        // Property to get the ID of the animal, no set method because the ID is generated in the constructor and should not be changed
        public string ID { get => id; }
        #endregion

        #region // Constructors //
        public Animal(string typePrefix, CategoryType category)
        {
            // Generate the ID based on the type of animal, type is sent from the derived class constructor
            id = GenerateID(typePrefix);
            // Set the category of the animal
            this.category = category;
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