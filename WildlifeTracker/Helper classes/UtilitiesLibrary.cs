using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WildlifeTracker.Mammals.Cats;
using WildlifeTracker.Mammals.Dogs;
using WildlifeTracker.Mammals.Donkeys;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Security.RightsManagement;


namespace WildlifeTracker.Helper_classes
{
    public class UtilitiesLibrary
    {
        /// <summary>
        /// Method to serialize a list of animals to a json file, only works for animals that are derived from the Animal class.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="list"></param>
        public static void JsonSerialize(string fileName, List<Animal> list) 
        {
            try
            {
                // Serialize the list of animals to a json string
                string jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
                // Write the json string to a file
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
            }
        }

        /// <summary>
        /// Method to deserialize a json file and return a list of animals, using a switch statement to determine the type of animal.
        /// The method to get the json strings before deserializing is inspired by a solution from this website:
        /// https://www.c-sharpcorner.com/UploadFile/vendettamit/parsing-list-of-json-elements-as-list-with-json-net/.
        /// How to iterate over the JArray object is inspired by this website: https://stackoverflow.com/questions/16045569/how-to-access-elements-of-a-jarray-or-iterate-over-them
        /// Not sure if we are allowed to use var? But i found that JObject and JArray worked for this purpose.
        /// JThe json objects are parsed to a JArray object to be able to iterate through them and check each before deserializing them.
        /// The issue I had was that once the json objects were deserialized, they was all created as Animal objects, and the animal type and all other species specific infomation was lost, 
        /// so this is the soulution I found to work best without having to rewrite the animal class too much.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<Animal> JsonDeserialize(string fileName)
        {
            // declare a variable to hold the json string and a list to hold the animals
            string jsonString = string.Empty;
            List<Animal> list = new List<Animal>();

            // Using a try block to catch any exceptions that may occur, read the json file and parse it
            try
            {
                // Read the json file
                string json = File.ReadAllText(fileName);
                // Convert the json string to a JArray object to iterate through the objects before deserializing them 
                // to be able to determind what type of animal each object is
                JArray jsonArray = JArray.Parse(json);
                // loop through the objects in the json array
                foreach (JObject animal in jsonArray)
                {
                    // Get the animal type from the json object, convert it to a string with a cast
                    string animalType = (string)animal["AnimalType"];
                    // A switch statement to determine the type of animal and deserialize it to the correct type
                    switch (animalType)
                    {
                        case "Cat":
                            list.Add(JsonConvert.DeserializeObject<Cat>(animal.ToString()));
                            break;
                        case "Dog":
                            list.Add(JsonConvert.DeserializeObject<Dog>(animal.ToString()));
                            break;
                        case "Donkey":
                            list.Add(JsonConvert.DeserializeObject<Donkey>(animal.ToString()));
                            break;
                        case "Owl":
                            list.Add(JsonConvert.DeserializeObject<Owl>(animal.ToString()));
                            break;
                        case "Parrot":
                            list.Add(JsonConvert.DeserializeObject<Parrot>(animal.ToString()));
                            break;
                        case "Penguin":
                            list.Add(JsonConvert.DeserializeObject<Penguin>(animal.ToString()));
                            break;
                        default:
                            // If the animal type is not recognized, add it as a generic animal
                            list.Add(JsonConvert.DeserializeObject<Animal>(animal.ToString()));
                            break;
                    }
                }
                // Catch any exceptions that may occur, I handle FileNotFoundException and general exceptions
            } catch (FileNotFoundException e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                list = null; // return null if an exception occurs, so that the caller method knows that something went wrong.
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                list = null;
            }
            // Return the list of animals
            return list;
        }

        public static bool TextSerialize<T>(string fileName, List<T> list) { 
            bool ok = false;
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (T item in list)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                ok = true;
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
            }
            return ok;
        }

        /// <summary>
        /// Mehtod that serialize a list of strings to a xml file, returns false if an exception occurs and shows a message box with the exception message
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="foodList"></param>
        /// <returns></returns>
        public static bool XmlSerialize(string fileName, List<string> foodList) 
        {
            bool ok = false;
            try
            {
                XmlSerializer xmlSerrializer = new XmlSerializer(typeof(List<string>));
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    xmlSerrializer.Serialize(writer, foodList);
                    writer.Close();
                }
                ok = true;
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
            }
            return ok;
        }

        /// <summary>
        /// Method that deserialize a xml file and return a list of strings, returns null if an exception occurs
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> XmlDeserialize(string fileName) 
        {
            List<string> foodItemList = new List<string>();
            try
            {
                XmlSerializer xmlSerrializer = new XmlSerializer(typeof(List<string>));
                using (StreamReader reader = new StreamReader(fileName))
                {
                    foodItemList = (List<string>)xmlSerrializer.Deserialize(reader);
                    reader.Close();
                }
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                foodItemList = null;
            }
            return foodItemList;
        }

        /// <summary>
        /// Method to serialize a list of food schedules to a xml file, only applicable to food schedules
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="foodList"></param>
        public static bool XmlSerializeSchedule(string fileName, List<FoodSchedule> foodList)
        {
            bool ok = false;
            try
            {
                XmlSerializer xmlSerrializer = new XmlSerializer(typeof(List<FoodSchedule>));
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    xmlSerrializer.Serialize(writer, foodList);
                    writer.Close();
                }
                ok = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
            }
            return ok;
        }

        /// <summary>
        /// Method to deserialize a xml file and return a list of food schedules
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<FoodSchedule> XmlDeserializeSchedule(string fileName) 
        { 
            List<FoodSchedule> foodList = new List<FoodSchedule>();
            try
            {
                XmlSerializer xmlSerrializer = new XmlSerializer(typeof(List<FoodSchedule>));
                using (StreamReader reader = new StreamReader(fileName))
                {
                    foodList = (List<FoodSchedule>)xmlSerrializer.Deserialize(reader);
                    reader.Close();
                }
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                foodList = null;
            }
            return foodList;    
        }
    }
}
