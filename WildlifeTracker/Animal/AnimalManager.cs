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
        public void AddAnimalWithID(Animal animal)
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

        #endregion
    }
}
