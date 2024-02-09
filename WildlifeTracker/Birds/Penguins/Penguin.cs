﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WildlifeTracker
{
    public class Penguin : Bird
    {
        #region // Instance variables //
        private string favoriteFish;
        private bool canSwim;
        #endregion

        #region // Properties //
        public string FavoriteFish { get => favoriteFish; set => favoriteFish = value; }
        public bool CanSwim { get => canSwim; set => canSwim = value; }
        #endregion

        #region // Constructors //
        public Penguin(bool sings, bool canFly, int wingSpan) : base(sings, canFly, wingSpan)
        {
        }
        #endregion

        #region // Methods //
        #endregion
    }
}