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
                foreach (JObject jObject in jsonArray.Children<JObject>())
                {
                    // Get the animal type from the json object
                    string animalType = jObject["AnimalType"].ToObject<string>();
                    // A switch statement to determine the type of animal and deserialize it to the correct type
                    switch (animalType)
                    {
                        case "Cat":
                            list.Add(JsonConvert.DeserializeObject<Cat>(jObject.ToString()));
                            break;
                        case "Dog":
                            list.Add(JsonConvert.DeserializeObject<Dog>(jObject.ToString()));
                            break;
                        case "Donkey":
                            list.Add(JsonConvert.DeserializeObject<Donkey>(jObject.ToString()));
                            break;
                        case "Owl":
                            list.Add(JsonConvert.DeserializeObject<Owl>(jObject.ToString()));
                            break;
                        case "Parrot":
                            list.Add(JsonConvert.DeserializeObject<Parrot>(jObject.ToString()));
                            break;
                        case "Penguin":
                            list.Add(JsonConvert.DeserializeObject<Penguin>(jObject.ToString()));
                            break;
                        default:
                            // If the animal type is not recognized, add it as a generic animal
                            list.Add(JsonConvert.DeserializeObject<Animal>(jObject.ToString()));
                            break;
                    }
                }
            } catch (FileNotFoundException e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                list = null;
            } catch (Exception e)
            {
                MessageBox.Show("A error occurred: \n" + e.Message.ToString());
                list = null;
            }
            // Return the list of animals
            return list;
        }

        public static void TextSerialize<T>(string fileName, List<T> list) { 
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (T item in list)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }


        public static void XmlSerialize(string fileName) { }

        public static void XmlDeserialize(string fileName) { }

    }
}
