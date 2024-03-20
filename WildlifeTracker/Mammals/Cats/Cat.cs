using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker.Mammals.Cats
{
    public class Cat : Mammal
    {
        #region // Instance variables //

        private string breed;
        private bool isHouseTrained;
        private string favoriteToy;
        private FoodSchedule foodSchedule;
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

        #region // Constructor //
        public Cat(int numOfTeeth, bool hasFurOrHair) : base(numOfTeeth, hasFurOrHair)
        {
        }
        #endregion

        #region // Methods //
        public static void AddCatSpecificAttributes(Cat cat, StackPanel stackPanel)
        {
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Breed:", cat.Breed.ToString());
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Favorite Toy:", cat.FavoriteToy.ToString());
            AnimalInfoWindow.AddAttributeRow(stackPanel, "Is House Trained:", cat.IsHouseTrained ? "Yes" : "No");
        }
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }
        #endregion
    }
}