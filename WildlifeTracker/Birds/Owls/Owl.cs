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
        #endregion

        #region // Properties //
        public string Species { get => species; set => species = value; }
        public bool IsNocturnal { get => isNocturnal; set => isNocturnal = value; }
        #endregion

        #region // Constructors //
        public Owl(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
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

        public override FoodSchedule GetFoodSchedule()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}