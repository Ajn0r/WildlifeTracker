using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WildlifeTracker.Mammals.Dogs
{
    public class Dog : Mammal
    {
        #region // Instance variables //
        private string breed;
        private double tailLength;
        private bool isSpecialTrained;
        private SpecialTrainingType specialTrainingType;
        private FoodSchedule foodSchedule;
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

        #region // Constructor //
        public Dog(int numOfTeeth, bool hasFurOrHair) : base(numOfTeeth, hasFurOrHair)
        {
            SetFoodSchedule();
        }

        /// <summary>
        /// Method to add the dog specific attributes to the AnimalInfoWindow stack panel, that displays the animal information
        /// if the user clicks on the view animal button. Made it static so that it can be called from the AnimalInfoWindow class
        /// without creating an instance of the DogView class
        /// </summary>
        /// <param name="dog"></param>
        /// <param name="parentPanel"></param>

        #endregion

        #region // Methods //
        public static void AddDogSpecificAttributes(Dog dog, StackPanel parentPanel)
        {
            // Add the dog specific attributes to the animal info window stack panel by calling the AddAttributeRow method from the AnimalInfoWindow class
            AnimalInfoWindow.AddAttributeRow(parentPanel, "Breed", dog.Breed);
            AnimalInfoWindow.AddAttributeRow(parentPanel, "Tail Length", dog.TailLength.ToString());
            AnimalInfoWindow.AddAttributeRow(parentPanel, "Special Trained", dog.IsSpecialTrained ? "Yes" : "No");
            if (dog.IsSpecialTrained) // Only add the special trained type if the dog is special trained
            {
                AnimalInfoWindow.AddAttributeRow(parentPanel, "Training Type", dog.SpecialTrainingType.ToString());
            }
        }
        public override FoodSchedule GetFoodSchedule()
        {
            return foodSchedule;
        }

        /// <summary>
        /// Method to set the food schedule for the dog
        /// </summary>
        private void SetFoodSchedule()
        {
            foodSchedule = new FoodSchedule();
            foodSchedule.EaterType = EaterType.Carnivore;
            foodSchedule.Add("Morning: Dog food");
            foodSchedule.Add("Lunch: Boiled chicken");
            foodSchedule.Add("Evening: Milk and dry food");
        }
        #endregion


    }
}