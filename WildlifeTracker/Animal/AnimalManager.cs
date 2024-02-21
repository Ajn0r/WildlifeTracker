using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    public class AnimalManager
    {
        #region // Instance variables //
        private List<Animal> animalList;
        private int startID = 1;
        #endregion

        #region // Properties //
        public int Count { get => animalList.Count; }
        #endregion

        #region // Constructors //
        /// <summary>
        /// Constructor that creates a new list of animals on instantiation
        /// </summary>
        public AnimalManager()
        {
            animalList = new List<Animal>();
        }
        #endregion

        #region // Methods //
        /// <summary>
        /// Method to add an animal to the list
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public bool Add(Animal animal)
        {
            if (animal == null) // Check if the animal passed is null
                return false; // if so, return false
            // else, continue with the method and set the id of the animal to the result of the GetNewID method
            animal.Id = GetNewID(animal.Category);
            animalList.Add(animal); // Add the animal to the list and return true
            return true;
        }

        /// <summary>
        /// Method that creates an id with the type of animal as its first initial and returns it
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private string GetNewID(CategoryType category)
        {
            string id = "";
            if (category == CategoryType.Bird)
            {
                id = "B" + startID.ToString("D3");
            }
            else if (category == CategoryType.Mammal)
            {
                id = "M" + startID.ToString("D3");
            }
            startID++;
            return id;
        }
        /// <summary>
        /// MEthod that returns the animal at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Animal GetAnimalAt(int index)
        {
            if (CheckIndex(index)) // Chekci if the index is valid
                return animalList[index]; // Return the animal at the index
            return null; // if not valid, return null
        }

        /// <summary>
        /// Method to validate that the index is within the range of the list and valid
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CheckIndex(int index)
        {
            return (index >= 0 && index < animalList.Count);
        }

        /// <summary>
        /// Method that returns a string array of the animal info from the animals in the list
        /// by calling their ToString method. 
        /// This method is currently not used, since the animal info is displayed in a listbox with the help of binding and the animallistcopy method
        /// </summary>
        /// <returns>A string array with the animal info</returns>
        public string[] GetAnimalInfoString()
        {
            // Create a string array with the same length as the animal list
            string[] animalInfo = new string[animalList.Count];
            for (int i = 0; i < animalList.Count; i++) // loop trough the animal list
            {
                animalInfo[i] = animalList[i].GetExtraInfo(); // for each index, call the ToString method of the animal and set it to the array
            }
            return animalInfo; // return the array

        }

        /// <summary>
        /// Method that sorts the list by the given parameter, using the sort method of the list
        /// and lambda expressions. Inspired by this solution: https://www.dotnetperls.com/sort-list
        /// </summary>
        /// <param name="sortBy"></param>
        public void SortList(string sortBy)
        {
            // Sort the list by the given parameter
            if (sortBy == "Id")
                animalList.Sort((x, y) => x.Id.CompareTo(y.Id));
            else if (sortBy == "Name")
                animalList.Sort((x, y) => x.Name.CompareTo(y.Name));
            else if (sortBy == "Age")
                animalList.Sort((x, y) => x.Age.CompareTo(y.Age));
            else if (sortBy == "Color")
                animalList.Sort((x, y) => x.Color.CompareTo(y.Color));
            else if (sortBy == "Species")
                animalList.Sort((x, y) => x.AnimalType.CompareTo(y.AnimalType));
        }

        /// <summary>
        ///  Method that sorts the list in descending order by the given parameter
        /// </summary>
        /// <param name="sortBy"></param>
        internal void SortListDesc(string sortBy)
        {
            if (sortBy == "Id")
                animalList.Sort((x, y) => y.Id.CompareTo(x.Id));
            else if (sortBy == "Name")
                animalList.Sort((x, y) => y.Name.CompareTo(x.Name));
            else if (sortBy == "Age")
                animalList.Sort((x, y) => y.Age.CompareTo(x.Age));
            else if (sortBy == "Color")
                animalList.Sort((x, y) => y.Color.CompareTo(x.Color));
            else if (sortBy == "Species")
                animalList.Sort((x, y) => y.AnimalType.CompareTo(x.AnimalType));
        }
        #endregion
    }
}
