using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Mammal : Animal
    {
        #region // Instance variables //
        private int numOfTeeth;
        private bool hasFurOrHair;
        #endregion

        #region // Properties //
        public int NumOfTeeth { get => this.numOfTeeth; set => this.numOfTeeth = value; }
        public bool HasFurOrHair { get => hasFurOrHair; set => hasFurOrHair = value; }
        #endregion

        #region // Constructors //
        // Default constructor
        public Mammal()
        {
            // Set the default values
            numOfTeeth = 0;
            hasFurOrHair = false;
        }

        // Constructor with all parameters
        public Mammal(string name, int age, bool isDomesticated, bool isEndangered, GenderType gender, CategoryType category, int numOfTeeth, bool hasFurOrHair) 
            : base(name, age, isDomesticated, isEndangered, gender, category)
        {
            this.numOfTeeth = numOfTeeth;
            this.hasFurOrHair = hasFurOrHair;
        }
        #endregion
    }
}