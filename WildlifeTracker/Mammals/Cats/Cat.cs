using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker.Mammals.Cats
{
    public class Cat : Mammal
    {
        #region // Instance variables //

        private string breed;
        private bool isHouseTrained;
        private string favoriteToy;
        #endregion

        #region // Properties //
        public string Breed
        {
            get => breed;
            // Check that the breed is not empty
            set
            {
                if (!string.IsNullOrEmpty(value))
                    breed = value; // user must enter a breed, will get an error if not
            }
        }
        public bool IsHouseTrained { get => isHouseTrained; set => isHouseTrained = value; }
        public string FavoriteToy { 
            get => favoriteToy;
            // Check that the favorite toy is not empty
            set
            {
                if (!string.IsNullOrEmpty(value))
                    favoriteToy = value;
                else //  If the favorite toy is empty, set it to "Unknown", as it is not of high importance i chose this approach
                    favoriteToy = "Unknown";
            }
        }
        #endregion

        #region // Constructors //
        // Default constructor
        public Cat(int numOfTeeth, bool hasFurOrHair) : base(numOfTeeth, hasFurOrHair)
        {

        }

        // Constructor with all parameters that calls the base constructor
       public Cat(string name, int age, bool isDemesticated, GenderType gender, CategoryType category, string color, int numOfTeeth, bool hasFurOrHair, string breed, bool isHouseTrained, string favoriteToy) :
            base(name, age, isDemesticated, gender, category, color, numOfTeeth, hasFurOrHair)
        {
            this.breed = breed;
            this.isHouseTrained = isHouseTrained;
            this.favoriteToy = favoriteToy;
        }
        #endregion

        #region // Methods //
        #endregion
    }
}