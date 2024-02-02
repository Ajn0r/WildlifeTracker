using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Mammal : Animal
    {
        private int numOfTeeth;

        public int NumOfTeeth { get => this.numOfTeeth; set => this.numOfTeeth = value; }
    }
}