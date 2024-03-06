﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    public class ListManager <T> : IListManager<T>
    {
        private List<T> list;

        public ListManager()
        {
            list = new List<T>();
        }

        /// <summary>
        /// Property that returns the number of items in the list
        /// </summary>
        public int Count { get => list.Count; }

        /// <summary>
        /// Method to add an item to the list
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Add(T type)
        {
            if (type == null)
                return false;
            list.Add(type);
            return true;
        }

        /// <summary>
        /// Method that changes an item at the index passed
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ChangeAt(int index, T type)
        {
            if (CheckIndex(index))
            {
                list[index] = type;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to validate that the index is within the range of the list and valid
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CheckIndex(int index)
        {
            return index >= 0 && index < list.Count;
        }

        /// <summary>
        /// Method that deletes an item at the index passed
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeleteAt(int index)
        {
            if (CheckIndex(index))
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method that deletes all items in the list
        /// </summary>
        public void DeleteAll()
        {
            list.Clear();
        }

        /// <summary>
        /// Method that gets an item at the index passed
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetAt(int index)
        {
            if (CheckIndex(index))
            {
                return list[index];
            }
            return default(T);
        }

        /// <summary>
        /// Method that returns a string array of the list
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            // Create a string array with the same length as the animal list
            string[] listInfo = new string[list.Count];
            for (int i = 0; i < list.Count; i++) // loop trough the animal list
            {
                listInfo[i] = list[i].ToString(); // for each index, call the ToString method of the animal and set it to the array
            }
            return listInfo; // return the array
        }

        /// <summary>
        /// Method that returns a string list of the list
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<string> ToStringList()
        {
            List<string> listInfo = new List<string>();
            foreach (var item in list)
            {
                listInfo.Add(item.ToString()); 
            }
            return listInfo;
        }

        public virtual void SortList(string sortBy, List<T> list)
        {
            throw new NotImplementedException();
        }

    }   

}