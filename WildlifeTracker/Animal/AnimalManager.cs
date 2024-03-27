using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WildlifeTracker;
using WildlifeTracker.Helper_classes;

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

        public override bool LoadFromJson(string fileName)
        {
            if (fileName == null)
                return false;
            DeleteAll();
            List<Animal> animalList = UtilitiesLibrary.JsonDeserialize(fileName);
            // if the list is null return false, since then a exception was thrown in the deserialization
            if (animalList == null)
                return false;
            // otherwise add each of the animals to the list
            foreach (Animal animal in animalList)
            {
                Add(animal); // add the animal to the list
            }
            return true;
        }

        #endregion
    }
}
