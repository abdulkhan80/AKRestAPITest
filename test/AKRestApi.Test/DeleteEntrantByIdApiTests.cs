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
    public class DeleteEntrantByIdApiTests
    {
        List<Entrants> entrantsList = new List<Entrants>()
            {
                new Entrants { id=1, firstName="Entrant 1",lastName="result 1"},
                new Entrants { id=2, firstName="Entrant 2",lastName="result 2"},
                new Entrants { id = 3, firstName = "Entrant 3", lastName = "result 3" },
                new Entrants { id = 4, firstName = "Entrant 4", lastName = "result 4" },
                new Entrants { id = 5, firstName = "Entrant 5", lastName = "result 5" },
            };

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
        public async Task DeleteEntrantById_ShouldReturn_200ok()
        {
            //Arrange
            int? id = 1;
            entrantService.Setup(x => x.DeleteEntrant(id));

            //Act
            var result = (await controller.DeleteEntrant(id)) as OkObjectResult;

            //Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Entrant Deleted Successfully!", result.Value.ToString());
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

    }//
}
