using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker.Helper_classes
{
    public class AnimalComparer : IComparer<Animal>
    {
        private string sortBy;

        public AnimalComparer(string sortBy)
        {
            this.sortBy = sortBy;
        }

        public int Compare(Animal x, Animal y)
        {
            switch (sortBy)
            {
                case "Id":
                    return x.Id.CompareTo(y.Id);
                case "Name":
                    return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
                case "Age":
                    return x.Age.CompareTo(y.Age);
                case "Color":
                    return string.Compare(x.Color, y.Color, StringComparison.OrdinalIgnoreCase);
                case "Species":
                    return x.AnimalType.CompareTo(y.AnimalType);
                default:
                    throw new ArgumentException("Invalid sortBy parameter");
            }
        }
    }

}
