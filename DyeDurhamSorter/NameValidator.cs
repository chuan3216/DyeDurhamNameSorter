using DyeDurhamSorter.Interfaces;
using System;
using System.Collections.Generic;

namespace DyeDurhamSorter
{
    public class NameValidator : INameValidator
    {
        // NOTE : this can be separated into two methods ReadNames to return List<PersonName>
        //          and ValidateNames to return error list
        //          but can be inefficient as it will lead to looping through the string lines twice
        public virtual (List<string>, List<PersonName>) ReadAndValidateNames(string[] lines)
        {
            List<string> errors = new List<string>();
            List<PersonName> names = new List<PersonName>();
            foreach (var line in lines)
            {
                // skip empty line
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // check number of given names
                var words = line.Split(" ");
                if (!CheckName(errors, words, line))
                {
                    continue;
                }

                // add valid name
                AddName(names, words);
            }

            // check at least one name
            if (!names.Any())
            {
                errors.Add("Name list is empty");
            }
            return (errors, names);
        }

        private bool CheckName(List<string> errors, string[] words, string line)
        {
            
            if (words.Length == 1)
            {
                errors.Add("No given name : " + line);
                return false;
            }
            if (words.Length > 4)
            {
                errors.Add("Given name more than 3 : " + line);
                return false;
            }
            return true;
        }

        private void AddName(List<PersonName> names, string[] words)
        {
            names.Add(new PersonName(words[words.Length - 1], words[0],
                    words.Length > 2 ? words[1] : null,
                    words.Length > 3 ? words[2] : null));

        }
    }     
}
