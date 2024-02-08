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

        #region // Constructors //
        // Default constructor
        public Bird() : base("B")
        {
        }
        // Constructor with all parameters
        public Bird(string name, int age, bool isDomesticated, GenderType genderType, CategoryType categoryType, string color, bool sings, bool canFly, int wingSpan) : 
            base("B", name, age, isDomesticated, genderType, categoryType, color)
        {
            this.sings = sings;
            this.canFly = canFly;
            this.wingSpan = wingSpan;
        }
        #endregion

        #region // Methods //
        public static string[] GetBirdTypes()
        {
            string[] birdTypes = { "Owl", "Penguin", "Parrot" };
            return birdTypes;
        }
        #endregion

    }
}