using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using COMP2007_AS2.Controllers;
using COMP2007_AS2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace COMP2007_AS2.Tests.Controllers
{
    [TestClass]
    public class PositionsControllerTest
    {
        PositionsController controller;
        Mock<IPositionsRepository> mock;
        List<Position> positions;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IPositionsRepository>();

            positions = new List<Position> {
                new Position { positionId = 1, duties = "Someting", hourlyPay = 12.12m, positionName = "Name" }
            };

            mock.Setup(m => m.Positions).Returns(positions.AsQueryable());
            controller = new PositionsController(mock.Object);
        }

        [TestMethod]
        public void PositionsIndexDataTest()
        {
            // Act
            var result = (List<Position>)((ViewResult)controller.Index()).Model;

            // Assert
            CollectionAssert.AreEqual(positions, result);
        }
        
        [TestMethod]
        public void PositionsIndexTest()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // DETAILS SECTION
        [TestMethod]
        public void PositionsDetailsValidIdTest()
        {
            // Act
            var result = (Position)((ViewResult)controller.Details(1)).Model;

            // Assert
            Assert.AreEqual(positions.ToList()[0], result);
        }

        [TestMethod]
        public void PositionsDetailsInvalidIdTest()
        {
            // Act
            var result = (Position)((ViewResult)controller.Details(551)).Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PositionsDetailsNullIdTest()
        {
            // Act
            var result = (ViewResult)controller.Details(null);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: DELETE SECTION
        [TestMethod]
        public void PositionsDeleteValidIdTest()
        {
            // Act
            var result = (Position)((ViewResult)controller.Delete(1)).Model;

            // Assert
            Assert.AreEqual(positions.ToList()[0], result);
        }

        [TestMethod]
        public void PositionsDeleteInvalidIdTest()
        {
            // Act
            var result = (ViewResult)controller.Delete(551);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void PositionsDeleteNullIdTest()
        {
            // Act
            var result = (ViewResult)controller.Delete(null);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: EDIT SECTION
        [TestMethod]
        public void PositionsEditValidIdTest()
        {
            // Act
            var result = (Position)((ViewResult)controller.Edit(1)).Model;

            // Assert
            Assert.AreEqual(positions.ToList()[0], result);
        }

        [TestMethod]
        public void PositionsEditInvalidIdTest()
        {
            // Act
            var result = (ViewResult)controller.Edit(551);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void PositionsEditNullIdTest()
        {
            // Act
            int? id = null;
            var result = (ViewResult)controller.Edit(id);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: CREATE SECTION
        [TestMethod]
        public void PositionCreateViewTest()
        {
            // Act
            ViewResult result = (ViewResult)controller.Create();

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        // POST: EDIT SECTION
        [TestMethod]
        public void PositionEditValidTest()
        {
            Position position = positions.ToList()[0];

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(position);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void PositionEditInvalidTest()
        {
            controller.ModelState.AddModelError("key", "error message");

            Position position = new Position
            {
                positionId = 77,
                positionName = "Invalid",
            };

            // Act
            ViewResult result = (ViewResult)controller.Edit(position);

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        // POST: DELETECONFIRMED
        [TestMethod]
        public void PositionsDeleteConfValidIdTest()
        {
            // Act            
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert            
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
