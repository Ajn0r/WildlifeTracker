using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace WildlifeTracker.Helper_classes
{
    public class UtilitiesLibrary <T> where T : class
    {
        public static void JsonSerialize<T>(string fileName, List<T> list) 
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (T item in list)
                {
                    string jsonString = JsonSerializer.Serialize<T>(item);
                    writer.WriteLine(jsonString);
                }
            }
        }

        public static void JsonDeserialize(string fileName) { }

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
