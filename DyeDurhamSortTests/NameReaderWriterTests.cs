using DyeDurhamSorter;
using DyeDurhamSorter.Interfaces;
using Moq;
using System;
using System.Collections.Generic;

namespace DyeDurhamSortTests
{
    [TestClass]
    public sealed class NameReaderWriterTests
    {
        [TestMethod]
        public void ReadFileSuccessTest()
        {
            var mockFileUtility = new Mock<IFileUtility>();
            var mockNameValidator = new Mock<NameValidator>();
            var result = (
                new List<string>(), new List<PersonName>
                {
                    new PersonName("Baggins", "Bilbo"),
                    new PersonName("Baggins", "Frodo")
                });
            mockFileUtility.Setup(x => x.CheckFile(It.IsAny<string>())).Returns(true);
            mockFileUtility.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(new string[1]);
            mockNameValidator.Setup(x => x.ReadAndValidateNames(It.IsAny<string[]>())).Returns(result);
            
            var nameReaderWriter = new NameReaderWriter(mockFileUtility.Object, mockNameValidator.Object);
            var fileName = "unsorted-names-list.txt";
            var names = nameReaderWriter.ReadNames(fileName);

            mockFileUtility.Verify(x => x.CheckFile(fileName), Times.Once);
            mockFileUtility.Verify(x => x.ReadFile(fileName), Times.Once);
            mockNameValidator.Verify(x => x.ReadAndValidateNames(It.IsAny<string[]>()), Times.Once);
            Assert.AreEqual(result.Item2, names);
        }

        [TestMethod]
        public void ReadFileNotFoundFailTest()
        {
            var mockFileUtility = new Mock<IFileUtility>();
            var mockNameValidator = new Mock<NameValidator>();
            mockFileUtility.Setup(x => x.CheckFile(It.IsAny<string>())).Returns(false);
            
            var nameReaderWriter = new NameReaderWriter(mockFileUtility.Object, mockNameValidator.Object);
            var fileName = "unsorted-names-list.txt";
            var exception = Assert.ThrowsException<CustomException>(() => nameReaderWriter.ReadNames(fileName));
            Assert.AreEqual("File not found : " + fileName, exception.Message);

            mockFileUtility.Verify(x => x.CheckFile(fileName), Times.Once);
            mockFileUtility.Verify(x => x.ReadFile(fileName), Times.Never);
            mockNameValidator.Verify(x => x.ReadAndValidateNames(It.IsAny<string[]>()), Times.Never);
        }

        [TestMethod]
        public void ReadFileErrorNamesFailTest()
        {
            var mockFileUtility = new Mock<IFileUtility>();
            var mockNameValidator = new Mock<NameValidator>();
            var result = (
                new List<string> { "Invalid name"}, new List<PersonName>());
            mockFileUtility.Setup(x => x.CheckFile(It.IsAny<string>())).Returns(true);
            mockFileUtility.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(new string[1]);
            mockNameValidator.Setup(x => x.ReadAndValidateNames(It.IsAny<string[]>())).Returns(result);

            var nameReaderWriter = new NameReaderWriter(mockFileUtility.Object, mockNameValidator.Object);
            var fileName = "unsorted-names-list.txt";
            var exception = Assert.ThrowsException<CustomException>(() => nameReaderWriter.ReadNames(fileName));
            Assert.AreEqual("Error reading names from file : Invalid name", exception.Message);

            mockFileUtility.Verify(x => x.CheckFile(fileName), Times.Once);
            mockFileUtility.Verify(x => x.ReadFile(fileName), Times.Once);
            mockNameValidator.Verify(x => x.ReadAndValidateNames(It.IsAny<string[]>()), Times.Once);
        }

        [TestMethod]
        public void SaveFileSuccessTest()
        {
            var mockFileUtility = new Mock<IFileUtility>();
            var mockNameValidator = new Mock<NameValidator>();
            mockFileUtility.Setup(x => x.SaveFile(It.IsAny<string>(), It.IsAny<List<string>>()));
            
            var nameReaderWriter = new NameReaderWriter(mockFileUtility.Object, mockNameValidator.Object);
            var personNames = new List<PersonName> { new PersonName ("Baggins", "Bilbo"),
                        new PersonName ("Baggins", "Frodo") };
            nameReaderWriter.SaveNames(personNames);
            mockFileUtility.Verify(x => x.SaveFile("sorted-names-list.txt", new List<string> { "Bilbo Baggins", "Frodo Baggins" }), Times.Once);
        }
    }
}
