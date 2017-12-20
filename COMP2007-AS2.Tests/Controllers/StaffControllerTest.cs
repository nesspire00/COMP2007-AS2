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
    public class StaffControllerTest
    {
        StaffController controller;
        Mock<IStaffRepository> mock;
        List<Staff> staffs;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IStaffRepository>();

            staffs = new List<Staff> {
                new Staff { staffId = 1, firstName = "Something", lastName = "Someone", positionId = 4, shiftHours = 12 }
            };

            mock.Setup(m => m.Staffs).Returns(staffs.AsQueryable());
            controller = new StaffController(mock.Object);
        }

        [TestMethod]
        public void StaffIndexDataTest()
        {
            // Act
            var result = (List<Staff>)((ViewResult)controller.Index()).Model;

            // Assert
            CollectionAssert.AreEqual(staffs, result);
        }

        [TestMethod]
        public void StaffIndexTest()
        {
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        // DETAILS SECTION
        [TestMethod]
        public void StaffDetailsValidIdTest()
        {
            // Act
            var result = (Staff)((ViewResult)controller.Details(1)).Model;

            // Assert
            Assert.AreEqual(staffs.ToList()[0], result);
        }

        [TestMethod]
        public void StaffDetailsInvalidIdTest()
        {
            // Act
            var result = (Staff)((ViewResult)controller.Details(551)).Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void StaffDetailsNullIdTest()
        {
            // Act
            var result = (ViewResult)controller.Details(null);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: DELETE SECTION
        [TestMethod]
        public void StaffDeleteValidIdTest()
        {
            // Act
            var result = (Staff)((ViewResult)controller.Delete(1)).Model;

            // Assert
            Assert.AreEqual(staffs.ToList()[0], result);
        }

        [TestMethod]
        public void StaffDeleteInvalidIdTest()
        {
            // Act
            var result = (ViewResult)controller.Delete(551);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void StaffDeleteNullIdTest()
        {
            // Act
            var result = (ViewResult)controller.Delete(null);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: EDIT SECTION
        [TestMethod]
        public void StaffEditValidIdTest()
        {
            // Act
            var result = (Staff)((ViewResult)controller.Edit(1)).Model;

            // Assert
            Assert.AreEqual(staffs.ToList()[0], result);
        }

        [TestMethod]
        public void StaffEditInvalidIdTest()
        {
            // Act
            var result = (ViewResult)controller.Edit(551);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void StaffEditNullIdTest()
        {
            // Act
            int? id = null;
            var result = (ViewResult)controller.Edit(id);

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET: CREATE SECTION
        [TestMethod]
        public void StaffCreateViewTest()
        {
            // Act
            ViewResult result = (ViewResult)controller.Create();

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        // POST: EDIT SECTION
        [TestMethod]
        public void StaffEditValidTest()
        {
            Staff staff = staffs.ToList()[0];

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(staff);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void StaffEditInvalidTest()
        {
            controller.ModelState.AddModelError("key", "error message");

            Staff staff = new Staff
            {
                staffId = 1,
                firstName = "Someting",
                lastName = "Someone",
                positionId = 4,
                shiftHours = 12 };

            // Act
            ViewResult result = (ViewResult)controller.Edit(staff);

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        // POST: DELETECONFIRMED
        [TestMethod]
        public void StaffDeleteConfValidIdTest()
        {
            // Act            
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            // Assert            
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
