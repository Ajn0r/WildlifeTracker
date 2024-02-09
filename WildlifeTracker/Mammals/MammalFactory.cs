using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildlifeTracker.Mammals.Cats;
using WildlifeTracker.Mammals.Dogs;
using WildlifeTracker.Mammals.Donkeys;

namespace WildlifeTracker.Mammals
{
    public class MammalFactory
    {
        /// <summary>
        /// A factory method to create a mammal object based on the species
        /// </summary>
        /// <param name="species"></param>
        /// <param name="numOfTeeth"></param>
        /// <param name="hasFurOrHair"></param>
        /// <returns></returns>
        public static Mammal CreateMammal(MammalSpecies species, int numOfTeeth, bool hasFurOrHair)
        {
            Mammal mammal = null;
            switch (species)
            {
                case MammalSpecies.Cat: // If the species is a cat, create a cat object
                    mammal = new Cat(numOfTeeth, hasFurOrHair);
                    break;
                case MammalSpecies.Dog: // If the species is a dog, create a dog object
                    mammal = new Dog(numOfTeeth, hasFurOrHair);
                    break;
                case MammalSpecies.Donkey: // If the species is a donkey, create a donkey object
                    mammal = new Donkey(numOfTeeth, hasFurOrHair);
                    break;
            }
            return mammal; // Return the mammal object
        }
    }
}
