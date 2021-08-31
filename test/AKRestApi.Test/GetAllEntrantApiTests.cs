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
    public class GetAllEntrantApiTests
    {
        List<Entrants> entrantsList = new List<Entrants>()
            {
                new Entrants { id=1, firstName="Entrant 1",lastName="result 1"},
                new Entrants { id=2, firstName="Entrant 2",lastName="result 2"},
                new Entrants { id = 3, firstName = "Entrant 3", lastName = "result 3" },
                new Entrants { id = 4, firstName = "Entrant 4", lastName = "result 4" },
                new Entrants { id = 5, firstName = "Entrant 5", lastName = "result 5" },
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
        public async Task GetAllEntrants_ShouldReturn_200ok()
        {
            //Arrange
            entrantService.Setup(x => x.GetEntrantAll()).ReturnsAsync(entrantsList);

            //Act
            var result = (await controller.GetAllEntrants()).Result as OkObjectResult;

            //Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(List<Entrants>));
            Assert.AreSame(entrantsList, (List<Entrants>)result.Value);
        }

        [TestMethod]
        public async Task GetAllEntrants_ShouldReturn_404BadRequest()
        {
            //Arrange
            entrantService.Setup(x => x.GetEntrantAll()).ReturnsAsync(new List<Entrants>());

            //Act
            var result = (await controller.GetAllEntrants()).Result as NotFoundResult;

            //Assert
            Assert.AreEqual(404, result.StatusCode);
        }

    }//
}
