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
        #endregion

        #region // Properties //
        public string FavoriteFish { get => favoriteFish; set => favoriteFish = value; }
        public bool CanSwim { get => canSwim; set => canSwim = value; }
        #endregion

        #region // Constructors //
        public Penguin(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
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

        #endregion
    }
}