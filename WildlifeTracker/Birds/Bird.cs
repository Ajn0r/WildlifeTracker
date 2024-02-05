using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Bird : Animal
    {
        #region // Instance variables //
        private bool sings;
        private bool canFly;
        private int wingSpan;
        #endregion

        #region // Properties //
        public bool Sings { get => sings; set => sings = value; }
        public bool CanFly { get => canFly; set => canFly = value; }
        #endregion

        #region // Constructors //
        #endregion

        #region // Methods //
        #endregion

    }
}