using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Cat : Mammal
    {
        #region // Instance variables //

        private string breed;
        private bool isHouseTrained;
        private string favoriteToy;
        #endregion

        #region // Properties //
        public string Breed { get => breed; set => breed = value; }
        public bool IsHouseTrained { get => isHouseTrained; set => isHouseTrained = value; }
        public string FavoriteToy { get => favoriteToy; set => favoriteToy = value; }
        #endregion

        #region // Constructors //

        #endregion

        #region // Methods //
        #endregion
    }
}