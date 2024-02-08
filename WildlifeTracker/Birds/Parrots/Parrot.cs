using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        #endregion

        #region // Methods //
        #endregion
    }
}