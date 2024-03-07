using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public abstract class Animal : IAnimal, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region // Properties //

        // Property to get the type/species of animal, I use this instead of the "GetAnimalSpecificData"
        // method suggested in the help document with typeOf method.
        // GetType() will return the type of the derived class and .Name will return the name of the class.
        // I then use databinding to display the type of animal in the UI with the property.
        public string AnimalType => GetType().Name; 

        // Property to get and set the gender of the animal
        public GenderType Gender { get => gender; set => gender = value; }

        // Property to get and set the category type of the animal
        public CategoryType Category { 
            get => category;
            set
            { 
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

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

        // Property to get the id of the animal, no set method because the id is generated in the constructor and should not be changed
        public string Id { get => id; set => id = value; }
        #endregion

        #region // Constructors //
        public Animal(CategoryType category)
        {
            // Set the category of the animal
            this.category = category;
        }
        #endregion

        #region // Methods //
        public abstract FoodSchedule GetFoodSchedule();

        public virtual string GetExtraInfo()
        {
            return $"{Id}, {Name}, {Age}, {Color}, {AnimalType}";
        }
        public override string ToString()
        {
            return $"{Id}, {Name}, {Age}, {Color}, {AnimalType}";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}