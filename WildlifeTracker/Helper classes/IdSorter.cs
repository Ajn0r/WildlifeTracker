using System.Collections.Generic;

namespace WildlifeTracker.Helper_classes
{
    public class IdSorter : IComparer<Animal>
    {
        private SortDirection direction;

        public IdSorter(SortDirection direction)
        {
            this.direction = direction;
        }
        public int Compare(Animal x, Animal y)
        {
            int result = direction == SortDirection.Ascending ? x.Id.CompareTo(y.Id) : y.Id.CompareTo(x.Id);
            return result;
        }
    }
}