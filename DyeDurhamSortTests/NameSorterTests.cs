using DyeDurhamSorter;
using System;
using System.Collections.Generic;

namespace DyeDurhamSortTests
{
    [TestClass]
    public sealed class NameSorterTests
    {
        [TestMethod]
        public void SortSurnamesTest()
        {
            var names = new List<PersonName>{ new PersonName("Beggins", "Abe"),
                new PersonName("Boggins", "Bil", "Bo"),
                new PersonName("Baggins", "Frodo") };
                
            var sorter = new NameSorter();
            var sortedNames = sorter.SortNames(names);
            Assert.AreEqual("Frodo Baggins", sortedNames[0].FullName);
            Assert.AreEqual("Abe Beggins", sortedNames[1].FullName);
            Assert.AreEqual("Bil Bo Boggins", sortedNames[2].FullName);
        }

        [TestMethod]
        public void SortGivenNamesTest()
        {
            var names = new List<PersonName>{ new PersonName("Beggins", "Abe"),
                new PersonName("Baggins", "Frodo"),
                new PersonName("Baggins", "Bil", "Bo"),
                new PersonName("Baggins", "Fro", "do"),
                new PersonName("Baggins", "Fro", "d", "o")};

            var sorter = new NameSorter();
            var sortedNames = sorter.SortNames(names);
            Assert.AreEqual("Bil Bo Baggins", sortedNames[0].FullName);
            Assert.AreEqual("Fro d o Baggins", sortedNames[1].FullName);
            Assert.AreEqual("Fro do Baggins", sortedNames[2].FullName);
            Assert.AreEqual("Frodo Baggins", sortedNames[3].FullName);
            Assert.AreEqual("Abe Beggins", sortedNames[4].FullName);
        }

        [TestMethod]
        public void SortNamesIgnoreCaseTest()
        {
            var names = new List<PersonName>{ new PersonName("Baggins", "abe"),
                new PersonName("Baggins", "Frodo"),
                new PersonName("baggins", "Bil", "Bo")};

            var sorter = new NameSorter();
            var sortedNames = sorter.SortNames(names);
            Assert.AreEqual("abe Baggins", sortedNames[0].FullName);
            Assert.AreEqual("Bil Bo baggins", sortedNames[1].FullName);
            Assert.AreEqual("Frodo Baggins", sortedNames[2].FullName);
        }
    }
}
