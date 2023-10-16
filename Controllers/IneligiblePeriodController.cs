using System;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tidsbanken_BackEnd.Data.DTOs.IneligiblePeriodDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;
using Tidsbanken_BackEnd.Services;

namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/v1/IneligiblePeriods")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class IneligiblePeriodController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to IneligiblePeriod-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the IneligiblePeriodController, which takes the ServiceFacade as a dependency.
        public IneligiblePeriodController(ServiceFacade serviceFacade, IMapper mapper)
        {
            // Initialize the serviceFacade field with the provided instance of ServiceFacade.
            _serviceFacade = serviceFacade;
            // Initialize the _mapper field with the provided instance of Imapper.
            _mapper = mapper;
        }


        /// <summary>
        /// Get all IneligiblePeriods
        /// </summary>
        /// <returns>
        /// A list of IneligiblePeriods
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IneligiblePeriodDTO>>> GetIneligiblePeriods()
        {
            return Ok(_mapper.Map<List<IneligiblePeriodDTO>>(await _serviceFacade._ineligiblePeriodService.GetAllAsync()));
        }


        /// <summary>
        /// Retrieves a specific IneligiblePeriod by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the IneligiblePeriod.</param>
        /// <returns>
        /// A IneligiblePeriod object if found; otherwise, an error message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IneligiblePeriodDTO>> GetIneligiblePeriod(int id)
        {
            try
            {
                return _mapper.Map<IneligiblePeriodDTO>(await _serviceFacade._ineligiblePeriodService.GetByIdAsync(id));
            }
            catch (IneligiblePeriodNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific IneligiblePeriod based on the provided User object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the IneligiblePeriod to be updated.</param>
        /// <param name="ineligiblePeriodDTO">The IneligiblePeriod object containing the updated details.</param>
        /// <returns>
        /// Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIneligiblePeriod(int id, IneligiblePeriodPutDTO ineligiblePeriodDTO)
        {
            if (id != ineligiblePeriodDTO.Id)
            {
                return BadRequest($"The id {id} given for IneligiblePeriod to be updated does not match the IneligiblePeriod id {ineligiblePeriodDTO.Id} given in the body");
            }

            try
            {
                await _serviceFacade._ineligiblePeriodService.UpdateAsync(_mapper.Map<IneligiblePeriod>(ineligiblePeriodDTO));
            }
            catch (IneligiblePeriodNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adds a new IneligiblePeriod to the database.
        /// </summary>
        /// <param name="ineligiblePeriodTO">The IneligiblePeriod object to be added.</param>
        /// <returns>
        /// Returns a CreatedAtAction result, directing to the GetIneligiblePeriod action to retrieve the newly added IneligiblePeriod; otherwise, an error response.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<IneligiblePeriodDTO>> PostIneligiblePeriod(IneligiblePeriodPostDTO ineligiblePeriodTO)
        {
            var newIneligiblePeriod = await _serviceFacade._ineligiblePeriodService.AddAsync(_mapper.Map<IneligiblePeriod>(ineligiblePeriodTO));

            return CreatedAtAction("GetIneligiblePeriod", new { id = newIneligiblePeriod.Id }, _mapper.Map<IneligiblePeriodDTO>(newIneligiblePeriod));
        }


        /// <summary>
        /// Deletes a specified IneligiblePeriod from the database.
        /// </summary>
        /// <param name="id">The unique ID of the IneligiblePeriod to be deleted.</param>
        /// <returns>
        /// Returns a NoContent response if deletion is successful; otherwise, a NotFound response with an error message
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIneligiblePeriod(int id)
        {
            try
            {
                await _serviceFacade._ineligiblePeriodService.DeleteAsync(id);
                return NoContent();
            }
            catch (IneligiblePeriodNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}