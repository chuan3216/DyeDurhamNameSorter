using DyeDurhamSorter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DyeDurhamSorter
{
    public class NameReaderWriter : INameReaderWriter
    {
        private readonly INameValidator _nameValidator;
        private readonly IFileUtility _fileUtility;
        private const string _outputFileName = "sorted-names-list.txt";
        public NameReaderWriter(IFileUtility fileUtility, INameValidator nameValidator)
        {
            _fileUtility = fileUtility;
            _nameValidator = nameValidator;
        }

        public virtual List<PersonName> ReadNames(string path)
        {
            if (!_fileUtility.CheckFile(path))
            {
                throw new CustomException("File not found : " + path);
            }

            var lines = _fileUtility.ReadFile(path);

            var (errors, names) = _nameValidator.ReadAndValidateNames(lines);
            if (errors.Count > 0)
            {
                throw new CustomException("Error reading names from file : " + String.Join(". ", errors));
            }
            return names;
        }

        public virtual void SaveNames(List<PersonName> names)
        {
            List<string> lines = names.Select(n => n.FullName).ToList();
            _fileUtility.SaveFile(_outputFileName, lines);
        }
    }
}
