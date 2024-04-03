using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker.Helper_classes
{
    public class NameSorter : IComparer<Animal>
    {
        private SortDirection direction;
        
        public NameSorter(SortDirection direction)
        {
            this.direction = direction;
        }
        public int Compare(Animal x, Animal y)
        {
            int result = direction == SortDirection.Ascending ? string.Compare(x.Name, y.Name) : string.Compare(y.Name, x.Name);
            return result;
        }
    }
}
