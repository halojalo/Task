using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaplesTask;
using StaplesTask.Controllers;
using StaplesTask.Models;

namespace StaplesTask.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SaveDetailsTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            FormViewModel form = new FormViewModel() { Name = "Michał", Surname = "Jasek" };

            // Act
            ViewResult result = controller.SaveDetails(form) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
