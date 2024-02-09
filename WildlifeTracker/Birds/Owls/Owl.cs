using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Owl : Bird
    {
        #region // Instance variables //
        private string species;
        private bool isNocturnal;
        #endregion

        #region // Properties //
        public string Species { get => species; set => species = value; }
        public bool IsNocturnal { get => isNocturnal; set => isNocturnal = value; }
        #endregion

        #region // Constructors //
        public Owl(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
        }
        #endregion

        #region // Methods //
        #endregion
    }
}