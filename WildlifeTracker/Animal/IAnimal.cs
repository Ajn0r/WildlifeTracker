using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildlifeTracker
{
    interface IAnimal
    {
        string Name { get; set; }
        string Id { get; set; }
        GenderType Gender { get; set; }
        CategoryType Category { get; set; }

        string GetExtraInfo();
    }
}
