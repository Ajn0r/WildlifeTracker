﻿using System;
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
        // Default constructor, pass the type of animal to the base class to use for ID
        public Mammal() : base("M")
        {
            // Set the default values
            numOfTeeth = 0;
            hasFurOrHair = false;
        }

        // Constructor with all parameters
        public Mammal(string name, int age, bool isDomesticated, GenderType gender, CategoryType category, string color, int numOfTeeth, bool hasFurOrHair) 
            : base("M", name, age, isDomesticated, gender, category, color)
        {
            this.numOfTeeth = numOfTeeth;
            this.hasFurOrHair = hasFurOrHair;
        }
        #endregion

        #region // Methods //
        #endregion
    }
}