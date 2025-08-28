using System;
using System.Collections.Generic;

namespace DyeDurhamSorter.Interfaces
{
    public interface IFileUtility
    {
        bool CheckFile(string fileName);
        string[] ReadFile(string fileName);
        void SaveFile(string fileName, List<string> lines);
    }
}
