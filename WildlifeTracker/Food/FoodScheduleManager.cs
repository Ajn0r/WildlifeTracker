﻿using System;
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

        /// <summary>
        /// Method to add an animal to a food schedule
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="foodSchedule"></param>
        public bool AddAnimalToFoodSchedule(Animal animal, FoodSchedule foodSchedule)
        {
            bool result = false;
            // Check if the food schedule exists
            if (foodScheduleDict.ContainsKey(foodSchedule))
            {
                // Get the list of animals for the food schedule
                ListManager<Animal> animalList = foodScheduleDict[foodSchedule];
                if (CheckifAnimalIsInFoodSchedule(animal)) // Check if the animal is already in the list
                {
                    MessageBox.Show("The animal is already connected to a food schedule");
                    return result;
                }
                // Check if the list is null
                if (animalList == null)
                {
                    // If it is, it means there is no list, so a new one is created
                    animalList = new ListManager<Animal>();
                    animalList.Add(animal); // Add the animal to the list

                    // Add the list to the dictionary with the food schedule as the key
                    foodScheduleDict[foodSchedule] = animalList;
                }
                else // else if the list is not null, there is a list so just add the animal to the list
                {
                    // add the animal to the list
                    animalList.Add(animal);
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// MEthod to remove a animal from the food schedule, not in use at the moment but can be used in the future
        /// needs to be tested first and might need improvements
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="foodSchedule"></param>
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

        /// <summary>
        /// Method to add food schedule to the dictionary
        /// </summary>
        /// <param name="foodSchedule"></param>
        public void AddFoodSchedule(FoodSchedule foodSchedule)
        {
            if (!foodScheduleDict.ContainsKey(foodSchedule))
            {
                foodScheduleDict.Add(foodSchedule, null);
            }
        }

        /// <summary>
        /// MEthod that returns a list of animals for a specific food schedule
        /// </summary>
        /// <param name="foodSchedule"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method that returns a food schedule based on the input key which is a string
        /// and check if the key matches the schedule title of the food schedule
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Methhod that returns the food schedule for a specific animal
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
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
                    // Check so that key and value are not null, and also that the value contains the animal
                    if (entry.Key != null && entry.Value != null && entry.Value.Contains(animal)) 
                    {
                        return entry.Key; // if the animal is found, return the food schedule
                    }
                }
            }
            return null; // return null if the animal is not found
        }

        /// <summary>
        /// Method to check if the animal is in the food schedule dictionary
        /// returns true if the animal is found, else false
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public bool CheckifAnimalIsInFoodSchedule(Animal animal)
        {
            // Loop through the dictionary and check if the animal is in the list
            foreach (KeyValuePair<FoodSchedule, ListManager<Animal>> entry in foodScheduleDict)
            {
                // if the value is not null, check if the animal is in the list
                if (entry.Value != null)
                {
                    // Get the list of animals
                    ListManager<Animal> anmList = entry.Value;
                    if (anmList.Contains(animal)) // if the animal is found, return true
                        return true;
                }
            }
            // return false if the animal is not found
            return false;
        }
    }
}
