using System;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tidsbanken_BackEnd.Data.DTOs.CommentDTOs;
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
    public class CommentController : ControllerBase
    {
        // Private field to store an instance of the ServiceFacade, providing access to Comment-related services.
        private readonly ServiceFacade _serviceFacade;

        // Private field to store an instance of the auto mapper.
        private readonly IMapper _mapper;

        // Constructor for the CommentController, which takes the ServiceFacade as a dependency.
        public CommentController(ServiceFacade serviceFacade, IMapper mapper)
        {
            // Initialize the serviceFacade field with the provided instance of ServiceFacade.
            _serviceFacade = serviceFacade;
            // Initialize the _mapper field with the provided instance of Imapper.
            _mapper = mapper;
        }


        /// <summary>
        /// Get all Comments
        /// </summary>
        /// <returns>
        /// A list of Comments
        /// </returns>
        [HttpGet("{vacationRequestId}/Comments")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(int vacationRequestId)
        {
            try
            {
                return Ok(_mapper.Map<List<CommentDTO>>(await _serviceFacade._commentService.GetAllAsync(vacationRequestId)));
            }
            catch (VacationRequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves a specific Comment by its unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the Comment.</param>
        /// <param name="vacationRequestId">The ID of the Vacation Request that the comment is associated to.</param>
        /// <returns>
        /// A Comment object if found; otherwise, an error message
        /// </returns>
        [HttpGet("{vacationRequestId}/Comments/{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id, int vacationRequestId)
        {
            try
            {
                return Ok(_mapper.Map<CommentDTO>(await _serviceFacade._commentService.GetByIdAsync(id, vacationRequestId)));
            }
            catch (VacationRequestNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates the details of a specific Comment based on the provided Comment object and unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the Comment to be updated.</param>
        /// <param name="commentPutDTO">The Comment object containing the updated details.</param>
        /// <param name="vacationRequestId">The ID of the Vacation Request that the comment is associated to.</param>
        /// <returns>
        /// Returns NoContent if the operation is successful; otherwise, BadRequest or NotFound based on the error.
        /// </returns>
        [HttpPut("{vacationRequestId}/Comments/{id}")]
        public async Task<IActionResult> PutComment(int id, CommentPutDTO commentPutDTO, int vacationRequestId)
        {
            if (id != commentPutDTO.Id)
            {
                return BadRequest($"The id {id} given for Comment to be updated does not match the Comment id {commentPutDTO.Id} given in the body");
            }
            try
            {
                await _serviceFacade._commentService.UpdateAsync(_mapper.Map<Comment>(commentPutDTO), vacationRequestId);
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        /// <summary>
        /// Adds a new Comment to the database.
        /// </summary>
        /// <param name="commentPostDTO">The Comment object to be added.</param>
        /// <param name="vacationRequestId">The Id of the related VacationRequest object.</param>
        /// <returns>
        /// Returns a CreatedAtAction result, directing to the GetComment action to retrieve the newly added comment; otherwise, an error response.
        /// </returns>
        [HttpPost("{vacationRequestId}/Comments")]
        public async Task<ActionResult<CommentDTO>> PostComment(CommentPostDTO commentPostDTO, int vacationRequestId)
        {
            var newComment = await _serviceFacade._commentService.AddAsync(_mapper.Map<Comment>(commentPostDTO), vacationRequestId);

            return CreatedAtAction("GetComment", new { id = newComment.Id, vacationRequestId }, _mapper.Map<CommentDTO>(newComment));
        }


        /// <summary>
        /// Deletes a specified Comment from the database.
        /// </summary>
        /// <param name="id">The unique ID of the Comment to be deleted.</param>
        /// <param name="vacationRequestId">The Id of the related VacationRequest object.</param>
        /// <returns>
        /// Returns a NoContent response if deletion is successful; otherwise, a NotFound response with an error message
        /// </returns>
        [HttpDelete("{vacationRequestId}/Comments/{id}")]
        public async Task<IActionResult> DeleteUser(int id, int vacationRequestId)
        {
            try
            {
                await _serviceFacade._commentService.DeleteAsync(id, vacationRequestId);
                return NoContent();
            }
            catch (CommentNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}