using DyeDurhamSorter;
using DyeDurhamSorter.Interfaces;
using Moq;
using System;
using System.Collections.Generic;

namespace DyeDurhamSortTests
{
    [TestClass]
    public sealed class NameProcessorTests
    {
        [TestMethod]
        public void ProcessSuccessTest()
        {
            var mockReaderWriter = new Mock<INameReaderWriter>();
            var mockSorter = new Mock<INameSorter>();

            var unsorted = new List<PersonName> { new PersonName ("Baggins", "Frodo"),
                        new PersonName ("Baggins", "Bilbo") };
            var sorted = new List<PersonName> { new PersonName ("Baggins", "Bilbo"),
                        new PersonName ("Baggins", "Frodo") };
            mockReaderWriter.Setup(x => x.ReadNames(It.IsAny<string>())).Returns(unsorted);
            mockReaderWriter.Setup(x => x.SaveNames(It.IsAny<List<PersonName>>()));
            mockSorter.Setup(x => x.SortNames(It.IsAny<List<PersonName>>())).Returns(sorted);
            
            var processor = new NameProcessor(mockReaderWriter.Object, mockSorter.Object);
            var fileName = "unsorted-names-list.txt";
            var count = processor.ProcessSortFile(fileName);

            mockReaderWriter.Verify(x => x.ReadNames(fileName), Times.Once);
            mockReaderWriter.Verify(x => x.SaveNames(sorted), Times.Once);
            mockSorter.Verify(x => x.SortNames(unsorted), Times.Once);
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void ProcessFailTest()
        {
            var mockReaderWriter = new Mock<INameReaderWriter>();
            var mockSorter = new Mock<INameSorter>();

            var unsorted = new List<PersonName> { new PersonName ("Baggins", "Frodo"),
                        new PersonName ("Baggins", "Bilbo") };
            mockReaderWriter.Setup(x => x.ReadNames(It.IsAny<string>())).Throws(new Exception("Error"));
            
            var processor = new NameProcessor(mockReaderWriter.Object, mockSorter.Object);
            var fileName = "unsorted-names-list.txt";
            var exception = Assert.ThrowsException<Exception>(() => processor.ProcessSortFile(fileName));
            Assert.AreEqual("Error", exception.Message);

            mockReaderWriter.Verify(x => x.ReadNames(fileName), Times.Once);
            mockReaderWriter.Verify(x => x.SaveNames(It.IsAny<List<PersonName>>()), Times.Never);
            mockSorter.Verify(x => x.SortNames(It.IsAny<List<PersonName>>()), Times.Never);
        }
    }
}
