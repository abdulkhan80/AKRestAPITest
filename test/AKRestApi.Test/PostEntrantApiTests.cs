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
    public class PostEntrantApiTests
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
            id = 6,
            firstName = "Entrant 6",
            lastName = "result 6"
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
        public async Task PostEntrants_ShouldReturn_200ok()
        {
            //Arrange
            entrantService.Setup(x => x.PostEntrant(entrants));

            //Act
            var result = (await controller.PostEntrant(entrants)) as OkObjectResult;
            //Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("New Entrant Added Successfully!", result.Value.ToString());
        }

        [TestMethod]
        public async Task PostEntrants_ShouldReturn_400BadRequest()
        {
            //Arrange
            entrantService.Setup(x => x.PostEntrant(entrants));

            //Act
            var result = (await controller.PostEntrant(null)) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual((string)result.Value, Constants.ErrorMessage_BadRequest);
        }

        [TestMethod]
        public async Task PostEntrants_Invlaid_ID_ShouldReturn_400BadRequest()
        {
            //Arrange
            Entrants invalidEntrant = new Entrants()
            {
                id = 0,
                firstName = "Entrant 6",
                lastName = "result 6"
            };

            entrantService.Setup(x => x.PostEntrant(entrants));

            //Act
            var result = (await controller.PostEntrant(invalidEntrant)) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual((string)result.Value, Constants.ErrorMessage_ID_NotFound);
        }
        [TestMethod]
        public async Task PostEntrants_Invlaid_FirstName_ShouldReturn_400BadRequest()
        {
            //Arrange
            Entrants invalidEntrant = new Entrants()
            {
                id = 1,
                firstName = null,
                lastName = "result 6"
            };

            entrantService.Setup(x => x.PostEntrant(entrants));

            //Act
            var result = (await controller.PostEntrant(invalidEntrant)) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual((string)result.Value, Constants.ErrorMessage_FirstName_NotFound);
        }

        [TestMethod]
        public async Task PostEntrants_Invlaid_LastName_ShouldReturn_400BadRequest()
        {
            //Arrange
            Entrants invalidEntrant = new Entrants()
            {
                id = 1,
                firstName = "Entrant 6",
                lastName = null
            };

            entrantService.Setup(x => x.PostEntrant(entrants));

            //Act
            var result = (await controller.PostEntrant(invalidEntrant)) as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual((string)result.Value, Constants.ErrorMessage_LastName_NotFound);
        }

    }//
}
