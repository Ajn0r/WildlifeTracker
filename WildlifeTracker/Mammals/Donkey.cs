using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Donkey : Mammal
    {
        #region // Instance variables //
        private int maxLoad;
        private bool isUsedAsPackAnimal;
        private double height;
        private double weight;
        #endregion

        #region // Properties //
        public int MaxLoad { get => maxLoad; set => maxLoad = value; }
        public bool IsUsedAsPackAnimal { get => isUsedAsPackAnimal; set => isUsedAsPackAnimal = value; }
        public double Height { get => height; set => height = value; }
        public double Weight { get => weight; set => weight = value; }
        #endregion

        #region // Constructors //
        #endregion

        #region // Methods //
        #endregion
    }
}