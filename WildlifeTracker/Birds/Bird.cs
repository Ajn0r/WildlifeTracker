using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Bird : Animal
    {
        private bool sings;

        public bool Sings { get => sings; set => sings = value; }
    }
}