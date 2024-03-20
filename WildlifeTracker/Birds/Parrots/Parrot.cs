using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker
{
    public class Parrot : Bird
    {
        #region // Instance variables //
        private string favoritePhrase;
        private bool canSpeak;
        private string species;
        #endregion

        #region // Properties //
        public string FavoritePhrase { get => favoritePhrase; set => favoritePhrase = value; }
        public bool CanSpeak { get => canSpeak; set => canSpeak = value; }
        public string Species { get => species; set => species = value; }
        #endregion

        #region // Constructors //
        public Parrot(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
           
        }
        #endregion

        #region // Methods //
        /// <summary>
        /// Method to add the parrot specific information to the animal info window
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="animalInfoStack"></param>
        public static void AddParrotSpecificAttributes(Parrot animal, StackPanel animalInfoStack)
        {
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Species", animal.Species);
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Can Talk", animal.CanSpeak ? "Yes" : "No");
            AnimalInfoWindow.AddAttributeRow(animalInfoStack, "Favorite Phrase", animal.FavoritePhrase);
        }

        #endregion
    }
}