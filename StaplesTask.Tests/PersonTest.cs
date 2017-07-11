using System;
using PersonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaplesTask.Tests
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void SaveToDatabaseTest()
        {
            //arrange
            Person person = new Person() { Name = "Michał", Surname = "Jasek" };

            //act
            bool saved = person.SaveToDatabase();

            //assert
            Assert.IsFalse(saved);
        }

        [TestMethod]
        public void SaveFileTest()
        {
            //arrange
            Person person = new Person() { Name = "Michał", Surname = "Jasek" };

            //act
            bool saved = person.SaveFile("xml");

            //assert
            Assert.IsFalse(saved);
        }
    }
}
