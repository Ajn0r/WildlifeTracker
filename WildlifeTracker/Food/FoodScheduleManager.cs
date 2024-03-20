using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WildlifeTracker.Food
{
    public class FoodScheduleManager
    {
        private Dictionary<FoodSchedule, ListManager<Animal>> foodScheduleDict;

        public FoodScheduleManager()
        {
            foodScheduleDict = new Dictionary<FoodSchedule, ListManager<Animal>>();
        }
        public void AddAnimalToFoodSchedule(Animal animal, FoodSchedule foodSchedule)
        {
            // Kontrollera om foodSchedule redan finns i dictionaryn
            if (foodScheduleDict.ContainsKey(foodSchedule))
            {
                // Hämta listan med djur associerad med foodSchedule
                ListManager<Animal> animalList = foodScheduleDict[foodSchedule];

                if (animalList == null)
                {
                    // Skapa en ny lista för djur och lägg till det nya djuret
                    animalList = new ListManager<Animal>();
                    animalList.Add(animal);

                    // Lägg till den nya nyckeln och dess associerade lista i dictionaryn
                    foodScheduleDict[foodSchedule] = animalList;
                }
                else
                {
                    // Lägg till det nya djuret i listan
                    animalList.Add(animal);
                }
            }
        }

        public void RemoveAnimalFromFoodSchedule(Animal animal, FoodSchedule foodSchedule)
        {
            // Check if the food schedule exists
            if (foodScheduleDict.ContainsKey(foodSchedule))
            {
                // Check that the method does not return null
                if (foodScheduleDict[foodSchedule] == null)
                {
                    MessageBox.Show("The food schedule does not contain any animals.");
                    return;
                }
                // Get the animal list
                ListManager<Animal> tempAnimalList = GetAnimalsForFoodSchedule(foodSchedule);
                // Get the index of the animal
                int index = tempAnimalList.IndexOf(animal);
                // Remove the animal from the list
                foodScheduleDict[foodSchedule].DeleteAt(index);
            }
        }

        public void AddFoodSchedule(FoodSchedule foodSchedule)
        {
            if (!foodScheduleDict.ContainsKey(foodSchedule))
            {
                foodScheduleDict.Add(foodSchedule, null);
            }
        }

        public ListManager<Animal> GetAnimalsForFoodSchedule(FoodSchedule foodSchedule)
        {
            if (foodScheduleDict.ContainsKey(foodSchedule))
            {
                return foodScheduleDict[foodSchedule];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a list of food schedules as a list of FoodSchedule objects
        /// </summary>
        /// <returns></returns>
        public ListManager<FoodSchedule> GetFoodScheduleAsList()
        {
            ListManager<FoodSchedule> foodScheduleList = new ListManager<FoodSchedule>();
            foreach (KeyValuePair<FoodSchedule, ListManager<Animal>> entry in foodScheduleDict)
            {
                foodScheduleList.Add(entry.Key);
            }
            return foodScheduleList;
        }

        public FoodSchedule GetSelectedFoodSchedule(string key)
        {
            // Loop through the dictionary and return the food schedule that matches the input food schedule
            foreach (KeyValuePair<FoodSchedule, ListManager<Animal>> entry in foodScheduleDict)
            {
                if (key.Equals(entry.Key.ScheduleTitle))
                {
                    return entry.Key;
                }
            }
            return null;
        }

        public FoodSchedule GetFoodScheduleForAnimal(Animal animal)
        {

            // Check that the foodschedule dictionary is not empty
            if (foodScheduleDict.Count == 0)
            {
                return null;
            }
            if (CheckifAnimalIsInFoodSchedule(animal))
            {
                // Loop through the dictionary and return the food schedule that contains the input animal
                foreach (KeyValuePair<FoodSchedule, ListManager<Animal>> entry in foodScheduleDict)
                {
                    if (entry.Key != null)
                    {
                        return entry.Key;
                    }
                }
            }

            return null;
        }

        public bool CheckifAnimalIsInFoodSchedule(Animal animal)
        {
            foreach (KeyValuePair<FoodSchedule, ListManager<Animal>> entry in foodScheduleDict)
            {
                if (entry.Value != null)
                {
                    ListManager<Animal> anmList = entry.Value;
                    if (anmList.Contains(animal))
                        return true;
                }

            }
            return false;
        }
    }
}
