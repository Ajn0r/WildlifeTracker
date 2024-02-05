using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Dog : Mammal
    {
        #region // Instance variables //
        private string breed;
        private int tailLength;
        private bool isSpecialTrained;
        private SpecialTrainingType specialTrainingType;
        #endregion

        #region // Properties //
        public string Breed { get => breed; set => breed = value; }
        public int TailLength { get => tailLength; set => tailLength = value; }
        public bool IsSpecialTrained { get => isSpecialTrained; set => isSpecialTrained = value; }
        public SpecialTrainingType SpecialTrainingType { get => specialTrainingType; set => specialTrainingType = value; }
        #endregion

        #region // Constructors //
        #endregion

        #region // Methods //
        #endregion

    }
}