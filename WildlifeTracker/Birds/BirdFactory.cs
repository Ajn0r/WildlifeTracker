using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker.Birds
{
    /// <summary>
    /// Factory class to create birds
    /// </summary>
    public class BirdFactory
    {
        /// <summary>
        /// Create a bird based on the species
        /// </summary>
        /// <param name="birdSpecies"></param>
        /// <returns></returns>
        public static Bird CreateBird(BirdSpecies birdSpecies, bool sings, bool canFly, int wingSpan)
        {
            Bird bird = null;
            switch (birdSpecies)
            {
                case BirdSpecies.Parrot:
                    bird = new Parrot(sings, canFly, wingSpan);
                    break;
                case BirdSpecies.Penguin:
                    bird = new Penguin(sings, canFly, wingSpan);
                    break;
                case BirdSpecies.Owl:
                    bird = new Owl(sings, canFly, wingSpan);
                    break;
                default:
                    return null;
            }
            return bird;
        }
    }
}
