using AKTest.Api.Controllers;
using AKTest.Business.Services;
using AKTest.Common;
using AKTest.Data;
using AKTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKRestApi.Test
{
    [TestClass]
    public class GetEntrantByIdApiTests
    {
        Entrants entrants = new Entrants()
            {
                id=1, firstName="Entrant 1",lastName="result 1"
            };

        private string MockExceptionMessage = "Mock Exception";
        Mock<ILogger<EntrantController>> logger;
        Mock<IEntrantService> entrantService;
        public EntrantController controller;

        [TestInitialize]
        public void Setup()
        {
            logger = new Mock<ILogger<EntrantController>>();
            entrantService = new Mock<IEntrantService>();

            controller = new EntrantController(logger.Object, entrantService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = ControllerHelper.httpHeaderContext()
            };
        }

        [TestMethod]
        public async Task GetEntrantById_ShouldReturn_200ok()
        {
            //Arrange
            int? id = 1;
            entrantService.Setup(x => x.GetEntrantById(id)).ReturnsAsync(entrants);

            //Act
            var result = (await controller.GetAllEntrantsById(id)).Result as OkObjectResult;

            //Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Entrants));
            Assert.AreSame(entrants, (Entrants)result.Value);
        }

        [TestMethod]
        public async Task GetEntrantById_ShouldReturn_400BadRequest()
        {
            //Act
            int? id = 0;
            var result = (await controller.GetAllEntrantsById(id)).Result as BadRequestObjectResult;

            //Assert
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual((string)result.Value,Constants.ErrorMessage_ID_NotFound);
        }

        [TestMethod]
        public async Task GetEntrantById_ShouldReturn_404BadRequest()
        {
            //Arrange
            int? id = 30;
            entrantService.Setup(x => x.GetEntrantById(id)).ReturnsAsync(new Entrants());

            //Act
            var result = (await controller.GetAllEntrantsById(id)).Result as NotFoundResult;

            //Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetEntrantById_ShouldReturn_Entrant()
        {
            //Arrange
            int? id = 1;
            entrantService.Setup(x => x.GetEntrantById(id)).ReturnsAsync(entrants);

            //Act
            var result = (await controller.GetAllEntrantsById(id)).Result as ObjectResult;
            var items = (Entrants)result.Value;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, items.id);
            Assert.AreEqual("Entrant 1", items.firstName);
            Assert.AreEqual("result 1", items.lastName);
        }

    }//
}
