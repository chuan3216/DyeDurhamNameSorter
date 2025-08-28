using DyeDurhamSorter.Interfaces;
using System.IO;

namespace DyeDurhamSorter
{
    public class FileUtility : IFileUtility
    {
        public bool CheckFile(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new CustomException("No filename provided");
            }
            return File.Exists(fileName);
        }

        public string[] ReadFile(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        public void SaveFile(string fileName, List<string> lines)
        {
            File.WriteAllLines(fileName, lines);
        }
    }
}
