using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker
{
    public class Penguin : Bird
    {
        #region // Instance variables //
        private string favoriteFish;
        private bool canSwim;
        private FoodSchedule foodSchedule;
        #endregion

        #region // Properties //
        public string FavoriteFish { get => favoriteFish; set => favoriteFish = value; }
        public bool CanSwim { get => canSwim; set => canSwim = value; }
        #endregion

        #region // Constructors //
        public Penguin(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
            SetFoodSchedule();
        }
        #endregion

        #region // Methods //

        /// <summary>
        /// Method to add the penguin specific information to the animal info window
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="animalInfoStack"></param>
        public static void AddPenguinSpecificAttributes(Penguin animal, StackPanel animalInfoStack)
        {
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Can swim", animal.CanSwim ? "Yes" : "No");
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Favorite fish", animal.FavoriteFish);
        }

        /// <summary>
        /// Method that returns the food schedule for the penguin
        /// </summary>
        /// <returns></returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }

        /// <summary>
        /// Method to set the food schedule for the penguin
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Add("Morning: Fish");
            foodSchedule.Add("Lunch: Krill");
            if (FavoriteFish != null) // if a favorite fish is set, add it to the food schedule
                foodSchedule.Add($"Evening: {FavoriteFish}");
            else // else, just add fish
                foodSchedule.Add("Evening: Fish");
        }
        #endregion
    }
}