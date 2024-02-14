using System.Collections.Generic;
using System.Windows.Documents;


namespace WildlifeTracker
{
    public class FoodSchedule
    {
        #region // Instance variables //
        private EaterType eaterType;
        private List<string> foodList;
        #endregion

        #region // Properties //
        public EaterType EaterType { get => eaterType; set => eaterType = value; }
        public int Count { get => foodList.Count; } // Get the number of items in the list

        #endregion

        #region // Constructors //
        public FoodSchedule()
        {
            // Create a new list to store the food items
            foodList = new List<string>();
        }
        #endregion

        #region // Methods //
        /// <summary>
        /// Method to add an item to the food list
        /// </summary>
        /// <param name="item"></param>
        public void Add(string item)
        {
            foodList.Add(item);
        }
        /// <summary>
        /// Method to get the change an item at a specific index, pass the index to CheckIndex to check if it is valid first
        /// if valid, the input item will replace the item at the index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns>true if successful, else false</returns>
        public bool ChangeAt(int index, string item)
        {
            if (CheckIndex(index))
            {
                foodList[index] = item;
                return true;
            }
            return false;
        }
        /// <summary>
        ///  Method to check if the index is within the range of the list and larger or equal to 0
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True if index is valid, else false</returns>
        public bool CheckIndex(int index)
        {
            return (index >= 0 && index < foodList.Count); // Return the result of the check is index is larger or equal to 0 and less than the number of items in the list
        }

        /// <summary>
        /// Method to delete an item at a specific index, pass the index to CheckIndex to check if it is valid first
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True if succesfully deleted, else false</returns>
        public bool DeleteAt(int index)
        {
            if (CheckIndex(index))
            {
                foodList.RemoveAt(index); // Remove the item at the index
                return true; // Return true if the item was removed
            }
            return false;
        }
        public string[] GetFoodListInfoStrings()
        {
            string[] infoString = foodList.ToArray(); // Convert the list to an array and set it to infoString
            return infoString; // Return the array
        }

        // Not completely sure what this method is supposed to do, but I use it to return the title of the food schedule for the animal info window
        /// <summary>
        /// Method that returns the title of the food schedule for the animal info window, displays the eater type
        /// </summary>
        /// <returns></returns>
        public string Title()
        {
            return $"Eater type {eaterType} Food Schedule"; // Return the title of the food schedule
        }

        public override string ToString()
        {
            return $"Eater type: {eaterType}"; // Return the eater type
        }
        #endregion
    }
}