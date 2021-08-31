using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AKTest.Business.Services;
using AKTest.Model;
using AKTest.Common;
using Newtonsoft.Json;

namespace AKTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrantController : ControllerBase
    {
        #region "Variable declaration and Const"
        private readonly ILogger<EntrantController> _logger;
        private readonly IEntrantService _entrantService;

        public EntrantController(ILogger<EntrantController> logger, IEntrantService entrantService)
        {
            _logger = logger;
            _entrantService = entrantService;
        }
        #endregion

        #region "GET Methods"
        [HttpGet("entrant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Entrants>>> GetAllEntrants()
        {
            try
            {
                _logger.LogInformation($"Get All Entrants List");

                var entrantsList = await this._entrantService.GetEntrantAll();

                if (entrantsList.Any())
                    return Ok(entrantsList);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("entrant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Entrants>> GetAllEntrantsById(int? id = 0)
        {
            try
            {
                _logger.LogInformation($"Get All Entrants By Id# {id}");

                if (id < 1 || id == null)
                {
                    _logger.LogError(Constants.ErrorMessage_ID_NotFound);
                    return BadRequest(Constants.ErrorMessage_ID_NotFound);
                }

                var entrant = await this._entrantService.GetEntrantById(id);

                if (entrant == null || entrant.id == null)
                    return NotFound();

                return Ok(entrant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region "POST Methods"
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostEntrant(Entrants entrants)
        {
            try
            {
                _logger.LogInformation($"Post Entrants Request: {JsonConvert.SerializeObject(entrants)}");

                if (entrants == null)
                {
                    _logger.LogError(Constants.ErrorMessage_BadRequest);
                    return BadRequest(Constants.ErrorMessage_BadRequest);
                }

                if (entrants.id == null || entrants.id < 1)
                {
                    _logger.LogError(Constants.ErrorMessage_ID_NotFound);
                    return BadRequest(Constants.ErrorMessage_ID_NotFound);
                }

                if (string.IsNullOrWhiteSpace(entrants.firstName))
                {
                    _logger.LogError(Constants.ErrorMessage_FirstName_NotFound);
                    return BadRequest(Constants.ErrorMessage_FirstName_NotFound);
                }

                if (string.IsNullOrWhiteSpace(entrants.lastName))
                {
                    _logger.LogError(Constants.ErrorMessage_LastName_NotFound);
                    return BadRequest(Constants.ErrorMessage_LastName_NotFound);
                }

                await this._entrantService.PostEntrant(entrants);

                return Ok("New Entrant Added Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region "DELETE Methods"
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEntrant(int? id)
        {
            try
            {
                _logger.LogInformation($"Get All Entrants By Id# {id}");

                if (id < 1 || id == null)
                {
                    _logger.LogError(Constants.ErrorMessage_ID_NotFound);
                    return BadRequest(Constants.ErrorMessage_ID_NotFound);
                }

                await this._entrantService.DeleteEntrant(id);

                return Ok("Entrant Deleted Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}
