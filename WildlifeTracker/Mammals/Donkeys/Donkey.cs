using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker.Mammals.Donkeys
{
    public class Donkey : Mammal
    {
        #region // Instance variables //
        private double maxLoad;
        private bool isUsedAsPackAnimal;
        private double height;
        private double weight;
        #endregion

        #region // Properties //
        public double MaxLoad { get => maxLoad; set => maxLoad = value; }
        public bool IsUsedAsPackAnimal { get => isUsedAsPackAnimal; set => isUsedAsPackAnimal = value; }
        public double Height { get => height; set => height = value; }
        public double Weight { get => weight; set => weight = value; }
        #endregion

        #region // Constructor //
        public Donkey(int numOfTeeth, bool hasFurOrHair) : base(numOfTeeth, hasFurOrHair)
        {
        }

        #endregion

        #region // Methods //

        /// <summary>
        /// Method to add the donkey specific information to the animal info window
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="animalInfoStack"></param>
        public static void AddDonkeySpecificAttributes(Donkey animal, StackPanel animalInfoStack)
        {
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Height", animal.Height.ToString());
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Weight", animal.Weight.ToString());
            // Set the value of the used as pack animal check box as a string, yes or no
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Used as pack animal", animal.IsUsedAsPackAnimal ? "Yes" : "No");
            if (animal.IsUsedAsPackAnimal) // If the donkey is used as a pack animal, add the max load information
                AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Max load", animal.MaxLoad.ToString());
        }
        #endregion
    }
}