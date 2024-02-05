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
            // If the dog is special trained, get the training type, otherwise return None
            get => isSpecialTrained ? specialTrainingType : SpecialTrainingType.None;
            set
            {
                // If the dog is special trained, then the training type is set, otherwise it will be set to None
                specialTrainingType = isSpecialTrained ? value : SpecialTrainingType.None;
            }
        }
        #endregion

        #region // Constructors //
        // Default constructor
        public Dog() : base()
        {
        }

        // Constructor with all parameters
        public Dog(string name, int age, bool isDemesticated, GenderType gender, CategoryType category, string color, int numOfTeeth, bool hasFurOrHair, string breed, double tailLength, bool isSpecialTrained, SpecialTrainingType trainingType) :
            base(name, age, isDemesticated, gender, category, color, numOfTeeth, hasFurOrHair)
        {
            this.breed = breed;
            this.tailLength = tailLength;
            this.isSpecialTrained = isSpecialTrained;
            this.specialTrainingType = trainingType;
        }
        #endregion

        #region // Methods //
        #endregion


    }
}