using System.Collections.Generic;

namespace WildlifeTracker.Helper_classes
{
    public class ColorSorter : IComparer<Animal>
    {
        private SortDirection direction;

        public ColorSorter(SortDirection direction)
        {
            this.direction = direction;
        }
        public int Compare(Animal x, Animal y)
        {
            int result = direction == SortDirection.Ascending ? string.Compare(x.Color, y.Color) : string.Compare(y.Color, x.Color);
            return result;
        }
    }
}