using System;
using System.Collections.Generic;

namespace DyeDurhamSorter.Interfaces
{
    public interface INameSorter
    {
        List<PersonName> SortNames(List<PersonName> names);
    }
}
