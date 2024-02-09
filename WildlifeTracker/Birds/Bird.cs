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
        public int WingSpan { get => wingSpan; set => wingSpan = value; }
        #endregion

        #region // Constructor //
        public Bird(bool sings, bool canFly, int wingSpan) : base("B")
        {
            this.sings = sings;
            this.canFly = canFly;
            this.wingSpan = wingSpan;
        }
        #endregion

        #region // Methods //
        #endregion

    }
}