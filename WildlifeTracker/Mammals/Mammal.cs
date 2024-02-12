using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WildlifeTracker.Mammals.Cats;

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
        // Default constructor, pass the type of animal to the base class to use for ID
        public Mammal(int numOfTeeth, bool hasFurOrHair) : base("M", CategoryType.Mammal)
        {
            // Set the default values
            this.numOfTeeth = numOfTeeth;
            this.hasFurOrHair = hasFurOrHair;
        }
        #endregion

        #region // Methods //

        #endregion
    }
}