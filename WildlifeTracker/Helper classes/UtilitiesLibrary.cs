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


namespace WildlifeTracker.Helper_classes
{
    public class UtilitiesLibrary
    {
        public static void JsonSerialize(string fileName, List<Animal> list) 
        {
            /*using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Animal item in list)
                {
                    string jsonString = JsonConvert.SerializeObject(item);
                    writer.WriteLine(jsonString);
                }
            }*/
            try
            {
                string jsonString = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public static List<Animal> JsonDeserialize(string fileName)
        {
            string jsonString = string.Empty;
            List<Animal> list = new List<Animal>();
            
            string json = File.ReadAllText(fileName);
            JArray jsonArray = JArray.Parse(json);
            foreach (JObject jObject in jsonArray.Children<JObject>())
            {
                string animalType = jObject["AnimalType"].ToObject<string>();
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
                    default:
                        // Om AnimalType är okänt eller saknas, lägg till som Animal
                        list.Add(JsonConvert.DeserializeObject<Animal>(jObject.ToString()));
                        break;
                }
            }

            /*
            using (StreamReader reader = new StreamReader(fileName))
            {
                while ((jsonString = reader.ReadLine()) != null)
                {
                    if (jsonString == null)
                        break;
                    Animal animal = System.Text.Json.JsonSerializer.Deserialize<Animal>(jsonString, options);
                    switch (animal.AnimalType)
                    {
                        case "Cat":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Cat>(jsonString, options));
                            break;
                        case "Dog":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Dog>(jsonString, options));
                            break;
                        case "Donkey":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Donkey>(jsonString, options));
                            break;
                        case "Owl":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Owl>(jsonString, options));
                            break;
                        case "Parrot":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Parrot>(jsonString, options));
                            break;
                        case "Penguin":
                            list.Add(System.Text.Json.JsonSerializer.Deserialize<Penguin>(jsonString, options));
                            break;
                        default:
                            list.Add(animal);
                            break;
                    }
                }
            }*/
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
