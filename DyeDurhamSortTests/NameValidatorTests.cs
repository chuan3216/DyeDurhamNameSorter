using DyeDurhamSorter;
using System;
using System.Collections.Generic;

namespace DyeDurhamSortTests
{
    [TestClass]
    public sealed class NameValidatorTests
    {
        [TestMethod]
        [DataRow("Frodo Baggins,Bilbo Baggins")]
        [DataRow("Frodo Baggins,Bil bo Baggins, Pri mula BrandyBuck")]
        [DataRow("Frodo Baggins,Pri mu la Brandybuck")]
        public void ValidNamesReadSuccessTest(string content)
        {
            var lines = content.Split(",");
            var validator = new NameValidator();
            (var errors, var names) = validator.ReadAndValidateNames(lines);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(lines.Count(), names.Count);
        }

        [TestMethod]
        [DataRow("Frodo Baggins", "Frodo", null, null, "Frodo", "Baggins")]
        [DataRow("Fro do Baggins", "Fro", "do", null, "Fro do", "Baggins")]
        [DataRow("Fro do Bag gins", "Fro", "do", "Bag", "Fro do Bag", "gins")]
        public void ValidNameAddedFailTest(string content, string givenName1, string givenName2, string givenName3, string givenName, string surName)
        {
            var lines = content.Split(",");
            var validator = new NameValidator();
            (var errors, var names) = validator.ReadAndValidateNames(lines);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(1, names.Count);

            var name1 = names[0];
            Assert.AreEqual(givenName1, name1.GivenName1);
            Assert.AreEqual(givenName2, name1.GivenName2);
            Assert.AreEqual(givenName3, name1.GivenName3);
            Assert.AreEqual(givenName, name1.GivenName);
            Assert.AreEqual(surName, name1.SurName);
            Assert.AreEqual(lines[0], name1.FullName);
        }

        [TestMethod]
        public void InvalidNameCountFailTest()
        {
            string content = "Frodo Baggins,Baggins,Pri mu la Brandy Buck";
            var lines = content.Split(",");
            var validator = new NameValidator();
            (var errors, var names) = validator.ReadAndValidateNames(lines);
            Assert.IsTrue(errors.Count == 2);
            Assert.IsTrue(errors.Contains("No given name : Baggins"));
            Assert.IsTrue(errors.Contains("Given name more than 3 : Pri mu la Brandy Buck"));
            Assert.IsTrue(names.Count == 1);
        }

        [TestMethod]
        public void NoNamesTest()
        {
            string content = "  ,   , ";
            var lines = content.Split(",");
            var validator = new NameValidator();
            (var errors, var names) = validator.ReadAndValidateNames(lines);
            Assert.IsTrue(errors.Count == 1);
            Assert.IsTrue(errors.Contains("Name list is empty"));
            Assert.IsTrue(names.Count == 0);
        }
    }
}
