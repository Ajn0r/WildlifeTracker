using System.Collections.Generic;

namespace WildlifeTracker.Helper_classes
{
    public class SpeciesSorter : IComparer<Animal>
    {
        private SortDirection direction;

        public SpeciesSorter(SortDirection direction)
        {
            this.direction = direction;
        }
        public int Compare(Animal x, Animal y)
        {
            int result = direction == SortDirection.Ascending ? string.Compare(x.AnimalType, y.AnimalType) : string.Compare(y.AnimalType, x.AnimalType);
            return result;
        }
    }
}