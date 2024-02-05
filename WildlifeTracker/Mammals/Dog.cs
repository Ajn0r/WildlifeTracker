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
        private double tailLength;
        private bool isSpecialTrained;
        private SpecialTrainingType specialTrainingType;
        #endregion

        #region // Properties //
        public string Breed { get => breed; set => breed = value; }
        public double TailLength { get => tailLength; set => tailLength = value; }
        public bool IsSpecialTrained { get => isSpecialTrained; set => isSpecialTrained = value; }
        public SpecialTrainingType SpecialTrainingType
        {
            get => default;
            set
            {
            }
        }
        #endregion

        #region // Constructors //
        #endregion

        #region // Methods //
        #endregion


    }
}