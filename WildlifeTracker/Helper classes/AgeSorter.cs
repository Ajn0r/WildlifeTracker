using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker.Helper_classes
{
    public class AgeSorter : IComparer<Animal>
    {
        private SortDirection direction;

        public AgeSorter(SortDirection direction)
        {
            this.direction = direction;
        }
        public int Compare(Animal x, Animal y)
        {
            int result = direction == SortDirection.Ascending ? x.Age.CompareTo(y.Age) : y.Age.CompareTo(x.Age);
            return result;
        }
    }
}
