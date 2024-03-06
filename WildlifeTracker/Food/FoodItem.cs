using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    public class FoodItem
    {
        #region // Instance variables //
        private ListManager<string> ingredients;
        private string name;
        #endregion

        #region // Properties //
        public int Count { get => ingredients.Count; }
        public string Name { get => name; set => name = value; }
        public ListManager<string> Ingredients { get => ingredients; }
        #endregion

        #region // Constructors //
        public FoodItem()
        {
            ingredients = new ListManager<string>();
        }
        #endregion

        #region // Methods //
        public override string ToString()
        {
            return name;
        }

        #endregion
    }
}
