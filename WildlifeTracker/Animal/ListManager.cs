using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    public class ListManager <T> : IListManager<T>, IEnumerable<T>
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
        public bool Contains(T type)
        {
            return list.Contains(type);
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

        internal int IndexOf(T type)
        {
            int index = list.IndexOf(type);
            return index;
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void SortList(string sortBy)
        {
            switch (sortBy)
            {
                case "Id":
                    list.Sort((x, y) => Comparer.Default.Compare((x as Animal).Id, (y as Animal).Id));
                    break;
                case "Name":
                    list.Sort((x, y) => Comparer.Default.Compare((x as Animal).Name, (y as Animal).Name));
                    break;
                case "Age":
                    list.Sort((x, y) => Comparer.Default.Compare((x as Animal).Age, (y as Animal).Age));
                    break;
                case "Color":
                    list.Sort((x, y) => Comparer.Default.Compare((x as Animal).Color, (y as Animal).Color));
                    break;
                case "Species":
                    list.Sort((x, y) => Comparer.Default.Compare((x as Animal).AnimalType, (y as Animal).AnimalType));
                    break;
                default:
                    throw new ArgumentException("Invalid sortBy parameter");
                    
            }
        }
        public virtual void SortListDesc(string sortBy)
        {
            // Sortera listan i fallande ordning efter den givna parametern
            switch (sortBy)
            {
                case "Id":
                    list.Sort((x, y) => Comparer.Default.Compare((y as Animal).Id, (x as Animal).Id));
                    break;
                case "Name":
                    list.Sort((x, y) => Comparer.Default.Compare((y as Animal).Name, (x as Animal).Name));
                    break;
                case "Age":
                    list.Sort((x, y) => Comparer.Default.Compare((y as Animal).Age, (x as Animal).Age));
                    break;
                case "Color":
                    list.Sort((x, y) => Comparer.Default.Compare((y as Animal).Color, (x as Animal).Color));
                    break;
                case "Species":
                    list.Sort((x, y) => Comparer.Default.Compare((y as Animal).AnimalType, (x as Animal).AnimalType));
                    break;
                default:
                    throw new ArgumentException("Invalid sortBy parameter");
            }
        }

  
    }

}
