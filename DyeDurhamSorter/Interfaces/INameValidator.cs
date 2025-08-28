using System;
using System.Collections.Generic;

namespace DyeDurhamSorter.Interfaces
{
    public interface INameValidator
    {
        (List<string>, List<PersonName>) ReadAndValidateNames(string[] lines);
    }
}
