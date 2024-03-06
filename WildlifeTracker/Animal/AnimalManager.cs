using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTracker;

namespace WildlifeTracker
{
    public class AnimalManager : ListManager<Animal>
    {
        #region // Instance variables //
        private int startID = 1;
        #endregion

        #region // Constructors //
        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public AnimalManager() : base() { }
        #endregion

        #region // Methods //

        /// <summary>
        /// Method to add animal with id
        /// </summary>
        /// <param name="animal"></param>
        private void AddAnimalWithID(Animal animal)
        {
            animal.Id = GetNewID(animal.Category);
            Add(animal);
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

        /*
        /// <summary>
        /// Method that sorts the list by the given parameter, using the sort method of the list
        /// and lambda expressions. Inspired by this solution: https://www.dotnetperls.com/sort-list
        /// </summary>
        /// <param name="sortBy"></param>
        public override void SortList(string sortBy, List<T> l)
        {
            // Sort the list by the given parameter
            if (sortBy == "Id")
                Sort((x, y) => x.Id.CompareTo(y.Id));
            else if (sortBy == "Name")
                Sort((x, y) => x.Name.CompareTo(y.Name));
            else if (sortBy == "Age")
                Sort((x, y) => x.Age.CompareTo(y.Age));
            else if (sortBy == "Color")
                Sort((x, y) => x.Color.CompareTo(y.Color));
            else if (sortBy == "Species")
                Sort((x, y) => x.AnimalType.CompareTo(y.AnimalType));
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
        } */
        #endregion
    }
}
