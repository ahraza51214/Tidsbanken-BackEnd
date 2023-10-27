using System;
using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tidsbanken_BackEnd.Data.DTOs.VacationRequestDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;
using Tidsbanken_BackEnd.Services;

namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/v1/VacationRequests")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class VacationRequestController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to vacationRequest-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the VacationRequestController, which takes the ServiceFacade as a dependency.
        public VacationRequestController(ServiceFacade serviceFacade, IMapper mapper)
        {
            // Initialize the serviceFacade field with the provided instance of ServiceFacade.
            _serviceFacade = serviceFacade;
            // Initialize the _mapper field with the provided instance of Imapper.
            _mapper = mapper;
        }


        /// <summary>
        /// Get all VacationRequests
        /// </summary>
        /// <returns>
        /// A list of VacationRequests.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacationRequestDTO>>> GetVacationRequests()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Ok(_mapper.Map<List<VacationRequestDTO>>(await _serviceFacade._vacationRequestService.GetAllAsync(Guid.Parse(subject))));
        }


        /// <summary>
        /// Retrieves a specific VacationRequest by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the VacationRequest.</param>
        /// <returns>
        /// A VacationRequest object if found; otherwise, an error message.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VacationRequestDTO>> GetVacationRequest(int id)
        {
            try
            {
                return _mapper.Map<VacationRequestDTO>(await _serviceFacade._vacationRequestService.GetByIdAsync(id));
            }
            catch (VacationRequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific VacationRequest based on the provided VacationRequest object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the VacationRequest to be updated.</param>
        /// <param name="vacationRequestDTO">The VacationRequest object containing the updated details.</param>
        /// <returns>
        /// Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacationRequest(int id, VacationRequestPutDTO vacationRequestDTO)
        {
            if (id != vacationRequestDTO.Id)
            {
                return BadRequest($"the id {id} given for vacation request to be updated does not match the vacation request id {vacationRequestDTO.Id} given in the body");
            }

            try
            {
                await _serviceFacade._vacationRequestService.UpdateAsync(_mapper.Map<VacationRequest>(vacationRequestDTO));
            }
            catch (VacationRequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adds a new VacationRequest to the database.
        /// </summary>
        /// <param name="vacationRequestDTO">The VacationRequest object to be added.</param>
        /// <returns>
        /// Returns a CreatedAtAction result, directing to the GetVacationRequest action to retrieve the newly added VacationRequest; otherwise, an error response.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<VacationRequestDTO>> PostVacationRequest(VacationRequestPostDTO vacationRequestDTO)
        {
            var newVacationRequest = await _serviceFacade._vacationRequestService.AddAsync(_mapper.Map<VacationRequest>(vacationRequestDTO));

            return CreatedAtAction("GetVacationRequest", new { id = newVacationRequest.Id }, _mapper.Map<VacationRequestDTO>(newVacationRequest));
        }


        /// <summary>
        /// Deletes a specified VacationRequest from the database.
        /// </summary>
        /// <param name="id">The unique ID of the VacationRequest to be deleted.</param>
        /// <returns>
        /// Returns a NoContent response if deletion is successful; otherwise, a NotFound response with an error message.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _serviceFacade._vacationRequestService.DeleteAsync(id);
                return NoContent();
            }
            catch (VacationRequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}