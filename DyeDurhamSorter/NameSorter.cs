using DyeDurhamSorter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DyeDurhamSorter
{
    public class NameSorter : INameSorter
    {
        public virtual List<PersonName> SortNames(List<PersonName> names)
        {
            return names.OrderBy(n => n.SurName, StringComparer.InvariantCultureIgnoreCase)
                    .ThenBy(n => n.GivenName, StringComparer.InvariantCultureIgnoreCase).ToList();
        }
    }
}
