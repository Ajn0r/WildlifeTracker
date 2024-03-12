using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker
{
    public class Owl : Bird
    {
        #region // Instance variables //
        private string species;
        private bool isNocturnal;
        private FoodSchedule foodSchedule;
        #endregion

        #region // Properties //
        public string Species { get => species; set => species = value; }
        public bool IsNocturnal { get => isNocturnal; set => isNocturnal = value; }
        #endregion

        #region // Constructors //
        public Owl(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
            SetFoodSchedule();
        }
        #endregion

        #region // Methods //
        /// <summary>
        /// Method to add the owl specific information to the animal info window
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="animalInfoStack"></param>
        public static void AddOwlSpecificAttributes(Owl animal, StackPanel animalInfoStack)
        {
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Species", animal.Species);
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Is Nocturnal", animal.IsNocturnal ? "Yes" : "No");
        }

        /// <summary>
        /// Method that returns the food schedule for the owl
        /// </summary>
        /// <returns></returns>
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }

        /// <summary>
        /// Method to set the food schedule for the owl
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Add("Morning: Mice");
            foodSchedule.Add("Lunch: Mice");
            foodSchedule.Add("Dinner: Mice");
        }
        #endregion
    }
}