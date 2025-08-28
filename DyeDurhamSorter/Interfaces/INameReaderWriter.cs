using System;
using System.Collections.Generic;

namespace DyeDurhamSorter.Interfaces
{
    public interface INameReaderWriter
    {
        List<PersonName> ReadNames(string fileName);
        void SaveNames(List<PersonName> names);
    }
}
