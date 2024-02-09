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
        public static Bird CreateBird(BirdSpecies birdSpecies)
        {
            Bird bird = null;
            switch (birdSpecies)
            {
                case BirdSpecies.Parrot:
                    bird = new Parrot(true, true, 10);
                    break;
                case BirdSpecies.Penguin:
                    bird = new Penguin(true, true, 10);
                    break;
                case BirdSpecies.Owl:
                    bird = new Owl(true, true, 10);
                    break;
                default:
                    return null;
            }
            return bird;
        }
    }
}
